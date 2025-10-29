import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';

import { GenreService } from './genre.service';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { GenreColor } from '../../models/genre-color.model';

describe('GenreService', () => {
  let service: GenreService;
  let httpMock: HttpTestingController;

  const mockGenres: ExpenseGenre[] = [
    { id: 1, name: '食費', color: GenreColor.Green },
    { id: 2, name: '交通費', color: GenreColor.Blue },
    { id: 3, name: '医療費', color: GenreColor.Red }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        GenreService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(GenreService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all genres', () => {
    service.getGenres().subscribe(genres => {
      expect(genres).toEqual(mockGenres);
      expect(genres.length).toBe(3);
    });

    const req = httpMock.expectOne('/api/genres');
    expect(req.request.method).toBe('GET');
    req.flush(mockGenres);
  });

  it('should get a genre by id', () => {
    const genreId = 1;
    const mockGenre = mockGenres[0];

    service.getGenreById(genreId).subscribe(genre => {
      expect(genre).toEqual(mockGenre);
    });

    const req = httpMock.expectOne(`/api/genres/${genreId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockGenre);
  });

  it('should create a new genre', () => {
    const newGenreData = { name: '娯楽費', color: GenreColor.Pink };
    const createdGenre: ExpenseGenre = { id: 4, ...newGenreData };

    service.createGenre(newGenreData).subscribe(genre => {
      expect(genre).toEqual(createdGenre);
      expect(genre.id).toBe(4);
    });

    const req = httpMock.expectOne('/api/genres');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newGenreData);
    req.flush(createdGenre);
  });

  it('should update a genre', () => {
    const genreId = 1;
    const updateData = { name: '食費（更新）' };
    const updatedGenre: ExpenseGenre = { id: 1, name: '食費（更新）', color: GenreColor.Green };

    service.updateGenre(genreId, updateData).subscribe(genre => {
      expect(genre).toEqual(updatedGenre);
    });

    const req = httpMock.expectOne(`/api/genres/${genreId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updateData);
    req.flush(updatedGenre);
  });

  it('should delete a genre', () => {
    const genreId = 1;

    service.deleteGenre(genreId).subscribe((result) => {
      expect(result).toBeNull();
    });

    const req = httpMock.expectOne(`/api/genres/${genreId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
