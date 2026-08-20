import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { IMovie, IMovieCreate, IMovieEdit} from "../types/movie";
import { getMovies, updateMovie } from "../services/movieService";
import MovieForm from "../components/MovieForm";

export default function EditMoviePage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [movie, setMovie] = useState<IMovie | null>(null);

  useEffect(() => {
    const loadMovie = async () => {
      try {
        const result = await getMovies();

        const foundMovie = result.data.find(
          (movie) => movie.id === Number(id)
        );

        setMovie(foundMovie ?? null);
      } catch (error) {
        console.error(error);
      }
    };

    loadMovie();
  }, [id]);

  if (!movie) {
    return <p>Loading movie...</p>;
  }

  const initialValues: IMovieCreate = {
    title: movie.title,
    year: movie.year,
    duration: movie.duration,
    genreId: movie.genreId,
    movieDetails: {
      synopsis: movie.movieDetails.synopsis ?? "",
      language: movie.movieDetails.language ?? "",
      budget: movie.movieDetails.budget,
    },
  };

  const handleUpdate = async (values: IMovieCreate) => {
    const movieToUpdate: IMovieEdit = {
      id: movie.id,
      ...values,
    };

    try {
      await updateMovie(movieToUpdate);

      navigate("/movies");
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Edit Movie</h1>

      <MovieForm initialValues={initialValues} submitLabel="Update Movie" onSubmit={handleUpdate}/>
    </div>
  );
}