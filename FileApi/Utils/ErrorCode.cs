using System.Collections.Generic;
using FileApi.Enums;

namespace FileApi.Utils
{
    public class ErrorCode
    {
        public static readonly Dictionary<ErrorEnums.Error, string> ErrorCodes
            = new Dictionary<ErrorEnums.Error, string>
            {
                {ErrorEnums.Error.FileNotFound, "File is not found"},
                {ErrorEnums.Error.ExceptionOccurred, "An exception is occured"}
            };
    }
}