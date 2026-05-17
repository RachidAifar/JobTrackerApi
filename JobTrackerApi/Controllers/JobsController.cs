using JobTrackerApi.Data;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using JobTrackerAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        //GET: api/jobs
        [HttpGet]
        public ActionResult<IEnumerable<JobResponseDto>> GetAllJobs()
        {
            var jobs = JobRepository.Jobs.Select(job => new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Position = job.Position,
                Status = job.Status,
                AppliedDate = job.AppliedDate
            }).ToList();

            return Ok(jobs);
        }

        //Post : api/job
        [HttpPost]
        public ActionResult<JobResponseDto> CreateJob(CreateJobDto dto)
        {
            var job = new Job
            {
                Id = JobRepository.Jobs.Count + 1,
                Company = dto.Company,
                Position = dto.Position,
                Status = "Applied",
                AppliedDate = DateTime.UtcNow
            };

            JobRepository.Jobs.Add(job);

            var response = new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Position = job.Position,
                Status = job.Status,
                AppliedDate = job.AppliedDate
            };

            return CreatedAtAction(nameof(GetJobs), new { id = job.Id }, response);
        }

        //Put : api/job/id
        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, UpdateJobDto dto)
        {
            var job = JobRepository.Jobs.FirstOrDefault(j => j.Id == id);

            if (job == null)
                return NotFound();

            job.Company = dto.Company;
            job.Position = dto.Position;
            job.Status = dto.Status;

            return NoContent();
        }

        //Delete : api/jobs/id
        [HttpDelete("{id}")]
        public ActionResult DeleteJob(int id)
        {
            var job = JobRepository.Jobs.FirstOrDefault(j => j.Id == id);

            if (job == null)
                return NotFound();

            JobRepository.Jobs.Remove(job);

            return NoContent();
        }










    }
}
