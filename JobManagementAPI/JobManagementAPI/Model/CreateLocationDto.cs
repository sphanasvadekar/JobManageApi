using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.Model
{
    public class CreateLocationDto
    {
       
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string? Title { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        public string? City { get; set; }
        [Required(ErrorMessage = "State is required")]
        [StringLength(50)]
        public string? State { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(50)]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public int Zip { get; set; }
    }
}
