namespace ValidVacation.Contracts.Vacation
{
    public record VacationResponse(
        Guid? id,
        string title,
        string description,
        DateTime startDate,
        DateTime endDate,
        DateTime lastModifyDate,
        uint length,
        uint price
        );
}
