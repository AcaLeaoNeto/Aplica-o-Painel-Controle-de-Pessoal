using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SimpleClientServices.Models.Login;
using SimpleClientServices.Models.Pessoal;
using SimpleClientServices.Services.LoginServices;
using SimpleClientServices.Services.PessoalServices;
using System.Reflection;
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
		public async Task<ActionResult<PessoalResponse>> Detail(int Id)
		{
			var result = await _pessoalServices.TakePessoa(Id);
			return BaseReturn(result, "Detail");
		}

		[HttpGet]
        public async Task<ActionResult<PessoalResponse>> ConfirmDelete(int Id)
        {
            return await Detail(Id);
        }

        [HttpPost]
        public async Task<ActionResult<PessoalResponse>> Delete(PessoalResponse response)
        {
            var result = await _pessoalServices.DeletePessoa(response.ResponseObject[0].Id);

			var FeedReturn = BaseReturn(result, "Index");

            if(FeedReturn.ToString() == "Microsoft.AspNetCore.Mvc.ViewResult")
                return RedirectToAction("Index");

			TempData["Message"] = "Erro na Execução";
			return FeedReturn;
        }

        [HttpGet]
        public async Task<ActionResult<PessoalResponse>> Editar(int Id)
        {
            return await Detail(Id);
        }

        [HttpPost]
        public async Task<ActionResult<PessoalResponse>> PessoaEdit(PessoalResponse response)
        {
            var result = await _pessoalServices.SetPessoa(response.ResponseObject[0]);
            
            var FeedReturn = BaseReturn(result, "Index");

            if (FeedReturn.ToString() == "Microsoft.AspNetCore.Mvc.ViewResult")
                return RedirectToAction("Index");

            TempData["Message"] = "Erro na Execução";
            return FeedReturn;
        }

		[HttpGet]
		public ActionResult Cadastro()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult<PessoalResponse>> CadastrarPessoa(PessoaRequest response)
		{
			var result = await _pessoalServices.MakePessoa(response);

			var FeedReturn = BaseReturn(result, "Index");

			if (FeedReturn.ToString() == "Microsoft.AspNetCore.Mvc.ViewResult")
				return RedirectToAction("Index");

			TempData["Message"] = "Erro na Execução";
			return FeedReturn;
		}

		[HttpGet]
		public async Task<ActionResult<PessoalResponse>> Desativar(int Id)
		{
			return await Detail(Id);
		}

		[HttpPost]
		public async Task<ActionResult<PessoalResponse>> DesativarPessoa(PessoalResponse response)
		{
			var result = await _pessoalServices.SetPessoaOFF(response.ResponseObject[0].Id);

			var FeedReturn = BaseReturn(result, "Index");

			if (FeedReturn.ToString() == "Microsoft.AspNetCore.Mvc.ViewResult")
				return RedirectToAction("Index");

			TempData["Message"] = "Erro na Execução";
			return FeedReturn;
		}

        [HttpGet]
        public async Task<ActionResult<PessoalResponse>> Desativados()
        {
            var result = await _pessoalServices.TakePessoalOFF();
            return BaseReturn(result, "Desativados");
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
