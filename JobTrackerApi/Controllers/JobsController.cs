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
    }
}
