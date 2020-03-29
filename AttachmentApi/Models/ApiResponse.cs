﻿using AttachmentApi.Enums;

namespace AttachmentApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public SuccessMsgEnums.Msg Message { get; set; }
        public T Result { get; set; }
        public ApiErrorResponse Error { get; set; }
    }
}