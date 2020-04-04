using System;
using System.Collections.Generic;
using FileApi.Enums;
using FileApi.Models;
using FileApi.Services;
using FileApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        public ApiResponse<File> UploadFile2Db([FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                var file = _fileService.UploadFile2Db(formFile);
                return new ApiResponse<File>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = file, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("AddFile: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<File>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpPost]
        public ApiResponse<List<File>> UploadMultipleFiles2Db([FromForm(Name = "files[]")] List<IFormFile> formFiles)
        {
            try
            {
                var files = _fileService.UploadMultipleFiles2Db(formFiles);
                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = files, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("AddFile: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public IActionResult GetFileFromDb(string fileId)
        {
            var item = _fileService.GetFileFromDb(fileId);
            return File(item.FileContent.ContentBytes, item.ContentType, item.FileName);
        }

        [HttpGet]
        public ApiResponse<File> GetFileInfoFromDb(string fileId)
        {
            try
            {
                var item = _fileService.GetFileInfoFromDb(fileId);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<File>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<File>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetById: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<File>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpDelete]
        public ApiResponse<File> DeleteFileFromDb(string fileId)
        {
            try
            {
                _fileService.DeleteFileFromDb(fileId);
                return new ApiResponse<File>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = null, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetById: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<File>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }
    }
}