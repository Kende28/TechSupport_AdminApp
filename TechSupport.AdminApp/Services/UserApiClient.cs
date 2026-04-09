using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechSupport.AdminApp.Models;

namespace TechSupport.AdminApp.Services
{
	public class UserApiClient
	{
		private readonly HttpClient _http;

		public UserApiClient()
		{
			_http = new HttpClient
			{
				BaseAddress = new Uri(AppConfig.ApiBaseUrl)

			};


		}

		public async Task<string> LoginAsync(LoginDto dto)
		{
			var response = await _http.PostAsJsonAsync("http://localhost:3000/auth/login", dto);
			return response.ToString();
		}
	}
}
