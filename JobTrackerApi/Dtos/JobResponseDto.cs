namespace JobTrackerApi.Dtos
{
    public class JobResponseDto
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }
    }
}
