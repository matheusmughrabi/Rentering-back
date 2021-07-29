using Rentering.Accounts.Domain.SafeEnums;

namespace Rentering.WebAPI.Security.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(int id, string username, string token, RoleType role)
        {
            Id = id;
            Username = username;
            Token = token;
            Role = RoleType.FromValue<RoleType>(role.Value);
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public RoleType Role { get; set; }
        public string Token { get; private set; }
    }
}
