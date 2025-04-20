using System.ComponentModel.DataAnnotations;

namespace Admin_Dashboard.DTOs
{
    public class CategoryAddDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Mô tả là bắt buộc.")]
        [ StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string Description { get; set; }
    }
}
