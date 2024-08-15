namespace TowbyJobs.Contracts.Job {
public record JobResponse(
    int Id,
    int CompanyID,
    string Position,
    string Link,
    string Contact,
    string ApplicationStatus,
    DateTime DateOfApplication,
    DateTime LastModifiedDateTime
);
    
}