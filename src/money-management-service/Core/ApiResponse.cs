namespace money_management_service.Core
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public T? Data { get; set; }

        public Pagination? Pagination { get; set; }

        public ApiResponse() {
            Status = string.Empty;
            Message = string.Empty;
            Pagination = null;
        }

        public ApiResponse(string Status, string Message, T Data, Pagination? Pagination) 
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
            this.Pagination = Pagination;
        }
    }
}
