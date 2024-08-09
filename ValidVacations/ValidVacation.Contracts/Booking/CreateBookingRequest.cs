namespace ValidVacation.Contracts.Booking
{
    public record CreateBookingRequest(
        string title,
        DateTime createdDate,
        DateTime updateDate,
        Guid customerId,
        Guid vacationId);
}
