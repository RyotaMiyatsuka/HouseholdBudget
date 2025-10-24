import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { Input } from './input';
import { GenreService } from '../../services/genre/genre.service';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { GenreColor } from '../../models/genre-color.model';

describe('Input', () => {
  let component: Input;
  let fixture: ComponentFixture<Input>;
  let httpMock: HttpTestingController;

  const mockGenres: ExpenseGenre[] = [
    { id: 1, name: '食費', color: GenreColor.Green },
    { id: 2, name: '交通費', color: GenreColor.Blue },
    { id: 3, name: '医療費', color: GenreColor.Red }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Input],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting(),
        GenreService
      ],
    })
      .compileComponents();

    fixture = TestBed.createComponent(Input);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);

    // Don't call detectChanges() yet to avoid triggering ngOnInit
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load genres on init', () => {
    fixture.detectChanges(); // Trigger ngOnInit

    const req = httpMock.expectOne('/api/genres');
    expect(req.request.method).toBe('GET');
    req.flush(mockGenres);

    const genres = component.genres();
    expect(genres.length).toBe(4); // 3 genres + 'add-button'
    expect(genres[genres.length - 1]).toBe('add-button');
  });

  it('should initialize with default values', () => {
    expect(component.inputControl.value).toBe('');
    expect(component.selectedGenreIndex()).toBe(0);
  });

  it('should update selected genre index when selectGenre is called', () => {
    component.selectGenre(3);
    expect(component.selectedGenreIndex()).toBe(3);
  });

  it('should return input control value through value getter', () => {
    component.inputControl.setValue('1000');
    expect(component.value).toBe('1000');
  });

  it('should open modal when addNewGenre is called', () => {
    fixture.detectChanges();
    const req = httpMock.expectOne('/api/genres');
    req.flush(mockGenres);

    component.addNewGenre();
    expect(component.modalState().isOpen).toBeTruthy();
    expect(component.modalState().title).toBe('新しいジャンルを追加');
  });

  it('should create a new genre when onConfirmGenre is called', () => {
    fixture.detectChanges();
    const initialReq = httpMock.expectOne('/api/genres');
    initialReq.flush(mockGenres);

    component.genreNameControl.setValue('新ジャンル');
    component.selectedColorControl.setValue(GenreColor.Indigo);

    component.onConfirmGenre();

    const req = httpMock.expectOne('/api/genres');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({
      name: '新ジャンル',
      color: GenreColor.Indigo
    });

    const newGenre: ExpenseGenre = { id: 4, name: '新ジャンル', color: GenreColor.Indigo };
    req.flush(newGenre);

    const genres = component.genres();
    expect(genres.length).toBe(5); // 4 genres + 'add-button'
    expect(genres[3]).toEqual(newGenre);
    expect(component.modalState().isOpen).toBeFalsy();
  });

  it('should show upper limit modal when genre limit is reached', () => {
    const maxGenres: ExpenseGenre[] = Array.from({ length: 8 }, (_, i) => ({
      id: i + 1,
      name: `ジャンル${i + 1}`,
      color: GenreColor.Green
    }));

    fixture.detectChanges();
    const initialReq = httpMock.expectOne('/api/genres');
    initialReq.flush(maxGenres);

    component.addNewGenre();

    expect(component.modalState().isOpen).toBeTruthy();
    expect(component.modalState().type).toBe('notification');
    expect(component.modalState().message).toBe('ジャンルは最大8つまで登録できます。');
  });
});
