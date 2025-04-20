using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Admin_Dashboard.DTOs
{
    public class ProductAddDTO
    {
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        public string Description { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        public IFormFile? ImageFile { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int? BrandId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int? CategoryId { get; set; }

    }
}
