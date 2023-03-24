namespace SimpleClientServices.Models.Base
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public List<string> ResponseMessage { get; set; }
    }
}
