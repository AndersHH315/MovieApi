import { useEffect, useState} from 'react';
import './App.css'


/*Interfaces for the frontend */
interface IMovie {
  title: string;
  year: number;
  duration: number;
  genre: string;
}

interface IMovieCreate {
  title: string;
  year: string;
  duration: number;
  genreId: number;
  movieDetails: {
    synopsis: string | null;
    language: string | null;
    budget: number;
  }
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
  const [title, setTitle] = useState("");
  const [year, setYear] = useState("");
  const [duration, setDuration] = useState("");
  const [genreId, setGenreId] = useState("");
  const [synopsis, setSynopsis] = useState("");
  const [language, setLanguage] = useState("");
  const [budget, setBudget] = useState("");
  const [error, setError] = useState<string | null>(null);

  /*Post method */
  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    const movie: IMovieCreate = {
      title,
      year,
      duration: Number(duration),
      genreId: Number(genreId),
      movieDetails: {
        synopsis,
        language,
        budget: Number(budget)
      }
    };

    try {
      const response = await fetch("https://localhost:7006/api/movies",{
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(movie)
        
      });

      if (!response.ok) {
        const error = await response.text();
        console.error("Backend error:", error);
        return;
      }

      const result = await response.json();

      console.log("Movie created! ", result);

      setTitle("");
      setYear("");
      setDuration("");
      setGenreId("");
      setSynopsis("");
      setLanguage("");
      setBudget("");
      await getMovies();
    } catch (error) {
      console.error("Request failed!", error);
    }
  };

  /*Get method*/
  const getMovies = async () => {
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
      }
  };

  useEffect(() => {

    getMovies();
  }, []);


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

      <form onSubmit={handleSubmit}>
        <div>
            <label>Title</label>
            <input
              type="text"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Title"/>
        </div>
        <div>
            <label>Year</label>
            <input
              type="date"
              value={year}
              onChange={(e) => setYear(e.target.value)}
              placeholder="example: 1995-06-25"/>
        </div>
        <div>
            <label>Duration</label>
            <input
              type="number"
              min="60"
              max="250"
              value={duration}
              onChange={(e) => setDuration(e.target.value)}/>
        </div>
        <div>
            <label>Genre</label>
            <select value={genreId} onChange={(e) => setGenreId(e.target.value)}>
              <option value="">Select Genre</option>
              <option value="1">Action</option>
              <option value="2">Sci-Fi</option>
              <option value="3">Drama</option>
              <option value="4">Comedy</option>
              <option value="5">Horro</option>
              <option value="6">Romance</option>
              <option value="7">Thriller</option>
              <option value="8">Documentary</option>
            </select>
        </div>
        <div>
            <label>Synospsis</label>
            <input
              type="test"
              value={synopsis}
              onChange={(e) => setSynopsis(e.target.value)}/>
        </div>
        <div>
            <label>Language</label>
            <input
              type="text"
              value={language}
              onChange={(e) => setLanguage(e.target.value)}/>
        </div>
        <div>
            <label>Budget</label>
            <input
              type="number"
              value={budget}
              onChange={(e) => setBudget(e.target.value)}/>
        </div>
        <button type="submit">Add Movie</button>
      </form>
    </>
  )
}