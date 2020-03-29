using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentApi.Enums;
using DocumentApi.Models;
using DocumentApi.Services;
using DocumentApi.Utils;

namespace DocumentApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentService _documentService;

        public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }

        [HttpPost]
        public ApiResponse<Document> AddDocument(Document document)
        {
            try
            {
                _documentService.AddDocument(document);
                return new ApiResponse<Document>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = document, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("AddDocument: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<Document>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetAll()
        {
            try
            {
                return new ApiResponse<List<Document>>
                {
                    Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = _documentService.GetAll(),
                    Error = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("GetAll: Exception occurred - message({0}), stackTrace({1})", e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<Document> GetById(long id)
        {
            try
            {
                var item = _documentService.GetById(id);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<Document>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<Document>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetById: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<Document>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetByNotifierId(string notifierId)
        {
            try
            {
                var item = _documentService.GetByNotifierId(notifierId);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<List<Document>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Document>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifierId: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetByNotifiedBy(string notifiedBy)
        {
            try
            {
                var item = _documentService.GetByNotifiedBy(notifiedBy);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<List<Document>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Document>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetByNotifiedBy: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetNotifierDocumentsAfterDate(string notifierId, DateTime afterDate)
        {
            try
            {
                var item = _documentService.GetNotifierDocumentsAfterDate(notifierId, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<List<Document>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Document>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifierDocumentsAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetNotifiedDocumentsAfterDate(string notifiedBy, DateTime afterDate)
        {
            try
            {
                var item = _documentService.GetNotifiedDocumentsAfterDate(notifiedBy, afterDate);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<List<Document>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Document>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "GetNotifiedDocumentsAfterDate: Exception occurred - message({0}), stackTrace({1})", e.Message,
                    e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }

        [HttpGet]
        public ApiResponse<List<Document>> GetDocumentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            try
            {
                var item = _documentService.GetDocumentsDetailed(notifierId, notifiedBy, afterDate, beforeDate,
                    entityType, entityAction, status);
                if (item == null)
                {
                    var error = new ApiErrorResponse
                    {
                        Code = ErrorEnums.Error.DocumentNotFound,
                        Message = ErrorCode.ErrorCodes[ErrorEnums.Error.DocumentNotFound]
                    };
                    return new ApiResponse<List<Document>>
                        {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
                }

                return new ApiResponse<List<Document>>
                    {Success = true, Message = SuccessMsgEnums.Msg.Ok, Result = item, Error = null};
            }
            catch (Exception e)
            {
                _logger.LogError("GetDocumentsDetailed: Exception occurred - message({0}), stackTrace({1})",
                    e.Message, e.StackTrace);
                var error = new ApiErrorResponse
                    {Code = ErrorEnums.Error.ExceptionOccurred, Message = e.Message};
                return new ApiResponse<List<Document>>
                    {Success = false, Message = SuccessMsgEnums.Msg.NotOk, Result = null, Error = error};
            }
        }
    }
}