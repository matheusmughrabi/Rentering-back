namespace Rentering.WebAPI.Authorization.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(int id, string username, string token)
        {
            Id = id;
            Username = username;
            Token = token;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Token { get; private set; }
    }
}
