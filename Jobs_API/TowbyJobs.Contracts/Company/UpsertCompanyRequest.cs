namespace TowbyJobs.Contracts.Company {
public record UpsertCompanyRequest(
    string Name,
    string Email,
    string Phone,
    int HouseNumber,
    string Street,
    int City_Id,
    int State_Id,
    int Country_Id
);
    
}