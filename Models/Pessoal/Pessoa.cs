using SimpleClientServices.Models.Base;

namespace SimpleClientServices.Models.Pessoal
{
    public class Pessoa : BaseResponse 
    { 
        public PessoalDetail ResponseObject { get; set; }
    }
}
