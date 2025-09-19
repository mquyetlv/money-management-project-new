using System.Linq.Expressions;

namespace money_management_service.Core
{
    public class CustomQuery<T>
    {
        public List<Expression<Func<T, bool>>> Filters { get; set; }

        public string? OrderBy { get; set; }

        public bool? IsDescending { get; set; } = false;

        public int Page { get; set; } = 0;

        public int Size { get; set; } = 10;
    }
}
