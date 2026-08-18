import { useEffect, useState} from 'react';
import './App.css'


/*Interfaces for the frontend */

interface IMovie {
  id: number;
  title: string;
  year: string;
  duration: number;
  genre: string;
  genreId: number;
  movieDetails: {
    synopsis: string | null;
    language: string | null;
    budget: number;
  }
}

interface IMovieEdit {
  id: number;
  title: string;
  year: string;
  duration: number;
  genreId: number;
  movieDetails: {
    synopsis: string;
    language: string;
    budget: number;
  }
}

interface IMovieCreate {
  title: string;
  year: string;
  duration: number;
  genreId: number;
  movieDetails: {
    synopsis: string;
    language: string;
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
  const [duration, setDuration] = useState<number>(0);
  const [genreId, setGenreId] = useState<number>(0);
  const [synopsis, setSynopsis] = useState<string>("");
  const [language, setLanguage] = useState<string>("");
  const [budget, setBudget] = useState<number>(0);
  const [editingMovieId, setEditMovieId] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);

  /*Delete method*/
  const deleteMovie = async(id: number) => {
    try {
      const response = await fetch(`https://localhost:7006/api/movies/${id}`, {
        method: "DELETE",

      });

      if (!response.ok) {
        const error = await response.text();
        console.error("Failed to delete the selected movie!", error)
        return;
      }

      console.log("Movie deleted!");

      await getMovies();
    } catch (error) {
      console.error("Delete request failed!", error);
    }
  };

  /*Update method */
  const updateMovie = async (movie: IMovieEdit) => {

    const patch = [
    {
      op: "replace",
      path: "/title",
      value: movie.title,
    },
    {
      op: "replace",
      path: "/year",
      value: movie.year,
    },
    {
      op: "replace",
      path: "/duration",
      value: movie.duration,
    },
    {
      op: "replace",
      path: "/genreId",
      value: movie.genreId,
    },
    {
      op: "replace",
      path: "/movieDetails/budget",
      value: movie.movieDetails.budget,
    },
    {
      op: "replace",
      path: "/movieDetails/language",
      value: movie.movieDetails.language,
    },
    {
      op: "replace",
      path: "/movieDetails/synopsis",
      value: movie.movieDetails.synopsis,
    },
  ];

    try {
      const response = await fetch(`https://localhost:7006/api/movies/${movie.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json-patch+json",
        },
        body: JSON.stringify(patch),
      });
      
      if (!response.ok) {
        const error = await response.text();
        console.error("Backend error", error);
        return;
      }

      const updatedMovie = await response.json();
      console.log("Updated movie! ", updatedMovie);
    } catch (error) {
      console.error(error);
      return;
    }
  };
      

  /*Post method */
  const createMovie = async (movie: IMovieCreate) => {

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

    } catch (error) {
      console.error("Request failed!", error);
      return;
    }
  }

  /*Get method*/
  const getMovies = async () => {
      try {
        const response = await fetch('https://localhost:7006/api/movies?page=1&pageSize=10');

      if (!response.ok) {
        const error = await response.text();
        console.error("Couldn't retrieve the movies!", error);
        return;
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
  /*Add/Update movie button */
  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (editingMovieId !== null) {
      const movie: IMovieEdit = {
        id: editingMovieId,
        title,
        year,
        duration,
        genreId,
        movieDetails: {
          synopsis,
          language,
          budget,
          },
      };

      await updateMovie(movie);
    } else {
      const movie: IMovieCreate = {
        title,
        year,
        duration,
        genreId,
        movieDetails: {
          synopsis,
          language,
          budget
        },

      };

      await createMovie(movie);

    }
      setTitle("");
      setYear("");
      setDuration(0);
      setGenreId(0);
      setSynopsis("");
      setLanguage("");
      setBudget(0);

      setEditMovieId(null);

      await getMovies();

    }

  /*Fills the form with the targeted movie for edit*/
  const editMovie = (movie: IMovie) => {

    console.log(movie);
    console.log(movie.movieDetails);
    setEditMovieId(movie.id);
    setTitle(movie.title);
    setYear(movie.year);
    setDuration(movie.duration);
    setGenreId(movie.genreId);
    setSynopsis(movie.movieDetails.synopsis ?? "");
    setLanguage(movie.movieDetails.language ?? "");
    setBudget(movie.movieDetails.budget);
  }

  useEffect(() => {

    getMovies();
  }, []);


  return (
    <>
      <div>
        <h1>Movies</h1>

        {movies.map((movie) =>(
          <div key={movie.id}>
            <h2>{movie.title}</h2>
            <p>
              {movie.year} · {movie.duration} minutes · {movie.genre}
            </p>
            <button type="button" onClick={() => editMovie(movie)}>Update</button>
            <button type="button" onClick={() => deleteMovie(movie.id)}>Delete</button>
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
              onChange={(e) => setDuration(Number(e.target.value))}/>
        </div>
        <div>
            <label>Genre</label>
            <select value={genreId} onChange={(e) => setGenreId(Number(e.target.value))}>
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
              type="text"
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
              onChange={(e) => setBudget(Number(e.target.value))}/>
        </div>
        <button type="submit">Add Movie</button>
      </form>
    </>
  )
}