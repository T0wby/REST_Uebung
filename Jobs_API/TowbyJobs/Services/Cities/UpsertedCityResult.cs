namespace TowbyJobs.Services.Cities
{
    public record struct UpsertedCityResult
    {
        public bool IsNewlyCreated;

        public UpsertedCityResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
