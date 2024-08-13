namespace TowbyJobs.Services.Jobs
{
    public record struct UpsertedJobResult
    {
        public bool IsNewlyCreated;

        public UpsertedJobResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
