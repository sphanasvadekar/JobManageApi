using JobManagementAPI.Data;
using JobManagementAPI.Model;
using JobManagementAPI.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobManagementAPI.Controllers
{
    //localhost:PortNumber/api/jobs
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public JobsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("CreateNewJob")]

        public IActionResult CreateJob([FromBody] CreateJobDto jobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                bool locationexist = false;
                bool departmentexist = false;
                var allDept = dbContext.Department.ToList();

                for (int i = 0; i < allDept.Count; i++)
                {
                    if (allDept[i].ID == jobDto.DepartmentId)
                    {
                        departmentexist = true;
                        break;
                    }
                }
                if (!departmentexist)
                    return BadRequest("Deparment doesnt exist");

                var allLoc = dbContext.Location.ToList();
                for (int i = 0; i < allLoc.Count; i++)
                {
                    if (allLoc[i].ID == jobDto.LocationId)
                    {
                        locationexist = true;
                        break;
                    }
                }

                if (!locationexist)
                    return BadRequest("Location doesnt exist");

                var job = new Job
                {
                    Title = jobDto.Title,
                    Description = jobDto.Description,
                    LocationId = jobDto.LocationId,
                    DepartmentId = jobDto.DepartmentId,
                    ClosingDate = jobDto.ClosingDate,
                    Code = GenerateJobCode()
                };

                // Add the job to the database
                dbContext.Jobs.Add(job);
                dbContext.SaveChanges();

                // Return 
                return Ok(job);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        //Get https://localhost:portnumber/api/GetJobById/{id}
        [HttpGet("GetJobById/{id}")]
        //[Route("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var job = dbContext.Jobs.Find(id);
                if (job == null)
                {
                    return NotFound();
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpGet("GetAllJobsList")]
        public IActionResult GetAllJobs()
        {
            try
            {
                var allJobs = dbContext.Jobs
                                        .Include(j => j.Location)
                                        .Include(j => j.Department)
                                        .ToList();

                return Ok(allJobs);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        //Get https://localhost:portnumber/api/UpdateJob/{id}
        [HttpPut("UpdateJob/{id}")]
        public IActionResult UpdateJob(int id, [FromBody] CreateJobDto jobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Get department by id
                var job = dbContext.Jobs.FirstOrDefault(d => d.Id == id);

                // If not found, return 404 Not Found
                if (job == null)
                {
                    return NotFound();
                }



                bool locationexist = false;
                bool departmentexist = false;
                var allDept = dbContext.Department.ToList();

                for (int i = 0; i < allDept.Count; i++)
                {
                    if (allDept[i].ID == jobDto.DepartmentId)
                    {
                        departmentexist = true;
                        break;
                    }
                }
                if (!departmentexist)
                    return BadRequest("Deparment doesnt exist");

                var allLoc = dbContext.Location.ToList();
                for (int i = 0; i < allLoc.Count; i++)
                {
                    if (allLoc[i].ID == jobDto.LocationId)
                    {
                        locationexist = true;
                        break;
                    }
                }

                if (!locationexist)
                    return BadRequest("Location doesnt exist");


                // Update department properties
                job.Title = jobDto.Title;
                job.Description = jobDto.Description;
                job.LocationId = jobDto.LocationId;
                job.DepartmentId = jobDto.DepartmentId;
                job.ClosingDate = jobDto.ClosingDate;

               

                // Saving changes to the database
                dbContext.SaveChanges();

                // Return  updated job
                return Ok(job);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }




        private string GenerateJobCode()
        {
            // Get the count of existing jobs
            var jobCount = dbContext.Jobs.Count();

            // Increment the count and format the code
            var nextJobCode = $"JOB-{jobCount + 1:D2}";

            return nextJobCode;
        }

        [HttpPost("GetJoblist")]
        public IActionResult GetJobs([FromBody] JobListRequestDto requestDto)
        {
            try
            {
                // Query jobs from the database based on the provided criteria
                var query = dbContext.Jobs.AsQueryable();

                // Filter by locationId if provided
                if (requestDto.LocationId.HasValue && requestDto.LocationId > 0 )
                {
                    query = query.Where(j => j.LocationId == requestDto.LocationId);
                }

                // Filter by departmentId if provided
                if (requestDto.DepartmentId.HasValue && requestDto.DepartmentId > 0)
                {
                    query = query.Where(j => j.DepartmentId == requestDto.DepartmentId);
                }

                // Filter search string if provided
                if (!string.IsNullOrWhiteSpace(requestDto.Q))
                {
                    query = query.Where(j => j.Title.Contains(requestDto.Q));
                }

                // total count of jobs
                var total = query.Count();

                // Paginate the results
                var jobs = query
                    .OrderByDescending(j => j.PostedDate)
                    .Skip((requestDto.PageNo - 1) * requestDto.PageSize)
                    .Take(requestDto.PageSize)
                    .Select(j => new
                    {
                        j.Id,
                        j.Code,
                        j.Title,
                        Location = j.Location.Title,
                        Department = j.Department.Title,
                        j.PostedDate,
                        j.ClosingDate
                    })
                    .ToList();

                // Return the response
                return Ok(new { total, data = jobs });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error occurred while fetching jobs: {ex}");

                // Return an error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}
