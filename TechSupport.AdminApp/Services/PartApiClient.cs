using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

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
			return await _http.GetFromJsonAsync<List<ComponentDto >>("http://localhost:3000/parts");
		}
		public async Task UpdateAsync(int id, ComponentDto dto)
		{
			string jsonstring = "{" + $"partName: {dto.PartName}, partDescription: {dto.PartDescription}, partVisible: {dto.PartVisible}" + "}";
			await _http.PatchAsJsonAsync($"http://localhost:3000/parts/{id}", jsonstring);
		}


		public async Task<bool> CreateAsync(ComponentDto dto)
		{
			var response = await _http.PostAsJsonAsync("http://localhost:3000/parts", dto);
			return response.IsSuccessStatusCode;
		}


		public async Task DeleteAsync(int id)
		{
			await _http.DeleteAsync($"http://localhost:3000/parts/{id}");
		}
	}
}
