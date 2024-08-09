namespace ValidVacations.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
