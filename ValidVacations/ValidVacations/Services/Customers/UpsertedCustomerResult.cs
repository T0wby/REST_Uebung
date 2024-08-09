namespace ValidVacations.Services.Customers
{
    public record struct UpsertedCustomerResult
    {
        public bool IsNewlyCreated;

        public UpsertedCustomerResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
