using SimpleClientServices.Models.Login;

namespace SimpleClientServices.Services.ApiBaseSets
{
	public interface IBaseSets<T> where T : class
	{
		HttpRequestMessage RequestApiSet(string RequestMethod, string TargetUri, object RequestContent);
		Task<T> RefreshAcess();
		HttpRequestMessage BearerSet(HttpRequestMessage request, string key);
		Task<T> DesJson(HttpResponseMessage response);
		void CookieSet(string CookieName, string CookieContent, int CookieExpTime);
	}
}
