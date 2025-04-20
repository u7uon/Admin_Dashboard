using Admin_Dashboard.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace Admin_Dashboard.Service
{
    public class CategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AuthHttpClient");
        }

        public async Task<DataResponseDTO<CategoryGetDTO>> GetPagedCategoriesAsync(int currentPage)
        {
            var response = await _client.GetFromJsonAsync<DataResponseDTO<CategoryGetDTO>>($"api/category?currentPage={currentPage}");
            return response;
        }

        public async Task<CategoryGetDTO> GetCategoryByIdAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<CategoryGetDTO>($"api/category/{id}");
            return response;
        }

        public async Task<Response> AddCategoryAsync(CategoryAddDTO dto)
        {
            try
            {
                var httpResponse = await _client.PostAsJsonAsync("api/category", dto);
                var responseText = await httpResponse.Content.ReadAsStringAsync();


                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                var message = root.TryGetProperty("message", out var messageElement)
                                ? messageElement.GetString()
                                : null;

                return new Response
                {
                    Isuccess = httpResponse.IsSuccessStatusCode,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Isuccess = false,
                    Message = $"Lỗi xảy ra: {ex.Message}"
                };
            }
        }



        public async Task<Response> UpdateCategoryAsync(CategoryAddDTO dto)
        {
            try
            {
                var httpResponse = await _client.PutAsJsonAsync($"api/category/{dto.Id}", dto);
                var responseText = await httpResponse.Content.ReadAsStringAsync();


                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                var message = root.TryGetProperty("Message", out var messageElement)
                                ? messageElement.GetString()
                                : null;

                return new Response
                {
                    Isuccess = httpResponse.IsSuccessStatusCode,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Isuccess = false,
                    Message = $"Lỗi xảy ra: {ex.Message}"
                };
            }
        }
    }
}
