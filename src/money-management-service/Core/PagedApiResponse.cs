namespace money_management_service.Core
{
    public class PagedApiResponse<T>
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public T? Data { get; set; }

        public Pagination Pagination { get; set; }

        public PagedApiResponse()
        {
            Status = string.Empty;
            Message = string.Empty;
            Pagination = new Pagination();
        }

        public PagedApiResponse(string status, string message, T data, Pagination pagination)
        {
            Status = status;
            Message = message;
            Data = data;
            Pagination = pagination ?? new Pagination();
        }
    }
}
