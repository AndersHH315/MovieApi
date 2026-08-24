import type {IMovie, IMovieEdit, IMovieCreate, IPagedResult, IMovieAllDetails, IReview, ICreateReview } from "../types/movie";

const API_URL = "https://localhost:7006/api/movies";

/*Get method*/
export const getMovies = async (): Promise<IPagedResult<IMovie>> => {
    const response = await fetch(`${API_URL}?page=1&pageSize=10`);

    if (!response.ok) {
        throw new Error("Couldn't retrieve the movies!")
    }

    return response.json();          
};

/*Get movie details method */
export const getMovieDetails = async (id: number): Promise<IMovieAllDetails> => {
  const response = await fetch(`${API_URL}/${id}/details`);

  if (!response.ok) {
    throw new Error("Couldn't retrive the seleced movie!");
  }

  return response.json();
}

/*Post movie method */
export const createMovie = async (movie: IMovieCreate): Promise<IMovie> => {
    const response = await fetch(`${API_URL}`,{
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(movie)        
    });

    if (!response.ok) {
        throw new Error("Failed to create the movie!")
    }

    console.log("Movie created!");
    return response.json();
};

/*Post review method */
export const postReview = async (movieId: number, review: ICreateReview): Promise<IReview> => {
    const response = await fetch(`${API_URL}/${movieId}/reviews`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(review),
    }
  );

  if (!response.ok) {
    throw new Error("Couldn't post the review!");
  }

  return response.json();
};

/*Update movie method */
export const updateMovie = async (movie: IMovieEdit): Promise<IMovie> => {

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

    const response = await fetch(`${API_URL}/${movie.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json-patch+json",
        },
        body: JSON.stringify(patch),
    });
      
    if (!response.ok) {
        throw new Error("Failed to update the movie!")
    }

    console.log("Movie updated!");
    return response.json();

};

/*Delete method*/
export const deleteMovie = async(id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) {
        throw new Error("Failed to delete movie!");
    }
    console.log("Movie deleted!");
};