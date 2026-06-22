namespace MovieApi.DTOs
{
    public class MovieUpdateDto
    {
        public string? Title { get; set; }
        public DateTime Year { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
    }
}
