using static System.Runtime.InteropServices.JavaScript.JSType;

namespace money_management_service.Core
{
    public class ErrorResponse
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public List<ErrorDetail> Details { get; set; }

        public ErrorResponse()
        {
            Status = string.Empty;
            Message = string.Empty;
            Details = new List<ErrorDetail>();
        }

        public ErrorResponse(string status, string message, List<ErrorDetail> details)
        {
            Status = status;
            Message = message;
            Details = details ?? new List<ErrorDetail>();
        }
    }

    public class ErrorDetail
    {
        public string Field { get; set; }

        public string Error { get; set; }

        public ErrorDetail() 
        {
            Field = string.Empty;
            Error = string.Empty;
        }

        public ErrorDetail(string field, string error)
        {
            Field = field;
            Error = error;
        }
    }
}
