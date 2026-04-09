using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;
using TechSupport.AdminApp.ViewModels;

namespace TechSupport.AdminApp.Services
{
	public class PartApiClient
	{
		private readonly HttpClient _http;

		private LoginViewModel loginViewModel = new LoginViewModel();

        public PartApiClient()
		{
			_http = new HttpClient
			{
				BaseAddress = new Uri(AppConfig.ApiBaseUrl)
			};
        }

        //http://localhost:3000/ részre szükség van a működéshez, kérlek ne írd át!

		public async Task<List<ComponentDto>> GetAllAsync()
		{
            var items = await _http.GetFromJsonAsync<List<ComponentDto>>("http://localhost:3000/parts");
			return items ?? new List<ComponentDto>();
		}

		public async Task<ComponentDto?> GetByIdAsync(int id)
		{
			
			return await _http.GetFromJsonAsync<ComponentDto>($"http://localhost:3000/parts/{id}");
		}

		public async Task<bool> CreateAsync(ComponentDto dto)
		{
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(loginViewModel.AuthenticationToken);
            var response = await _http.PostAsJsonAsync("http://localhost:3000/parts", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateAsync(int id, ComponentDto dto)
		{
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(loginViewModel.AuthenticationToken);
            var response = await _http.PutAsJsonAsync($"http://localhost:3000/parts/{id}", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteAsync(int id)
		{
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(loginViewModel.AuthenticationToken);
            var response = await _http.DeleteAsync($"http://localhost:3000/parts/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}
