namespace ValidVacation.Contracts.Vacation
{
	public record UpsertVacationRequest(
		string title,
		string description,
		DateTime startDate,
		DateTime endDate,
		uint length,
		uint price
		);
}