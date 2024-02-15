namespace Dispo.Shared.Filter.Model
{
    public class PaginationFilter
    {
        public string Entity { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
