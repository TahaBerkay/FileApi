using DocumentApi.Enums;

namespace DocumentApi.Models
{
    public class ApiErrorResponse
    {
        public ErrorEnums.Error Code { get; set; }
        public string Message { get; set; }
    }
}