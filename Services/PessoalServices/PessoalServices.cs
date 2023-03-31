using SimpleClientServices.Models.Base;
using SimpleClientServices.Models.Pessoal;
using SimpleClientServices.Services.ApiBaseSets;
using SimpleClientServices.Services.LoginServices;
using System.Text.Json;

namespace SimpleClientServices.Services.PessoalServices
{
    public class PessoalServices : BaseSets<PessoalResponse>, IPessoalServices 
    {
        private readonly ILoginServices _loginServices;

		public PessoalServices(HttpClient httpClient,
                                            IHttpContextAccessor httpContextAccessor, IConfiguration config,
                                            ILoginServices loginServices) : base (httpClient, httpContextAccessor, config)
        {
            _loginServices = loginServices;
		}



        public async Task<PessoalResponse> TakePessoalON()
        {
			return await PessoalEndPointSet("Get", "/api/Usuario");
        }



		public async Task<PessoalResponse> TakePessoalOFF()
		{
			return await PessoalEndPointSet("Get", "/api/Usuario/Desativados");
		}



		public async Task<PessoalResponse> TakePessoa(int id)
		{
			return await PessoalEndPointSet("Get", "/api/Usuario/", id);
		}



		public async Task<PessoalResponse> SetPessoaOFF(int id)
		{
			return await PessoalEndPointSet("Patch", "/api/Usuario/Desativar/", id);
		}



		public async Task<PessoalResponse> DeletePessoa(int id)
		{
			return await PessoalEndPointSet("Delete", "/api/Usuario/", id);
		}



		public async Task<PessoalResponse> SetPessoa(PessoalDetail request)
		{
			return await PessoalEndPointSet("Put", "/api/Usuario", request);
		}



		public async Task<PessoalResponse> MakePessoa(PessoaRequest request)
		{
			return await PessoalEndPointSet("Post", "/api/Usuario", request);
		}


		private async Task<PessoalResponse> PessoalEndPointSet(string method, string endCall, object content = null)
		{

			if (Cookies["TKA"] is null || Cookies["TKR"] is null)
				return new PessoalResponse().NewPessoa(401);


			var requestM = content is int ? RequestApiSet(method, host+endCall+content, null)  :
															RequestApiSet(method, host+endCall, content);

			requestM = BearerSet(requestM, Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if (!response.IsSuccessStatusCode)
				return await ReturnFail(response);


			var pessoa = DesJson(response);

			return pessoa.Result;
		}


		private async Task<PessoalResponse> FailResponse()
        {
			//var result = await _loginServices.ExpToken();
			if (await _loginServices.ExpToken())
				return null;


			_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKR");
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKA");
				
            return new PessoalResponse().NewPessoa(401);
		}

        private async Task<PessoalResponse> ReturnFail(HttpResponseMessage response)
        {
			if ((int)response.StatusCode == 401)
				return await FailResponse();
			

			var pessoa = DesJson(response).Result;
			return new PessoalResponse().NewPessoa(pessoa.StatusCode, pessoa.ResponseMessage); 
			
		}
	}
}
