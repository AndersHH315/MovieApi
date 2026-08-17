import { useEffect, useState } from 'react'
import './App.css'

interface IMovie {
  title: string;
  year: number;
  duration: number;
  genre: string;
}

interface IPagingMeta {
  totalItems: number;
  currentPage: number;
  totalPages: number
  pageSize: number;

}

interface IPagedResult<T> {
  data: T[];
  meta: IPagingMeta;
}

export default function App() {
  
  const [movies, setMovies] = useState<IMovie[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function getMovies() {
      try {
        const response = await fetch('https://localhost:7006/api/movies?page=1&pageSize=10');

      if (!response.ok) {
        throw new Error(`HTTP error: ${response.status}`)
      }

      const result: IPagedResult<IMovie> = await response.json();

      setMovies(result.data);

      console.log(result.meta);
      
      console.error(error);
      } catch (error) {
        setError('Failed to fetch movies');
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    getMovies();
  }, []);

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>(error)</p>;
  }

  return (
    <>
      <div>
        <h1>Movies</h1>

        {movies.map((movie) =>(
          <div key={movie.title}>
            <h2>{movie.title}</h2>
            <p>
              {movie.year} · {movie.duration} minutes · {movie.genre}
            </p>
          </div>
        ))}
      </div>
    </>
  )
}