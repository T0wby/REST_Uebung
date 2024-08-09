namespace ValidVacation.Contracts.Customer
{
    public record CustomerResponse(
        Guid? id,
        string firstName,
        string lastName,
        string email,
        int houseNumber,
        string street,
        string city,
        string areaCode,
        string state,
        string country,
        string phone
        );
}
