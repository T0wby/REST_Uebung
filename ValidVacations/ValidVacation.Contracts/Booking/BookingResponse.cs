namespace ValidVacation.Contracts.Booking
{
    public record BookingResponse(
        Guid id, 
        string title, 
        DateTime createdDate, 
        DateTime updateDate, 
        Guid customerId,
        Guid vacationId);
}
