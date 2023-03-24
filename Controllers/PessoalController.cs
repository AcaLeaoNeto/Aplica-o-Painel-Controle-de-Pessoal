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

        [HttpGet]
        public async Task<ActionResult<PessoalResponse>> ConfirmDelete(int id)
        {
            var result = await _pessoalServices.TakePessoa(id);
            return BaseReturn(result, "ConfirmDelete");
        }

        [HttpPost]
        public async Task<ActionResult<PessoalResponse>> Delete(PessoalResponse reponse)
        {
            var result = await _pessoalServices.DeletePessoa(reponse.ResponseObject[0].Id) ;

            var FeedReturn = BaseReturn(result, "ConfirmDelete");

            if(FeedReturn.ToString() == "Microsoft.AspNetCore.Mvc.ViewResult")
                return RedirectToAction("Index");

            return FeedReturn;
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
