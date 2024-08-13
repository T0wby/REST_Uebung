namespace TowbyJobs.Services.Companys
{
    public record struct UpsertedCompanyResult
    {
        public bool IsNewlyCreated;

        public UpsertedCompanyResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
