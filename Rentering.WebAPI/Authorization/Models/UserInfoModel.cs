using Rentering.Accounts.Domain.Enums;

namespace Rentering.WebAPI.Authorization.Models
{
    public class UserInfoModel
    {
        public UserInfoModel(int id, string username, string token, e_Roles roles)
        {
            Id = id;
            Username = username;
            Token = token;

            switch (roles)
            {
                case e_Roles.RegularUser:
                    Role = "RegularUser";
                    break;
                case e_Roles.Admin:
                    Role = "Admin";
                    break;
                default:
                    break;
            }
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Role { get; set; }
        public string Token { get; private set; }
    }
}
