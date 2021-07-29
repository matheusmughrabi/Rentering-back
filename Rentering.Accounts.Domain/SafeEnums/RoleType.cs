using Rentering.Common.Shared.SafeEnums;

namespace Rentering.Accounts.Domain.SafeEnums
{
    public class RoleType : SafeEnumeration
    {
        public static readonly RoleType RegularUser = new RoleType(1, "RegularUser");
        public static readonly RoleType Admin = new RoleType(2, "Admin");

        public RoleType() 
        {
        }

        private RoleType(int value, string displayName) : base(value, displayName)
        {
        }
    }
}
