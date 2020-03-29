using System.Collections.Generic;
using DocumentApi.Enums;

namespace DocumentApi.Utils
{
    public class ErrorCode
    {
        public static readonly Dictionary<ErrorEnums.Error, string> ErrorCodes
            = new Dictionary<ErrorEnums.Error, string>
            {
                {ErrorEnums.Error.DocumentNotFound, "Document is not found"},
                {ErrorEnums.Error.ExceptionOccurred, "An exception is occured"}
            };
    }
}