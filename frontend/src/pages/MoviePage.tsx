import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import type { IMovie } from "../types/movie";
import { deleteMovie, getMovies } from "../services/movieService";
import MovieList from "../components/MovieList";

export default function MoviesPage() {
  const [movies, setMovies] = useState<IMovie[]>([]);
  const [error, setError] = useState<string | null>(null);

  const loadMovies = async () => {
    try {
      const result = await getMovies();
      setMovies(result.data);
    } catch (error) {
      console.error(error);
      setError("Failed to fetch movies");
    }
  };

  useEffect(() => {
    loadMovies();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteMovie(id);
      await loadMovies();
    } catch (error) {
      console.error(error);
      setError("Failed to delete movie");
    }
  };

  return (
    <div>
      <h1>Movies</h1>

      {error && <p>{error}</p>}

      <Link to="/movies/create">Add Movie</Link>

      <MovieList movies={movies} onDelete={handleDelete}/>
    </div>
  );
}