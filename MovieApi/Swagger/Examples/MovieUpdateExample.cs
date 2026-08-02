namespace MovieApi.Swagger.Examples
{
    using Microsoft.AspNetCore.JsonPatch;
    using MovieApi.Core.DTOs;
    using Swashbuckle.AspNetCore.Filters;

    public class MovieUpdateExample : IExamplesProvider<JsonPatchDocument<MovieUpdateDto>>
    {
        public JsonPatchDocument<MovieUpdateDto> GetExamples()
        {
            var patch = new JsonPatchDocument<MovieUpdateDto>();

            patch.Replace(x => x.Title, "Interstellar");
            patch.Replace(x => x.Year, new DateTime(2014, 11, 7));
            patch.Replace(x => x.Duration, 169);
            patch.Replace(x => x.Genre, "Sci-Fi");

            patch.Replace(x => x.MovieDetails.Budget, 165000000);
            patch.Replace(x => x.MovieDetails.Language, "English");
            patch.Replace(x => x.MovieDetails.Synopsis,
                "A thief enters people's dreams to steal information.");

            return patch;
        }
    }
}
