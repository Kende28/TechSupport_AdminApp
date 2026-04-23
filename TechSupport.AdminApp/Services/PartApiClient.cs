using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;

namespace TechSupport.AdminApp.Services
{
    // Backend alkatrész API kliense
    public class PartApiClient
    {
        // HTTP kliens backend kérésekhez
        private readonly HttpClient _http;

        // Konstruktor: HTTP kliens beállítása
        public PartApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.ApiBaseUrl)
            };
        }

        // Bearer token hozzáadása az Authorization fejléchez
        private void SetAuthorizationHeader()
        {
            if (string.IsNullOrWhiteSpace(AppConfig.BearerToken))
            {
                _http.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.BearerToken);
            }
        }

        // Összes alkatrész lehívása
        public async Task<List<ComponentDto>> GetAllAsync()
        {
            // Ha szükséges, adhatsz meg Bearer tokent
            SetAuthorizationHeader();
            var items = await _http.GetFromJsonAsync<List<ComponentDto>>("parts");
            return items ?? new List<ComponentDto>();
        }

        // Egy alkatrész lehívása ID alapján
        public async Task<ComponentDto?> GetByIdAsync(int id)
        {
            SetAuthorizationHeader();
            return await _http.GetFromJsonAsync<ComponentDto>($"parts/{id}");
        }

        // Új alkatrész létrehozása (Bearer token szükséges)
        public async Task<bool> CreateAsync(ComponentDto dto)
        {
            SetAuthorizationHeader();
            var response = await _http.PostAsJsonAsync("parts", dto);
            return response.IsSuccessStatusCode;
        }

        // Alkatrész módosítása (Bearer token szükséges)
        public async Task<bool> UpdateAsync(int id, ComponentDto dto)
        {
            SetAuthorizationHeader();
            var response = await _http.PutAsJsonAsync($"parts/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // Alkatrész törlése (Bearer token szükséges)
        public async Task<bool> DeleteAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _http.DeleteAsync($"parts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
