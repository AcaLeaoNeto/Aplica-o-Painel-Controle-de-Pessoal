using SimpleClientServices.Models.Login;
using SimpleClientServices.Models.Pessoal;

namespace SimpleClientServices.Services.PessoalServices
{
    public interface IPessoalServices
    {
        public Task<PessoalResponse> TakePessoalON();

    };
}
