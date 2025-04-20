using Admin_Dashboard.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Admin_Dashboard.Service
{
    public class ProductService
    {
        private readonly IHttpClientFactory _factory;

        public ProductService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<DataResponseDTO<ProductGetDTO>?> LoadProduct(int currentPage ,bool status)
        {
            var client = _factory.CreateClient("AuthHttpClient");

            return await client.GetFromJsonAsync<DataResponseDTO<ProductGetDTO>>($"api/product/?currentPage={currentPage}&status={status}");

        }

        public async Task<Response> AddProduct(ProductAddDTO dto , IBrowserFile selectedFile)
        {

            if (selectedFile == null)
            {
                return new Response
                {
                    Isuccess = false , 
                    Message = "Chưa chọn ảnh"
                   
                }; 

            }
            var client  = _factory.CreateClient("AuthHttpClient");

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Name), "Name");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Price.ToString()), "Price");
            content.Add(new StringContent(dto.Quantity.ToString()), "Quantity");
            content.Add(new StringContent(dto.BrandId.ToString()), "BrandId");
            content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");

            var stream = selectedFile.OpenReadStream(5 * 1024 * 1024); // Max 5MB
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(selectedFile.ContentType);
            content.Add(fileContent, "ImageFile", selectedFile.Name);

            var response = await client.PostAsync("api/product", content);

            var responseData = await response.Content.ReadFromJsonAsync<Response>();

            return responseData ?? new Response
            {
                Isuccess = false,
                Message = "Không thể đọc phản hồi từ server"
            };

        }

        public async Task<ProductGetDTO?> GetById(int id)
        {
            if (id == 0)
                return null;

            var client = _factory.CreateClient("AuthHttpClient");

            var product = await client.GetFromJsonAsync<ProductGetDTO>($"api/product/{id}");

            return product;
        }


        public async Task<Response> UpdateProduct(int id, ProductAddDTO dto, IBrowserFile? selectedFile)
        {
            if(id is 0)
            {
                return new Response
                {
                    Isuccess = false,
                    Message ="ID sản phẩm không hợp lệ"
                    
                }; 
            }

            var client = _factory.CreateClient("AuthHttpClient");

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Name), "Name");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Price.ToString()), "Price");
            content.Add(new StringContent(dto.Quantity.ToString()), "Quantity");
            content.Add(new StringContent(dto.BrandId.ToString()), "BrandId");
            content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");

            if(selectedFile is not null)
            {
                var stream = selectedFile.OpenReadStream(5 * 1024 * 1024); // Max 5MB
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(selectedFile.ContentType);
                content.Add(fileContent, "ImageFile", selectedFile.Name);
            }
           
            var response = await client.PutAsync($"api/product/{id}", content);

            var responseData = await response.Content.ReadFromJsonAsync<Response>();

            return responseData ?? new Response
            {
                Isuccess = false,
                Message = "Không thể đọc phản hồi từ server"
            };

        }


            public async Task<Response> UpdateProductStatus(int id , bool status) 
            {
                var client = _factory.CreateClient("AuthHttpClient") ;

                var response = await client.PatchAsJsonAsync($"api/product/{id}", status);

                if(response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        Isuccess = true,
                        Message = ""
                    };
                }

                {
                    var mesage = await response.Content.ReadFromJsonAsync<Response>();
                    return new Response
                    {
                        Isuccess = false,
                        Message =  mesage.Message ??  "Chỉnh sửa thất bại"   
                    }; 
                }

           

            }
    }
}
