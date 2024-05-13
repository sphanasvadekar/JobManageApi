using JobManagementAPI.Model.Entities;
using JobManagementAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobManagementAPI.Data;

namespace JobManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public LocationController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetAllLocationsList")]
        public IActionResult GeAllLocations()
        {
            var allLoc = dbContext.Location.ToList();
            return Ok(allLoc);
        }

        [HttpPost("CreateLocation")]
        public IActionResult CreateLocation([FromBody] CreateLocationDto LocDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Location = new Location
            {
                Title = LocDto.Title,
                Zip = LocDto.Zip,
                City = LocDto.City,
                Country = LocDto.Country,
                State = LocDto.State
            };

            // Add the job to the database
            dbContext.Location.Add(Location);
            dbContext.SaveChanges();

            // Return 
            return Ok(Location);
        }

        [HttpPut("UpdateLocation/{id}")]
        public IActionResult UpdateLocation(int id, [FromBody] CreateLocationDto Loc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Get department by id
                var Location = dbContext.Location.FirstOrDefault(d => d.ID == id);

                // If department not found, return 404 Not Found
                if (Location == null)
                {
                    return NotFound();
                }

                // Update department properties
                Location.Title = Loc.Title;
                Location.Zip = Loc.Zip;
                Location.City = Loc.City;
                Location.Country = Loc.Country;
                Location.State = Loc.State;


                // Update other properties as needed

                // Saving changes to the database
                dbContext.SaveChanges();

                // Return  updated job
                return Ok(Location);
            }
            catch (Exception ex)
            {
                // error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
