namespace TowbyJobs.Services.Countries
{
    public record struct UpsertedCountryResult
    {
        public bool IsNewlyCreated;

        public UpsertedCountryResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
