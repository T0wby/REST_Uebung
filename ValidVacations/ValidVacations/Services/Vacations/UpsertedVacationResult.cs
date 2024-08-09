namespace ValidVacations.Services.Vacations
{
    public record struct UpsertedVacationResult
    {
        public bool IsNewlyCreated;

        public UpsertedVacationResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
