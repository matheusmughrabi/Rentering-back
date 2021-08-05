using Rentering.Accounts.Domain.Enums;

namespace Rentering.WebAPI.Security.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(int id, string username, string token, e_Role role)
        {
            Id = id;
            Username = username;
            Token = token;
            Role = role;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public e_Role Role { get; set; }
        public string Token { get; private set; }
    }
}

