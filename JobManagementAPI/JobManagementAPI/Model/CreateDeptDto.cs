using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.Model
{
    public class CreateDeptDto
    {

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
    }
}
