using SimpleClientServices.Models.Login;
using SimpleClientServices.Models.Pessoal;
using SimpleClientServices.Services.ApiBaseSets;
using SimpleClientServices.Services.LoginServices;
using System.Text.Json;

namespace SimpleClientServices.Services.PessoalServices
{
    public class PessoalServices : BaseSets<PessoalResponse>, IPessoalServices 
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoginServices _loginServices;


        public PessoalServices(HttpClient httpClient,
                                            IHttpContextAccessor httpContextAccessor,
                                            ILoginServices loginServices) : base (httpClient, httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _loginServices = loginServices;
        }

        public async Task<PessoalResponse> TakePessoalON()
        {
            
			if (_httpContextAccessor.HttpContext.Request.Cookies["TKA"] is null)
				return PessoaFail();


            var requestM = RequestApiSet("Get", "https://localhost:7005/api/Usuario", null);

			requestM =  BearerSet(requestM, _httpContextAccessor.HttpContext.Request.Cookies["TKA"]);

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
