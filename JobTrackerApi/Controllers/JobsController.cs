using JobTrackerApi.Data;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;






namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobDbContext _context;
        public JobsController(JobDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("myjobs")]
        public async Task<IActionResult> GetMyJob()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var jobs = await _context.Jobs
                .Where(j => j.CreatedByUserId == userId)
                .ToListAsync();
            return Ok(jobs);
        }




        //GET: api/jobs
        [HttpGet]
        public ActionResult<IEnumerable<JobResponseDto>> GetJobs()
        {
            var jobs = _context.Jobs.ToList();

            var response = jobs.Select(job => new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Position = job.Position,
                Status = job.Status,
                AppliedDate = job.AppliedDate
            });

            return Ok(response);
        }

        // GET: api/jobs/5
        [HttpGet("{id}")]
        public ActionResult<JobResponseDto> GetJob(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);

            if (job == null)
                return NotFound();

            return Ok(new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Position = job.Position,
                Status = job.Status,
                AppliedDate = job.AppliedDate
            });
        }



        //Post : api/job
        [Authorize]
        [HttpPost]
        public ActionResult<JobResponseDto> CreateJob(CreateJobDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            var job = new Job
            {
                Company = dto.Company,
                Position = dto.Position,
                Status = "Applied",
                AppliedDate = DateTime.UtcNow,
                CreatedByUserId = userId
            };

            _context.Jobs.Add(job);
            _context.SaveChanges();

            return Ok(new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Position = job.Position,
                Status = job.Status,
                AppliedDate = job.AppliedDate
            });
        }

        //Put : api/job/id
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, UpdateJobDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var job = _context.Jobs.FirstOrDefault(j =>
                j.Id == id &&
                j.CreatedByUserId == userId);

            if (job == null)
                return NotFound();

            job.Company = dto.Company;
            job.Position = dto.Position;
            job.Status = dto.Status;

            _context.SaveChanges();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteJob(int id)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var job = _context.Jobs.FirstOrDefault(j =>
                j.Id == id &&
                j.CreatedByUserId == userId);

            if (job == null)
                return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return NoContent();
        }

        //[HttpPut("{id}")]
        //public ActionResult UpdateJob(int id, UpdateJobDto dto)
        //{
        //    var job = _context.Jobs.FirstOrDefault(j => j.Id == id);

        //    if (job == null)
        //        return NotFound();

        //    job.Company = dto.Company;
        //    job.Position = dto.Position;
        //    job.Status = dto.Status;

        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //[Authorize]
        //[HttpDelete("{id}")]
        //public ActionResult DeleteJob(int id)
        //{
        //    var job = _context.Jobs.FirstOrDefault(j => j.Id == id);

        //    if (job == null)
        //        return NotFound();

        //    _context.Jobs.Remove(job);
        //    _context.SaveChanges();

        //    return NoContent();
        //}
    }
}
