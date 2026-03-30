using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;

namespace TechSupport.AdminApp.Services
{
	public class PartApiClient
	{
		private readonly HttpClient _http;

		public PartApiClient()
		{
			_http = new HttpClient
			{
				BaseAddress = new Uri(AppConfig.ApiBaseUrl)
			};
		}

		public async Task<List<ComponentDto>> GetAllAsync()
		{
			var items = await _http.GetFromJsonAsync<List<ComponentDto>>("parts");
			return items ?? new List<ComponentDto>();
		}

		public async Task<ComponentDto?> GetByIdAsync(int id)
		{
			return await _http.GetFromJsonAsync<ComponentDto>($"parts/{id}");
		}

		public async Task<bool> CreateAsync(ComponentDto dto)
		{
			var response = await _http.PostAsJsonAsync("parts", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateAsync(int id, ComponentDto dto)
		{
			var response = await _http.PutAsJsonAsync($"parts/{id}", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var response = await _http.DeleteAsync($"parts/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}
