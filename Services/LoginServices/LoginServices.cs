using Microsoft.AspNetCore.Http;
using SimpleClientServices.Models.Login;
using SimpleClientServices.Services.ApiBaseSets;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace SimpleClientServices.Services.LoginServices
{
	public class LoginServices : BaseSets<LoginResponse> , ILoginServices
	{
		private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;


		public LoginServices(HttpClient httpClient,
										IHttpContextAccessor httpContextAccessor) 
										: base(httpClient, httpContextAccessor)
		{
			_httpClient = httpClient;
			_httpContextAccessor = httpContextAccessor;
		}



		public async Task<bool> Login(Login logRequest)
		{

			//var login = new Login();
			//login.Username = "Metro";
			//login.Password = "Test!234";

			var requestLog = RequestApiSet("Post", "https://localhost:7005/api/Login/login", logRequest);

			var responseLog = await _httpClient.SendAsync(requestLog);

			if(!responseLog.IsSuccessStatusCode)
				return false;

			var LogResponse = DesJson(responseLog).Result;

			CookieSet("TKA", LogResponse.ResponseObject.AcessToken, 10);
			CookieSet("TKR", LogResponse.ResponseObject.RefreshToken, 20);

			return true;
		}

		public async Task<bool> ExpToken()
		{
			var response = RefreshAcess().Result;

			if(response is null)
				return false;
			else
			{
				CookieSet("TKA", response.ResponseObject.AcessToken, 10);
				CookieSet("TKR", response.ResponseObject.RefreshToken, 20);
			}
			return true;
		}

	}
}
