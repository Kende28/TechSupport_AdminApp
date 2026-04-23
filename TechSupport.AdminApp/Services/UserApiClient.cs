using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;

namespace TechSupport.AdminApp.Services
{
    // Backend felhasználói API kliense
    public class UserApiClient
    {
        // HTTP kliens backend kérésekhez
        private readonly HttpClient _http;

        // Konstruktor: HTTP kliens beállítása
        public UserApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.ApiBaseUrl)
            };
        }

        // Bejelentkezés a backendhez
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            // POST kérés az auth/login végpontra
            var response = await _http.PostAsJsonAsync("auth/login", dto);
            response.EnsureSuccessStatusCode();
            var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            return loginResult ?? throw new InvalidOperationException("Failed to deserialize login response.");
        }
    }
}
