using System;
using System.Collections.Generic;
using FileApi.Enums;
using FileApi.Extensions;
using FileApi.Models;
using FileApi.Services;
using FileApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileApi.Controllers
{
    [ApiController]
    [Route("api/File/[Controller]/[Action]")]
    public class File2DbController : ControllerBase
    {
        private readonly File2DbService _fileService;
        private readonly ILogger<File2DbController> _logger;

        public File2DbController(ILogger<File2DbController> logger, File2DbService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        public ApiResponse<File> UploadFile([FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                var file = _fileService.UploadFile(formFile);
                return new ApiResponse<File>
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = file.WithoutCriticalData(), Error = null
                };
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
        public ApiResponse<List<File>> UploadMultipleFiles([FromForm(Name = "files[]")] List<IFormFile> formFiles)
        {
            try
            {
                var files = _fileService.UploadMultipleFiles(formFiles);
                files.WithoutCriticalData();
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

        [HttpPost]
        public ApiResponse<File> UpdateFile([FromForm(Name = "file")] IFormFile formFile,
            [FromForm(Name = "fileId")] string fileId)
        {
            try
            {
                var file = _fileService.UpdateFile(formFile, fileId);
                return new ApiResponse<File>
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = file.WithoutCriticalData(), Error = null
                };
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

        [HttpGet]
        public IActionResult GetFile(string fileId)
        {
            try
            {
                var item = _fileService.GetFile(fileId);
                return File(item.FileContent.ContentBytes, item.ContentType, item.FileName, true);
            }
            catch (Exception e)
            {
                _logger.LogError("GetFile: Exception occurred - fileId({0}), message({1}), stackTrace({2})", fileId,
                    e.Message,
                    e.StackTrace);
                return NoContent();
            }
        }

        [HttpGet]
        public ApiResponse<File> GetFileInfo(string fileId)
        {
            try
            {
                var item = _fileService.GetFileInfo(fileId);
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
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item.WithoutCriticalData(), Error = null
                };
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
        public ApiResponse<File> DeleteFile(string fileId)
        {
            try
            {
                _fileService.DeleteFile(fileId);
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