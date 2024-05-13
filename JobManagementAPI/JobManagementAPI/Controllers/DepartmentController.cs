using JobManagementAPI.Data;
using JobManagementAPI.Model;
using JobManagementAPI.Model.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        

        public DepartmentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetDepartmentList")]
        public IActionResult GetAllDepartment()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var allDept = dbContext.Department.ToList();
                return Ok(allDept);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }

        [HttpPost("CreateDepartment")]
        public IActionResult CreateDepartment([FromBody] CreateDeptDto deptDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Map JobDto to Job entity
                var department = new Department
                {
                    Title = deptDto.Title,
                };

                // Add the job to the database
                dbContext.Department.Add(department);
                dbContext.SaveChanges();

                // Return 
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("UpdateDepartment/{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] CreateDeptDto deptDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Get department by id
                var department = dbContext.Department.FirstOrDefault(d => d.ID == id);

                // If department not found, return 404 Not Found
                if (department == null)
                {
                    return NotFound();
                }

                // Update department properties
                department.Title = deptDto.Title;
                // Update other properties as needed

                // Saving changes to the database
                dbContext.SaveChanges();

                // Return  updated department
                return Ok(department);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}
