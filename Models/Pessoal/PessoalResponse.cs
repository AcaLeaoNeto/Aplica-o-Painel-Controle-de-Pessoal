using SimpleClientServices.Models.Base;

namespace SimpleClientServices.Models.Pessoal
{
    public class PessoalResponse : BaseResponse 
	{
        public List<Pessoal> ResponseObject { get; set; }
    }
}
