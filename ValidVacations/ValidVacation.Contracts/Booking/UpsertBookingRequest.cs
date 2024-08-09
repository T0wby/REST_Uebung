namespace ValidVacation.Contracts.Booking
{
    public record UpsertBookingRequest(
        string title,
        DateTime createdDate,
        DateTime updateDate,
        Guid customerId,
        Guid vacationId);
}
