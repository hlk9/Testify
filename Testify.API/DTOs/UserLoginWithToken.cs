namespace Testify.API.DTOs
{
    public class UserLoginWithToken
    {
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Token { get; set; }
        public bool? IsLoginSucces { get; set; }
        public int?  LevelId {  get; set; }


    }
}
