using SimpleClientServices.Models.Login;
using SimpleClientServices.Models.Pessoal;

namespace SimpleClientServices.Services.PessoalServices
{
    public interface IPessoalServices
    {
        public Task<PessoalResponse> TakePessoalON();
        //public Task<PessoalResponse> TakePessoalOFF();
        public Task<PessoalResponse> TakePessoa(int id);
        //public Task<PessoalResponse> SetPessoaOFF(int id);
        public Task<PessoalResponse> DeletePessoa(int id);
        public Task<PessoalResponse> SetPessoa(PessoalDetail request);
        //      public Task<PessoalResponse> MakePessoa(PessoaRequest request);
    };
}
