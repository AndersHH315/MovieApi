import './App.css'
import { Link, Route, Routes } from "react-router-dom";
import MoviePage from "./pages/MoviePage";
import CreateMoviePage from "./pages/CreateMoviePage";
import EditMoviePage from "./pages/EditMoviePage";
import MovieDetailsPage from "./pages/MovieDetailsPage";

export default function App() {
  return (
    <>
      <nav>
        <Link to="/movies">Movies</Link>
      </nav>

      <Routes>
        <Route path="/movies" element={<MoviePage />} />
        <Route path="/movies/create" element={<CreateMoviePage />}/>
        <Route path="/movies/:id/details" element={<MovieDetailsPage />}/>
        <Route path="/movies/:id/edit" element={<EditMoviePage />}/>
      </Routes>
    </>
  );
}