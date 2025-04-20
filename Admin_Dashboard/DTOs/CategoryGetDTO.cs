namespace Admin_Dashboard.DTOs
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductGetDTO> Products { get; set; }
    }
}
