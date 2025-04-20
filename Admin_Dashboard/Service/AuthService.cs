using Admin_Dashboard.DTOs;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace Admin_Dashboard.Service
{
    public class AuthService
    {

        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<LoginResponse> LoginAsync(loginDTO login)
        {
            var payload = new { login.UserName, login.Password };
            var response = await _http.PostAsJsonAsync("api/auth/admin/login", payload);
            var message = "";

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var token = doc.RootElement.GetProperty("token").GetString();
                var role = doc.RootElement.GetProperty("role").GetString();
                message = doc.RootElement.GetProperty("message").GetString();

                await _js.InvokeVoidAsync("localStorage.setItem", "token", token);
                await _js.InvokeVoidAsync("localStorage.setItem", "role", role);
                return new LoginResponse
                {
                    IsSuccess = true,
                    Message = message ?? "Đăng nhập thành công"
                };

            }

            return new LoginResponse
            {
                IsSuccess = false,
                Message = message ?? "Đăng nhập thất bại vui lòng thử lại"
            }; ;
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
            await _js.InvokeVoidAsync("localStorage.removeItem", "role");
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");
            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "token");
        }

        public async Task<string> GetRoleAsync()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "role");
        }
    }
}
