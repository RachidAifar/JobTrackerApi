using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobTrackerApi.Data;
using JobTrackerApi.Models;

namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        //GET: api/jobs
        [HttpGet]
        public ActionResult<List<Job>> GetAllJobs()
        {
            return Ok(JobRepository.Jobs);
        }

        //Post : api/job
        [HttpPost]
        public ActionResult<Job> CreateJob(Job newJob)
        {
            newJob.Id = JobRepository.Jobs.Max(i => i.Id) + 1;
            JobRepository.Jobs.Add(newJob);
            return CreatedAtAction(nameof(GetAllJobs), new { id = newJob.Id }, newJob);
        }

        //Put : api/job/id
        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, Job updateJob)
        {
            var job = JobRepository.Jobs.FirstOrDefault(i => i.Id == id);

            if (job == null)
                return NotFound();

            job.Company = updateJob.Company;
            job.Position = updateJob.Position;
            job.Status = updateJob.Status;
            job.AppliedDate = updateJob.AppliedDate;


            return NoContent();
        }



    }
}
