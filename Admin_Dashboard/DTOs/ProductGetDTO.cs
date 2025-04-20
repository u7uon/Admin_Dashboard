namespace Admin_Dashboard.DTOs
{
    public class ProductGetDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

        public BrandDTO Brand { get; set; }


        public CategoryDTO Category { get; set; }

    }
}
