using Microsoft.AspNetCore.Http;
using SimpleClientServices.Models.Login;
using SimpleClientServices.Services.ApiBaseSets;

namespace SimpleClientServices.Services.LoginServices
{
	public class LoginServices : BaseSets<LoginResponse> , ILoginServices
	{


		public LoginServices(HttpClient httpClient, IConfiguration config,
										IHttpContextAccessor httpContextAccessor)
										: base(httpClient, httpContextAccessor, config)
		{ }



		public async Task<bool> Login(Login logRequest)
		{
			//var login = new Login();
			//login.Username = "Metro";
			//login.Password = "Test!234";

			var requestLog = RequestApiSet("Post", $"{host}/api/Login/login", logRequest);

			var responseLog = await _httpClient.SendAsync(requestLog);

			if(!responseLog.IsSuccessStatusCode)
				return false;

			var LogResponse = DesJson(responseLog).Result;

			CreateCookieLogToken(LogResponse);

			return true;
		}



		public async Task<bool> ExpToken()
		{
			var response = RefreshAcess().Result;

			if(response is null)
				return false;
			else
				CreateCookieLogToken(response);
			

			return true;
		}

		private void CreateCookieLogToken(LoginResponse response)
		{
			CookieSet("TKA", response.ResponseObject.AcessToken, 10);
			CookieSet("TKR", response.ResponseObject.RefreshToken, 20);
		}
	}
}
