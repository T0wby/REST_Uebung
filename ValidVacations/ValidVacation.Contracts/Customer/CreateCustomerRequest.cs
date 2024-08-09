namespace ValidVacation.Contracts.Customer
{
    public record CreateCustomerRequest(
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

