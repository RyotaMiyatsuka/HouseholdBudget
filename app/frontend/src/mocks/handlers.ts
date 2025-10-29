import { http, HttpResponse } from 'msw';
import { ExpenseGenre } from '../app/models/expense-genre.model';
import { GenreColor } from '../app/models/genre-color.model';

// Mock data for expense genres with colors
let mockGenres: ExpenseGenre[] = [
  { id: 1, name: '食費', color: GenreColor.Green },
  { id: 2, name: '交通費', color: GenreColor.Blue },
  { id: 3, name: '医療費', color: GenreColor.Red },
  { id: 4, name: '衣服', color: GenreColor.Purple },
  { id: 5, name: '食料品', color: GenreColor.Yellow },
  { id: 6, name: '娯楽費', color: GenreColor.Pink },
  { id: 7, name: 'その他', color: GenreColor.Orange },
];

// Define your mock API handlers here
export const handlers = [
  // GET all genres
  http.get('/api/genres', () => {
    return HttpResponse.json(mockGenres);
  }),

  // GET a single genre by ID
  http.get('/api/genres/:id', ({ params }) => {
    const { id } = params;
    const genre = mockGenres.find(g => g.id === Number(id));

    if (!genre) {
      return new HttpResponse(null, { status: 404 });
    }

    return HttpResponse.json(genre);
  }),

  // POST a new genre
  http.post('/api/genres', async ({ request }) => {
    const newGenre = await request.json() as Omit<ExpenseGenre, 'id'>;

    // Generate new ID
    const maxId = mockGenres.length > 0
      ? Math.max(...mockGenres.map(g => g.id))
      : 0;

    const genre: ExpenseGenre = {
      id: maxId + 1,
      ...newGenre
    };

    // Check genre limit (max 8)
    if (mockGenres.length >= 8) {
      return HttpResponse.json(
        { error: 'ジャンルは最大8つまで登録できます。' },
        { status: 400 }
      );
    }

    mockGenres.push(genre);
    return HttpResponse.json(genre, { status: 201 });
  }),

  // PUT update an existing genre
  http.put('/api/genres/:id', async ({ params, request }) => {
    const { id } = params;
    const updatedData = await request.json() as Partial<ExpenseGenre>;
    const index = mockGenres.findIndex(g => g.id === Number(id));

    if (index === -1) {
      return new HttpResponse(null, { status: 404 });
    }

    mockGenres[index] = { ...mockGenres[index], ...updatedData };
    return HttpResponse.json(mockGenres[index]);
  }),

  // DELETE a genre
  http.delete('/api/genres/:id', ({ params }) => {
    const { id } = params;
    const index = mockGenres.findIndex(g => g.id === Number(id));

    if (index === -1) {
      return new HttpResponse(null, { status: 404 });
    }

    mockGenres.splice(index, 1);
    return new HttpResponse(null, { status: 204 });
  }),
];
