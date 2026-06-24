using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MovieApi.DTOs
{
    public class ActorDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 3-30 chars!")]
        public string? Name { get; set; }
        public DateTime BirthYear { get; set; }
    }
}
