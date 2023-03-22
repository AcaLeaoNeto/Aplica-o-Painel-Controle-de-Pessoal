using Newtonsoft.Json;
using SimpleClientServices.Models.Login;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace SimpleClientServices.Services.ApiBaseSets
{
	public class BaseSets<T> : IBaseSets<T> where T : class
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly HttpClient _httpClient;
		private HttpClient httpClient;

		public BaseSets(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_httpClient = httpClient;
		}

		public HttpRequestMessage RequestApiSet(string RequestMethod, string TargetUri, object RequestContent)
		{
			StringContent contentObj = null;
			if (RequestContent is not null)
			{
				contentObj = new StringContent
					(//Converte Objeto Usuario em Json
						JsonConvert.SerializeObject(RequestContent),
						Encoding.UTF8,//Padrão Enconde
						"application/json"
					);
			}

			var request = new HttpRequestMessage
			{
				Method = new HttpMethod(RequestMethod),
				RequestUri = new Uri(TargetUri),
				Content = contentObj
			};

			return request;
		}



		public async Task<T> RefreshAcess()
		{
			var RefRequest =RequestApiSet("Post",
				"https://localhost:7005/api/Login/RefreshLogin",
				_httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			RefRequest = BearerSet(RefRequest, _httpContextAccessor.HttpContext.Request.Cookies["TKR"]);

			var response = await _httpClient.SendAsync(RefRequest);

			if (!response.IsSuccessStatusCode) return null;

			var responseClass = DesJson(response).Result;

			return responseClass;
		}


		public void CookieSet(string CookieName, string CookieContent, int CookieExpTime)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Append
			(
				CookieName,
				CookieContent,
				new CookieOptions
				{
					HttpOnly = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTime.Now.AddMinutes(CookieExpTime)
				}
			);
		}


		public HttpRequestMessage BearerSet(HttpRequestMessage request, string key)
		{
			request.Headers.TryAddWithoutValidation("Accept", "application/json");
			request.Headers.TryAddWithoutValidation("Authorization",
				$"Bearer {key}");

			return request;
		}



		public async Task<T> DesJson(HttpResponseMessage response)
		{
			JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

			var content = await response.Content.ReadAsStringAsync();//Converte Json para string
			var TObject = System.Text.Json.JsonSerializer.Deserialize<T>(content, options);

			return TObject;
		}

	}
}
