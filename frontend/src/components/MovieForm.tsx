import { useState } from "react";
import type { IMovieCreate } from "../types/movie";

interface IMovieFormProps {
    initialValues?: IMovieCreate;
    submitLabel: string;
    onSubmit: (movie: IMovieCreate) => Promise<void>;
}

export default function MovieForm({
    initialValues,
    submitLabel,
    onSubmit,
}: IMovieFormProps) {
    const [title, setTitle] = useState(initialValues?.title ?? "");
    const [year, setYear] = useState(initialValues?.year ?? "");
    const [duration, setDuration] = useState(initialValues?.duration ?? 0);
    const [genreId, setGenreId] = useState(initialValues?.genreId ?? 0);
    const [synopsis, setSynopsis] = useState(initialValues?.movieDetails.synopsis ?? "");
    const [language, setLanguage] = useState(initialValues?.movieDetails.language ?? "");
    const [budget, setBudget] = useState(initialValues?.movieDetails.budget ?? 0);

    const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

      const movie: IMovieCreate = {
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

      await onSubmit(movie);
    };
    return (
        <div className="min-h-screen bg-gray-100 py-10">
          <div className="mx-auto max-w-2xl px-6">
            <div className="rounded-xl bg-white p-8 shadow-md">
              <h1 className="mb-6 text-3xl font-bold text-gray-900">
                  {submitLabel === "Add Movie"
                      ? "Create Movie"
                      : "Edit Movie"}
              </h1>

              <form onSubmit={handleSubmit} className="space-y-6">

                {/* Title */}
                <div>
                    <label htmlFor="title" className="mb-2 block text-sm font-medium text-gray-700">
                        Title
                    </label>

                    <input
                        id="title"
                        type="text"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        placeholder="Enter movie title"
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-gray-900 
                        outline-none transition placeholder:text-gray-400 focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Year */}
                <div>
                    <label
                        htmlFor="year"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Release date
                    </label>

                    <input
                        id="year"
                        type="date"
                        value={year}
                        onChange={(e) => setYear(e.target.value)}
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         text-gray-900 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Duration */}
                <div>
                    <label
                        htmlFor="duration"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Duration (minutes)
                    </label>

                    <input
                        id="duration"
                        type="number"
                        min="60"
                        max="250"
                        value={duration}
                        onChange={(e) => setDuration(Number(e.target.value))}
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         text-gray-900 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Genre */}
                <div>
                    <label
                        htmlFor="genre"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Genre
                    </label>

                    <select
                        id="genre"
                        value={genreId}
                        onChange={(e) => setGenreId(Number(e.target.value))}
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         text-gray-900 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200">
                        <option value="">Select Genre</option>
                        <option value="1">Action</option>
                        <option value="2">Sci-Fi</option>
                        <option value="3">Drama</option>
                        <option value="4">Comedy</option>
                        <option value="5">Horror</option>
                        <option value="6">Romance</option>
                        <option value="7">Thriller</option>
                        <option value="8">Documentary</option>
                    </select>
                </div>

                {/* Synopsis */}
                <div>
                    <label
                        htmlFor="synopsis"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Synopsis
                    </label>

                    <textarea
                        id="synopsis"
                        value={synopsis}
                        onChange={(e) => setSynopsis(e.target.value)}
                        placeholder="Enter a short synopsis"
                        rows={5}
                        className="w-full resize-none rounded-lg border border-gray-300
                         bg-white px-4 py-2.5 text-gray-900 outline-none transition placeholder:text-gray-400
                          focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Language */}
                <div>
                    <label
                        htmlFor="language"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Language
                    </label>

                    <input
                        id="language"
                        type="text"
                        value={language}
                        onChange={(e) => setLanguage(e.target.value)}
                        placeholder="e.g. English"
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         text-gray-900 outline-none transition placeholder:text-gray-400 focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Budget */}
                <div>
                    <label
                        htmlFor="budget"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Budget
                    </label>

                    <input
                        id="budget"
                        type="number"
                        value={budget}
                        onChange={(e) => setBudget(Number(e.target.value))}
                        placeholder="e.g. 165000000"
                        className="w-full rounded-lg border border-gray-300 bg-white px-4 py-2.5
                         text-gray-900 outline-none transition placeholder:text-gray-400 focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Submit */}
                <button
                    type="submit"
                    className="w-full rounded-lg bg-blue-600 px-5 py-3 
                    font-medium text-white transition hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-300">
                    {submitLabel}
                </button>
            </form>
        </div>
    </div>
</div>
    )
}
