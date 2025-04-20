using Admin_Dashboard.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace Admin_Dashboard.Service
{
    public class BrandService
    {
        private readonly HttpClient _client;

        public BrandService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AuthHttpClient");
        }

        public async Task<DataResponseDTO<BrandGetDTO>> GetPagedCategoriesAsync(int currentPage)
        {
            var response = await _client.GetFromJsonAsync<DataResponseDTO<BrandGetDTO>>($"api/brand?currentPage={currentPage}");
            return response;
        }

        public async Task<BrandGetDTO> GetBrandByIdAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<BrandGetDTO>($"api/brand/{id}");
            return response;
        }

        public async Task<Response> AddBrandAsync(BrandAddDTO dto)
        {
            try
            {
                var httpResponse = await _client.PostAsJsonAsync("api/brand", dto);
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



        public async Task<Response> UpdateBrandAsync(BrandAddDTO dto)
        {
            try
            {
                var httpResponse = await _client.PutAsJsonAsync($"api/brand/{dto.Id}", dto);
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
    }
}
