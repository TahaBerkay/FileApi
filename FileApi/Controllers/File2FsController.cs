﻿using System;
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
    public class File2FsController : ControllerBase
    {
        private readonly File2FsService _fileService;
        private readonly ILogger<File2FsController> _logger;

        public File2FsController(ILogger<File2FsController> logger, File2FsService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
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
        [DisableRequestSizeLimit]
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
        [DisableRequestSizeLimit]
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
        [DisableRequestSizeLimit]
        public IActionResult GetFile(string fileId)
        {
            try
            {
                var file = _fileService.GetFile(fileId);
                var fileNameInFs = _fileService.GetFilePathInFs(file.FileNameInFs);
                return PhysicalFile(fileNameInFs, file.ContentType, file.FileName, true);
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
        [DisableRequestSizeLimit]
        public ActionResult GetFileByStreaming(string fileId)
        {
            try
            {
                var file = _fileService.GetFile(fileId);
                var fileStream = _fileService.GetFileStream(file);
                return File(fileStream, file.ContentType, file.FileName, true);
            }
            catch (Exception e)
            {
                _logger.LogError("GetFileByStreaming: Exception occurred - fileId({0}), message({1}), stackTrace({2})",
                    fileId, e.Message,
                    e.StackTrace);
                return NoContent();
            }
        }

        [HttpGet]
        public ApiResponse<File> GetFileInfoFromFs(string fileId)
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