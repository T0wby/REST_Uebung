namespace TowbyJobs.Services.States
{
    public record struct UpsertedStateResult
    {
        public bool IsNewlyCreated;

        public UpsertedStateResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
