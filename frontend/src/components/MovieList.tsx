import { Link } from "react-router-dom";
import type { IMovie } from "../types/movie";

interface movieListProps {
    movies: IMovie[];
    onDelete: (id: number) => void;
}

export default function MovieList({
    movies,
    onDelete,   
}: movieListProps) {
    return(
        <div>
            {movies.map((movie) => (
                <div key={(movie.id)}>
                    <h2>{movie.title}</h2>
                    <p>
                        {movie.year} - {movie.duration} minutes - {movie.genre}
                    </p>

                    <Link to={`/movies/${movie.id}/edit`}>Update</Link>
                    <button type="button" onClick={() => onDelete(movie.id)}>Delete</button>
                </div>
            ))}
        </div>
    );
}