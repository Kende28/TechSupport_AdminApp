using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;
using System.Net.Http.Json;

namespace TechSupport.AdminApp.Services
{
	public class ComponentApiClient
	{
		private readonly HttpClient _http;

		public ComponentApiClient()
		{
			_http = new HttpClient
			{
				BaseAddress = new Uri(AppConfig.ApiBaseUrl)
			};
		}

		public async Task<List<ComponentDto>> GetAllAsync()
		{
			return await _http.GetFromJsonAsync<List<ComponentDto >>("components");
		}
		public async Task UpdateAsync(int id, ComponentDto dto)
		{
			await _http.PutAsJsonAsync($"components/{id}", dto);
		}


		public async Task<bool> CreateAsync(ComponentDto dto)
		{
			var response = await _http.PostAsJsonAsync("components", dto);
			return response.IsSuccessStatusCode;
		}


		public async Task DeleteAsync(int id)
		{
			await _http.DeleteAsync($"components/{id}");
		}
	}
}
