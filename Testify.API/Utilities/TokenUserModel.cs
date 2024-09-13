namespace Testify.API.Utilities
{
    public class TokenUserModel
    {
        public Guid UserId { get; set; }
        public DateTime Expried {  get; set; }
        public string Token { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
