using Microsoft.AspNetCore.Mvc;
using SimpleClientServices.Models.Base;
using SimpleClientServices.Models.Login;
using SimpleClientServices.Services.LoginServices;

namespace SimpleClientServices.Controllers
{		
	public class LoginController : Controller
	{

		private readonly ILoginServices _loginServices;


		public LoginController(ILoginServices loginServices)
		{
			_loginServices = loginServices;
		}


		public IActionResult Index()
		{
			return View();
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(Login request)
		{
			if(await _loginServices.Login(request))
				return RedirectToAction("Index", "Pessoal");

			TempData["Message"] = "Usuario ou senha Incorreto";
			return RedirectToAction("Index");
		}

	}
}
