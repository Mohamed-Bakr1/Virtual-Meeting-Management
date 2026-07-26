using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.StreamingDto
{
    public class CreateMeetingDto
    {
        [Required(ErrorMessage = "[0] Field is Required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "[0] Field is Required!")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "[0] Field is Required!")]
        public DateTime EndTime { get; set; }

        [MaxLength(500, ErrorMessage = "The Max Length is [1]!")]
        public string? Description { get; set; }
    }
}