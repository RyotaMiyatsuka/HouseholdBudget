import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { Input } from './input';

describe('Input', () => {
  let component: Input;
  let fixture: ComponentFixture<Input>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Input],
      providers: [provideRouter([])],
    })
      .compileComponents();

    fixture = TestBed.createComponent(Input);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with default values', () => {
    expect(component.inputControl.value).toBe('');
    expect(component.selectedGenreIndex()).toBe(0);
    expect(component.genres().length).toBe(11);
  });

  it('should update selected genre index when selectGenre is called', () => {
    component.selectGenre(3);
    expect(component.selectedGenreIndex()).toBe(3);
  });

  it('should return input control value through value getter', () => {
    component.inputControl.setValue('1000');
    expect(component.value).toBe('1000');
  });
});
