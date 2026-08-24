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

export interface IMovieAllDetails {
  id: number;
  title: string;
  year: string;
  duration: number;
  genre: string;
  synopsis: string;
  language: string;
  budget: number;
  reviews: IReview[];
  actors: IActor[];
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

export interface IReview {
  id: number;
  reviewerName: string;
  comment: string;
  rating: number;
}

export interface ICreateReview {
  reviewerName: string;
  comment: string;
  rating: number;
}

export interface IActor {
  id: number;
  name: string;
  birthYear: string;
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