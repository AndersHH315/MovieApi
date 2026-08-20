/*Interfaces for the frontend */

export interface IMovie {
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

export interface IMovieEdit {
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

export interface IMovieCreate {
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

export interface IPagingMeta {
  totalItems: number;
  currentPage: number;
  totalPages: number
  pageSize: number;

}

export interface IPagedResult<T> {
  data: T[];
  meta: IPagingMeta;
}