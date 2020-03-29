using AttachmentApi.Enums;

namespace AttachmentApi.Models
{
    public class ApiErrorResponse
    {
        public ErrorEnums.Error Code { get; set; }
        public string Message { get; set; }
    }
}