using FileApi.Enums;

namespace FileApi.Models
{
    public class ApiErrorResponse
    {
        public ErrorEnums.Error Code { get; set; }
        public string Message { get; set; }
    }
}