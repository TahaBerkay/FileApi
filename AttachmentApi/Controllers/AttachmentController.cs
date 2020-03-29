using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AttachmentApi.Enums;
using AttachmentApi.Models;
using AttachmentApi.Services;
using AttachmentApi.Utils;

namespace AttachmentApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class AttachmentController : ControllerBase
    {
        private readonly ILogger<AttachmentController> _logger;
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(ILogger<AttachmentController> logger, IAttachmentService attachmentService)
        {
            _logger = logger;
            _attachmentService = attachmentService;
        }

        [HttpPost]
        public ApiResponse<Attachment> AddAttachment(Attachment attachment)
        {
            try
            {
                _attachmentService.AddAttachment(attachment);
                return new ApiResponse<Attachment>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = attachment, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("AddAttachment: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<Attachment>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetAll()
        {
            try
            {
                return new ApiResponse<List<Attachment>>
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = _attachmentService.GetAll(),
                    Error = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("GetAll: Exception occurred - message({0}), stackTrace({1})", e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<Attachment> GetById(long id)
        {
            try
            {
                var item = _attachmentService.GetById(id);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<Attachment>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<Attachment>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetById: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<Attachment>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetByNotifierId(string notifierId)
        {
            try
            {
                var item = _attachmentService.GetByNotifierId(notifierId);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<List<Attachment>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Attachment>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifierId: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetByNotifiedBy(string notifiedBy)
        {
            try
            {
                var item = _attachmentService.GetByNotifiedBy(notifiedBy);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<List<Attachment>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Attachment>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifiedBy: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetNotifierAttachmentsAfterDate(string notifierId, DateTime afterDate)
        {
            try
            {
                var item = _attachmentService.GetNotifierAttachmentsAfterDate(notifierId, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<List<Attachment>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Attachment>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifierAttachmentsAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetNotifiedAttachmentsAfterDate(string notifiedBy, DateTime afterDate)
        {
            try
            {
                var item = _attachmentService.GetNotifiedAttachmentsAfterDate(notifiedBy, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<List<Attachment>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Attachment>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifiedAttachmentsAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Attachment>> GetAttachmentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            try
            {
                var item = _attachmentService.GetAttachmentsDetailed(notifierId, notifiedBy, afterDate, beforeDate,
                    entityType, entityAction, status);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.AttachmentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.AttachmentNotFound]
                    };
                    return new ApiResponse<List<Attachment>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Attachment>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetAttachmentsDetailed: Exception occurred - message({0}), stackTrace({1})",
                    e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Attachment>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }
    }
}