using SimpleClientServices.Models.Pessoal;
using SimpleClientServices.Services.ApiBaseSets;
using SimpleClientServices.Services.LoginServices;

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
            
			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


            var requestM = RequestApiSet("Get", $"{host}/api/Usuario", null);

			requestM =  BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

            var response = await _httpClient.SendAsync(requestM);
            
            if ((int)response.StatusCode == 401)
                return await FailResponse();


            var pessoa = DesJson(response);

            return pessoa.Result;

        }



		public async Task<PessoalResponse> TakePessoalOFF()
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Get", $"{host}/api/Usuario/Desativados", null);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> TakePessoa(int id)
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Get", $"{host}/api/Usuario/{id}", null);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> SetPessoaOFF(int id)
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Get", $"{host}/api/Usuario/Desativar/{id}", null);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> DeletePessoa(int id)
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Delete", $"{host}/api/Usuario/{id}", null);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> SetPessoa(PessoaSetRequest request)
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Put", $"{host}/api/Usuario", request);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		public async Task<PessoalResponse> MakePessoa(PessoaRequest request)
		{

			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


			var requestM = RequestApiSet("Post", $"{host}/api/Usuario", request);

			requestM = BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

			var response = await _httpClient.SendAsync(requestM);

			if ((int)response.StatusCode == 401)
				return await FailResponse();


			var pessoa = DesJson(response);

			return pessoa.Result;

		}



		private async Task<PessoalResponse> FailResponse()
        {
			var result = await _loginServices.ExpToken();
			if (!result)
			{
				_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKR");
				_httpContextAccessor.HttpContext.Response.Cookies.Delete("TKA");
				return PessoaFail();
			}

			return null;
		}

        private PessoalResponse PessoaFail()
        {
			var needLogin = new PessoalResponse();
			needLogin.StatusCode = 401;
			return needLogin;
		}
    }
}
