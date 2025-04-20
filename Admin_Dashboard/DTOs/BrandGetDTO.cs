namespace Admin_Dashboard.DTOs
{
    public class BrandGetDTO
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductGetDTO> Products { get; set; }
    }
}
