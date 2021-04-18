namespace Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults
{
    public class AuthGuarantorProfilesOfTheCurrentUserQueryResults
    {
        public AuthGuarantorProfilesOfTheCurrentUserQueryResults()
        {

        }

        public AuthGuarantorProfilesOfTheCurrentUserQueryResults(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
