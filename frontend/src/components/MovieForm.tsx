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
        <div>
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
            <button type="submit">{submitLabel}</button>
          </form>
        </div>
    )
}
