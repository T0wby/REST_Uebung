namespace TowbyJobs.Contracts.Country {
public record CountryResponse(
    int Id,
    string Name, 
    string Code,
    DateTime LastModifiedDateTime
);
    
}