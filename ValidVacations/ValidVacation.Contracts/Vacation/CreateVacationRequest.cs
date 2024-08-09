namespace ValidVacation.Contracts.Vacation
{
    public record CreateVacationRequest(
        string title,
        string description,
        DateTime startDate,
        DateTime endDate,
        uint length,
        uint price
        );
}

