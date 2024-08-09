namespace ValidVacations.Services.Bookings
{
    public record struct UpsertedBookingResult
    {
        public bool IsNewlyCreated;

        public UpsertedBookingResult(bool isNewlyCreated)
        {
            IsNewlyCreated = isNewlyCreated;
        }
    }
}
