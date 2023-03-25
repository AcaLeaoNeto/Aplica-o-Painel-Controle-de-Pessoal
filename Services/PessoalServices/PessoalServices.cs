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

			if (Cookies["TKA"] is null || Cookies["TKR"] is null)
				return new PessoalResponse().NewPessoa(401);
            
				
            var requestM = RequestApiSet("Get", $"{host}/api/Usuario", null);

			requestM =  BearerSet(requestM, Cookies["TKA"]);

            var response = await _httpClient.SendAsync(requestM);

			if (!response.IsSuccessStatusCode)
                return await ReturnFail(response);

            var pessoa = DesJson(response);
			
            return pessoa.Result;

        }



		//public async Task<PessoalResponse> TakePessoalOFF()
		//{

		//	if (Cookies["TKA"] is null || Cookies["TKR"] is null)
		//		return new PessoalResponse(401);


		//	var requestM = RequestApiSet("Get", $"{host}/api/Usuario/Desativados", null);

		//	requestM = BearerSet(requestM, Cookies["TKA"]);

		//	var response = await _httpClient.SendAsync(requestM);

		//	if (!response.IsSuccessStatusCode)
		//		return await ReturnFail(response);


		//	var pessoa = DesJson(response);

		//	return pessoa.Result;

		//}



		public async Task<PessoalResponse> TakePessoa(int id)
		{

			if (Cookies["TKA"] is null || Cookies["TKR"] is null)
				return new PessoalResponse().NewPessoa(401);


			var requestM = RequestApiSet("Get", $"{host}/api/Usuario/{id}", null);

			requestM = BearerSet(requestM, Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if (!response.IsSuccessStatusCode)
				return await ReturnFail(response);

			var pessoa = DesJson(response).Result;

			return pessoa;

		}



		//public async Task<PessoalResponse> SetPessoaOFF(int id)
		//{

		//	if (Cookies["TKA"] is null || Cookies["TKR"] is null)
		//		return new PessoalResponse(401);


		//	var requestM = RequestApiSet("Get", $"{host}/api/Usuario/Desativar/{id}", null);

		//	requestM = BearerSet(requestM, Cookies["TKA"]);

		//	var response = await _httpClient.SendAsync(requestM);

		//	if (!response.IsSuccessStatusCode)
		//		return await ReturnFail(response);


		//	var pessoa = DesJson(response);

		//	return pessoa.Result;

		//}



		public async Task<PessoalResponse> DeletePessoa(int id)
		{

			if (Cookies["TKA"] is null || Cookies["TKR"] is null)
                return new PessoalResponse().NewPessoa(401);


            var requestM = RequestApiSet("Delete", $"{host}/api/Usuario/{id}", null);

			requestM = BearerSet(requestM, Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if (!response.IsSuccessStatusCode)
				return await ReturnFail(response);


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> SetPessoa(PessoalDetail request)
		{

			if (Cookies["TKA"] is null || Cookies["TKR"] is null)
				return new PessoalResponse().NewPessoa(401);


			var requestM = RequestApiSet("Put", $"{host}/api/Usuario", request);

			requestM = BearerSet(requestM, Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if (!response.IsSuccessStatusCode)
				return await ReturnFail(response);


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		//public async Task<PessoalResponse> MakePessoa(PessoaRequest request)
		//{

		//	if (Cookies["TKA"] is null || Cookies["TKR"] is null)
		//		return new PessoalResponse(401);


		//	var requestM = RequestApiSet("Post", $"{host}/api/Usuario", request);

		//	requestM = BearerSet(requestM, Cookies["TKA"]);

		//	var response = await _httpClient.SendAsync(requestM);

		//	if (!response.IsSuccessStatusCode)
		//              return await ReturnFail(response);


		//	var pessoa = DesJson(response);

		//	return pessoa.Result;

		//}



		private async Task<PessoalResponse> FailResponse()
        {
			var result = await _loginServices.ExpToken();
			if (!result)
			{
				_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKR");
				_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKA");
				
                return new PessoalResponse().NewPessoa(401);
            }

			return null;
		}

        private async Task<PessoalResponse> ReturnFail(HttpResponseMessage response)
        {
			if ((int)response.StatusCode == 401)
				return await FailResponse();
			else
			{ 
				var pessoa = DesJson(response).Result;
				return new PessoalResponse().NewPessoa(pessoa.StatusCode, pessoa.ResponseMessage); 
			}
		}
	}
}
