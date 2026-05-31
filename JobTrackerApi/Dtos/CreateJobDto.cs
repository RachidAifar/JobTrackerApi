using System.ComponentModel.DataAnnotations;

namespace JobTrackerApi.Dtos
{
    public class CreateJobDto
    {
        [Required]
        [StringLength(100)]
        public string Company { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;
    }
}
