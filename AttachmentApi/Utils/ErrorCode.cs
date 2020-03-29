using System.Collections.Generic;
using AttachmentApi.Enums;

namespace AttachmentApi.Utils
{
    public class ErrorCode
    {
        public static readonly Dictionary<ErrorEnums.Error, string> ErrorCodes
            = new Dictionary<ErrorEnums.Error, string>
            {
                {ErrorEnums.Error.AttachmentNotFound, "Attachment is not found"},
                {ErrorEnums.Error.ExceptionOccurred, "An exception is occured"}
            };
    }
}