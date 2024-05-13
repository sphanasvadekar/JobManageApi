using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.Model.Entities
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } //Role for the job
        [Required]
        public string Description { get; set; }

        public string Code { get; set; } // Auto-generated for details
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow; // Track job creation

        public Location Location { get; set; }
        public Department Department { get; set; }
    }
}
