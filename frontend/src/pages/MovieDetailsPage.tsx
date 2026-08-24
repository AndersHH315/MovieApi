import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import type { IMovieAllDetails, IReview } from "../types/movie";
import { getMovieDetails } from "../services/movieService";
import ReviewForm from "../components/ReviewForm";

export default function MovieDetails() {
    const { id } = useParams<{ id: string }>();

    const [movie, setMovie] = useState<IMovieAllDetails | null>(null);

    const addReview = (newReview: IReview) => {
        setMovie((currentMovie) => {
            if (!currentMovie) {
                return currentMovie;
            }

            return {
                ...currentMovie,
                reviews: [...currentMovie.reviews, newReview],
            };
        });
    };

    useEffect(() => {
        const loadMovie = async () => {
            try {
                if (!id) {
                    throw new Error("Movie ID is missing");
                }

                const data = await getMovieDetails(Number(id));
                setMovie(data);
                console.log("Movie data", data);
            } catch (error) {
                console.error(error);
            } 
        };

        loadMovie();
    }, [id]);

    if (!movie) {
        return (
            <div className="mx-auto max-w-5xl p-6">
                <p className="text-gray-600">Movie not found.</p>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-100">
            <div className="mx-auto max-w-5xl p-6">

                {/* Back button */}
                <Link
                    to="/movies"
                    className="mb-6 inline-block text-blue-600 hover:text-blue-800">
                    ← Back to movies
                </Link>

                {/* Movie information */}
                <div className="rounded-xl bg-white p-8 shadow-md">
                    <h1 className="text-4xl font-bold text-gray-900">
                        {movie.title}
                    </h1>

                    <div className="mt-4 flex flex-wrap gap-3 text-gray-600">
                        <span>{new Date(movie.year).getFullYear()}</span>
                        <span>•</span>
                        <span>{movie.duration} minutes</span>
                        <span>•</span>
                        <span>{movie.genre}</span>
                        <span>•</span>
                        <span>{movie.language}</span>
                    </div>

                    {/* Synopsis */}
                    <div className="mt-8">
                        <h2 className="text-2xl font-semibold text-gray-900">
                            Synopsis
                        </h2>

                        <p className="mt-3 leading-7 text-gray-600">
                            {movie.synopsis}
                        </p>
                    </div>

                    {/* Budget */}
                    <div className="mt-6">
                        <h2 className="text-lg font-semibold text-gray-900">
                            Budget
                        </h2>

                        <p className="mt-1 text-gray-600">
                            ${movie.budget.toLocaleString()}
                        </p>
                    </div>
                </div>

                {/* Actors */}
                <div className="mt-8 rounded-xl bg-white p-8 shadow-md">
                    <h2 className="text-2xl font-bold text-gray-900">
                        Actors
                    </h2>

                    {movie.actors.length === 0 ? (
                        <p className="mt-4 text-gray-500">
                            No actors available.
                        </p>
                    ) : (
                        <div className="mt-4 grid grid-cols-1 gap-4 sm:grid-cols-2">
                            {movie.actors.map((actor) => (
                                <div
                                    key={actor.id} className="rounded-lg bg-gray-50 p-4">
                                    <h3 className="font-semibold text-gray-900">
                                        {actor.name}
                                    </h3>

                                    <p className="text-sm text-gray-500">
                                        Born {actor.birthYear}
                                    </p>
                                </div>
                            ))}
                        </div>
                    )}
                </div>

                {/* Reviews */}
                <div className="mt-8 rounded-xl bg-white p-8 shadow-md">
                    <h2 className="text-2xl font-bold text-gray-900">
                        Reviews
                    </h2>

                    {movie.reviews.length === 0 ? (
                        <p className="mt-4 text-gray-500">
                            No reviews yet. Be the first to leave one!
                        </p>
                    ) : (
                        <div className="mt-6 space-y-4">
                            {movie.reviews.map((review) => (
                                <div key={review.id} className="rounded-lg border border-gray-200 p-5">
                                    <div className="flex items-center justify-between">
                                        <h3 className="font-semibold text-gray-900">
                                            {review.reviewerName}
                                        </h3>

                                        <span className="text-yellow-500">
                                            {"⭐".repeat(review.rating)}
                                        </span>
                                    </div>

                                    <p className="mt-3 text-gray-600">
                                        {review.comment}
                                    </p>
                                </div>
                            ))}
                        </div>
                    )}
                </div>

                {/* Add review */}
                <div className="mt-8 rounded-xl bg-white p-8 shadow-md">
                    <ReviewForm movieId={movie.id} addReview={addReview} />
                </div>

            </div>
        </div>
    );
}