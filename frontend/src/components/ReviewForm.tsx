import { useState } from "react";
import { postReview } from "../services/movieService";
import type { IReview, ICreateReview } from "../types/movie";

interface ReviewFormProps {
    movieId: number;
    addReview: (review: IReview) => void;
}

export default function ReviewForm({ movieId, addReview }: ReviewFormProps) {
    const [reviewerName, setReviewerName] = useState("");
    const [comment, setComment] = useState("");
    const [rating, setRating] = useState(5);

    const [message, setMessage] = useState("");
    const [error, setError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (
        event: React.SubmitEvent<HTMLFormElement>
    ) => {
        event.preventDefault();

        try {
        setMessage("");
        setError("");
        setIsSubmitting(true);

        const review: ICreateReview = {
            reviewerName,
            comment,
            rating,
        };

            const newReview = await postReview(movieId, review);

            addReview(newReview);

            setMessage("Review submitted successfully!");

            // Clears the form
            setReviewerName("");
            setComment("");
            setRating(5);
        } catch (error) {
            console.error(error);
            setError("Couldn't submit your review.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div>
            <h2 className="text-2xl font-bold text-gray-900">
                Leave a review
            </h2>

            <p className="mt-2 text-gray-500">
                What did you think about this movie?
            </p>

            <form
                onSubmit={handleSubmit}
                className="mt-6 space-y-5"
            >
                {/* Reviewer name */}
                <div>
                    <label
                        htmlFor="reviewerName"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Name
                    </label>

                    <input
                        id="reviewerName"
                        type="text"
                        value={reviewerName}
                        onChange={(event) => setReviewerName(event.target.value)} required className="w-full rounded-lg border border-gray-300 px-4 py-2.5
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200" placeholder="Enter your name"/>
                </div>

                {/* Rating */}
                <div>
                    <label
                        htmlFor="rating"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Rating
                    </label>

                    <select
                        id="rating"
                        value={rating}
                        onChange={(event) =>
                            setRating(Number(event.target.value))
                        }
                        className="rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200">
                        <option value={1}>⭐ 1 - Poor</option>
                        <option value={2}>⭐⭐ 2 - Fair</option>
                        <option value={3}>⭐⭐⭐ 3 - Good</option>
                        <option value={4}>⭐⭐⭐⭐ 4 - Great</option>
                        <option value={5}>⭐⭐⭐⭐⭐ 5 - Excellent</option>
                    </select>
                </div>

                {/* Comment */}
                <div>
                    <label
                        htmlFor="comment"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Comment
                    </label>

                    <textarea
                        id="comment"
                        value={comment}
                        onChange={(event) => setComment(event.target.value)} required rows={5}
                        placeholder="Write your comment..."
                        className="w-full resize-none rounded-lg border border-gray-300 px-4 py-3
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Submit */}
                <button
                    type="submit"
                    disabled={isSubmitting}
                    className="rounded-lg bg-blue-600 px-5 py-2.5 
                    font-medium text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-blue-300">
                    {isSubmitting ? "Submitting..." : "Submit review"}
                </button>

                {/* Success */}
                {message && (
                    <p className="rounded-lg bg-green-100 p-3 text-green-700">
                        {message}
                    </p>
                )}

                {/* Error */}
                {error && (
                    <p className="rounded-lg bg-red-100 p-3 text-red-700">
                        {error}
                    </p>
                )}
            </form>
        </div>
    );
}