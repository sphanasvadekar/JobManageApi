using JobManagementAPI.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.Model
{
    public class CreateJobDto
    {
            [Required(ErrorMessage = "Title is required")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Description is required")]
            public string Description { get; set; }

            [Required(ErrorMessage = "LocationId is required")]
            public int LocationId { get; set; }

            [Required(ErrorMessage = "DepartmentId is required")]
            public int DepartmentId { get; set; }

            [Required(ErrorMessage = "ClosingDate is required")]
            public DateTime ClosingDate { get; set; }

            //public Location Location { get; set; }
            //public Department Department { get; set; }

    }
}
