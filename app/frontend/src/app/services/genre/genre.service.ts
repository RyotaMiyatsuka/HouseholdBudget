import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  //genre of expenses
  genres = [];

  getGenres() {
    return this.genres;
  }

}
