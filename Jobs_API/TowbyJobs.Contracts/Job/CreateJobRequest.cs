namespace TowbyJobs.Contracts.Job {
public record CreateJobRequest(
    int CompanyID,
    string CompanyName,
    string Position,
    string Link,
    string Contact,
    string ApplicationStatus,
    DateTime DateOfApplication
);
    
}
