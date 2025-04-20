namespace Admin_Dashboard.DTOs
{
    public class DataResponseDTO<T> where T : class
    {
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int MaxPage { get; set; }
    }
}
