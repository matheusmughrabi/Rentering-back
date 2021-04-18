namespace Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults
{
    public class AuthTenantProfilesOfTheCurrentUserQueryResults
    {
        public AuthTenantProfilesOfTheCurrentUserQueryResults()
        {

        }

        public AuthTenantProfilesOfTheCurrentUserQueryResults(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
