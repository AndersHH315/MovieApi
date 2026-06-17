namespace MovieApi.DTOs
{
    public class MovieCreateDto
    {
        public string? Title { get; set; }
        public DateTime Year { get; set; }
        public string? Genre { get; set; }
        public int Duration { get; set; }

    }
}
