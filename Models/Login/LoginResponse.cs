using SimpleClientServices.Models.Base;

namespace SimpleClientServices.Models.Login
{
    public class LoginResponse : BaseResponse
	{
        public LogReturn ResponseObject { get; set;}
    }
}
