namespace TowbyJobs.Contracts.Company {
public record CompanyResponse(
    int Id,
    string Name, 
    string Email, 
    int HouseNumber, 
    string Street, 
    int CityId,
    int StateId, 
    int CountryId, 
    string Phone,
    DateTime LastModifiedDateTime
);
    
}