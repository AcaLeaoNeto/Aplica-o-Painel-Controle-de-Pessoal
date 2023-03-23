using SimpleClientServices.Models.Base;

namespace SimpleClientServices.Models.Pessoal
{
    public class PessoalResponse : BaseResponse 
	{
        public List<PessoalDetail> ResponseObject { get; set; }
    }
}
