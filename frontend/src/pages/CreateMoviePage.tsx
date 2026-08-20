import { useNavigate } from "react-router-dom";
import type { IMovieCreate } from "../types/movie";
import { createMovie } from "../services/movieService";
import MovieForm from "../components/MovieForm";

export default function CreateMoviePage() {
  const navigate = useNavigate();

  const handleCreate = async (movie: IMovieCreate) => {
    try {
      await createMovie(movie);

      navigate("/movies");
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Add Movie</h1>
      <MovieForm submitLabel="Add Movie" onSubmit={handleCreate}/>
    </div>
  );
}