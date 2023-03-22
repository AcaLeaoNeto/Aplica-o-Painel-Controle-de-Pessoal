using SimpleClientServices.Models.Login;

namespace SimpleClientServices.Services.LoginServices
{
	public interface ILoginServices
	{
		Task<bool> Login(Login logRequest);
		Task<bool> ExpToken();
	}
}
