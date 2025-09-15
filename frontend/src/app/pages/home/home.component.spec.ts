import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { of } from 'rxjs';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

class OpenLibraryServiceMock {
  searchBooksByAuthor = jasmine
    .createSpy('searchBooksByAuthor')
    .and.returnValue(of({ docs: [] }));
}
class BsModalServiceMock {
  show = jasmine.createSpy('show').and.returnValue({} as BsModalRef);
}

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let service: OpenLibraryServiceMock;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeComponent, BrowserAnimationsModule],
      providers: [
        { provide: OpenLibraryService, useClass: OpenLibraryServiceMock },
        { provide: BsModalService, useClass: BsModalServiceMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(
      OpenLibraryService
    ) as unknown as OpenLibraryServiceMock;
    fixture.detectChanges();
  });

  it('deve criar componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve renderizar título e subtítulo padrão', () => {
    const el: HTMLElement = fixture.nativeElement;

    expect(el.querySelector('.titulo')?.textContent).toContain(
      'Livros de J.R.R. Tolkien'
    );
    expect(el.querySelector('.subtitulo')?.textContent).toContain(
      'Descubra a magia da Terra-Média'
    );
  });

  it('deve renderizar os componentes filhos (menu e livros)', () => {
    const el: HTMLElement = fixture.nativeElement;

    expect(el.querySelector('app-menu-sidebar')).toBeTruthy();
    expect(el.querySelector('app-livros')).toBeTruthy();
  });

  it('deve verificar se a estrutura básica do layout existe', () => {
    const el: HTMLElement = fixture.nativeElement;
    expect(el.querySelector('.home-container')).toBeTruthy();
    expect(el.querySelector('.menu-sidebar-container')).toBeTruthy();
    expect(el.querySelector('.info-container')).toBeTruthy();
    expect(el.querySelector('.livro-content')).toBeTruthy();
  });

  it('deve inicializar LivrosComponent sem erro (serviço chamado)', () => {
    expect(service.searchBooksByAuthor).toHaveBeenCalled();
  });
});
