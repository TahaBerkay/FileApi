using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileApi.Enums;
using FileApi.Models;
using FileApi.Services;
using FileApi.Utils;

namespace FileApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService _fileService;

        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        public ApiResponse<File> AddFile(File file)
        {
            try
            {
                _fileService.AddFile(file);
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

        [HttpGet]
        public ApiResponse<List<File>> GetAll()
        {
            try
            {
                return new ApiResponse<List<File>>
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = _fileService.GetAll(),
                    Error = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("GetAll: Exception occurred - message({0}), stackTrace({1})", e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<File> GetById(long id)
        {
            try
            {
                var item = _fileService.GetById(id);
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

        [HttpGet]
        public ApiResponse<List<File>> GetByNotifierId(string notifierId)
        {
            try
            {
                var item = _fileService.GetByNotifierId(notifierId);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<List<File>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifierId: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<File>> GetByNotifiedBy(string notifiedBy)
        {
            try
            {
                var item = _fileService.GetByNotifiedBy(notifiedBy);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<List<File>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifiedBy: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<File>> GetNotifierFilesAfterDate(string notifierId, DateTime afterDate)
        {
            try
            {
                var item = _fileService.GetNotifierFilesAfterDate(notifierId, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<List<File>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifierFilesAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<File>> GetNotifiedFilesAfterDate(string notifiedBy, DateTime afterDate)
        {
            try
            {
                var item = _fileService.GetNotifiedFilesAfterDate(notifiedBy, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<List<File>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifiedFilesAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<File>> GetFilesDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            try
            {
                var item = _fileService.GetFilesDetailed(notifierId, notifiedBy, afterDate, beforeDate,
                    entityType, entityAction, status);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.FileNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.FileNotFound]
                    };
                    return new ApiResponse<List<File>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<File>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetFilesDetailed: Exception occurred - message({0}), stackTrace({1})",
                    e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<File>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }
    }
}