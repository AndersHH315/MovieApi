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
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
            {movies.map((movie) => (
                <div key={movie.id} className="flex flex-col">
            
            {/* Movie card */}
            <Link to={`/movies/${movie.id}/details`}
                className="block rounded-xl bg-white p-6 shadow-md transition hover:-translate-y-1 hover:shadow-xl">
                <h2 className="mb-2 text-2xl font-bold text-gray-900">
                    {movie.title}
                </h2>
                <p className="text-gray-600">
                    {new Date(movie.year).getFullYear()} - {movie.duration} minutes
                </p>
                <span className="mt-3 inline-block rounded-full bg-blue-100 px-3 py-1 text-sm font-medium text-blue-700">
                    {movie.genre}
                </span>
            </Link>

            {/* Buttons */}
            <div className="mt-3 flex gap-3">
                <Link
                    to={`/movies/${movie.id}/edit`}
                    className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-medium text-white transition hover:bg-blue-700">
                    Update
                </Link>

                <button
                    type="button"
                    onClick={() => onDelete(movie.id)}
                    className="rounded-lg bg-red-600 px-4 py-2 text-sm font-medium text-white transition hover:bg-red-700">
                    Delete
                </button>
            </div>
        </div>
            ))}
        </div>
    );
}