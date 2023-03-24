using SimpleClientServices.Models.Base;

namespace SimpleClientServices.Models.Pessoal
{
    public class PessoalResponse : BaseResponse
	{
        public List<PessoalDetail> ResponseObject { get; set; }

        public PessoalResponse NewPessoa(int status = 0, List<string> menssage = null, List<PessoalDetail> obj = null)
        {
            var pessoa = new PessoalResponse();
            pessoa.StatusCode = status;
            pessoa.ResponseMessage = menssage is null ? new List<string>() : menssage;
            pessoa.ResponseObject = obj is null ? new List<PessoalDetail>() : obj; 
            return pessoa;
        }
    }
}
