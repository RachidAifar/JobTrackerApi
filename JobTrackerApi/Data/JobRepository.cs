using JobTrackerApi.Models;

namespace JobTrackerApi.Data
{
    public class JobRepository
    {
        public static List<Job> Jobs = new List<Job>()
        {
        new Job {Id = 1, Company = "Google", Position = "Backend Dev" },
        new Job {Id = 2, Company = "Microsoft", Position = "Cloud Engineer" },
        };
    }
}
