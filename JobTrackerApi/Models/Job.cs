namespace JobTrackerApi.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Company { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string Status { get; set; } = "Applied";

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }

        public User? CreatedByUser { get; set; }
    }
}
