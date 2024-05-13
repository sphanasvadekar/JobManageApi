using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.Model
{
    public class UpdateDeptDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
    }
}
