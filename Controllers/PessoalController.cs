using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SimpleClientServices.Models.Login;
using SimpleClientServices.Models.Pessoal;
using SimpleClientServices.Services.LoginServices;
using SimpleClientServices.Services.PessoalServices;
using System.Web;

namespace SimpleClientServices.Controllers
{
    public class PessoalController : Controller
    {
        private readonly IPessoalServices _pessoalServices;

        public PessoalController(IPessoalServices pessoalServices)
        {
            _pessoalServices = pessoalServices;
        }

        [HttpGet]
        public async Task<ActionResult<PessoalResponse>> Index()
        {
            var result = await _pessoalServices.TakePessoalON();
            return BaseReturn(result, "Index");
        }


        private ActionResult BaseReturn(PessoalResponse result, string callFunc)
        {
			if (result is null)
			{
				return RedirectToAction(callFunc);
			}
			else if (result.StatusCode == 401)
			{
				return RedirectToAction("Index", "Login");
			}
			return View(result);
		}
    }
}
