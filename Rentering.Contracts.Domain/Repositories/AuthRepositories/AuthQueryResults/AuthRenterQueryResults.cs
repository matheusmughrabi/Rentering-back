namespace Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults
{
    public class AuthRenterProfilesOfTheCurrentUserQueryResults
    {
        public AuthRenterProfilesOfTheCurrentUserQueryResults(int id)
        {
            Id = id;
        }

        public AuthRenterProfilesOfTheCurrentUserQueryResults()
        {

        }

        public int Id { get; set; }
    }
}
