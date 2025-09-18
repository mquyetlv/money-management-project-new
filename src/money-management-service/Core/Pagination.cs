namespace money_management_service.Core
{
    public class Pagination
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public int Total { get; set; }

        public Pagination() {
            Page = 0;
            Size = 0;
            Total = 0;
        }

        public Pagination(int page, int size, int total)
        {
            Page = page;
            Size = size;
            Total = total;
        }
    }
}
