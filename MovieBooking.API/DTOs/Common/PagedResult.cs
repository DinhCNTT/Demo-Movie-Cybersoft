namespace MovieBooking.API.DTOs.Common
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
