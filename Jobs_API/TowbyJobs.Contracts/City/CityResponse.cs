namespace TowbyJobs.Contracts.City {
public record CityResponse(
    int Id,
    string Name, 
    string AreaCode,
    DateTime LastModifiedDateTime
);
    
}