using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MovieApi.DTOs
{
    public class ActorDto
    {
        public string? Name { get; set; }
        public DateTime BirthYear { get; set; }
    }
}
