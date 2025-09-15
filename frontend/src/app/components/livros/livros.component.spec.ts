import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LivrosComponent } from './livros.component';
import { of } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { TipoModalEnum } from '../../core/enums/tipo-modal.enum';

class OpenLibraryServiceMock {
  searchBooksByAuthor() {
    return of({
      docs: [
        {
          titulo: 'Hobbit',
          autor: ['J. R. R. Tolkien'],
          autorFormatado: 'J. R. R. Tolkien',
          anoPublicacao: 2017,
          codigoAutor: ['OL26320A'],
        },
        {
          titulo: 'Complete History of Middle-Earth',
          autor: ['Christopher Tolkien'],
          autorFormatado: 'Christopher Tolkien',
          anoPublicacao: 2017,
          codigoAutor: ['OL2623360A'],
        },
      ],
    });
  }
}
class BsModalServiceMock {
  show = jasmine.createSpy('show').and.returnValue({} as BsModalRef);
}

describe('LivrosComponent', () => {
  let fixture: ComponentFixture<LivrosComponent>;
  let component: LivrosComponent;
  let modalService: BsModalService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LivrosComponent, BrowserAnimationsModule],
      providers: [
        { provide: OpenLibraryService, useClass: OpenLibraryServiceMock },
        { provide: BsModalService, useClass: BsModalServiceMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LivrosComponent);
    component = fixture.componentInstance;
    modalService = TestBed.inject(BsModalService);
    fixture.detectChanges();
  });

  it('deve criar componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve filtrar todos os itens ao aplicar filtro vazio', () => {
    const tamanhoListaCompleta = component.dataSource.data.length;

    component.applyFilter({ target: { value: '' } } as any);

    expect(component.dataSource.data.length).toBe(tamanhoListaCompleta);
    expect(component.dataSource.filteredData.length).toBe(tamanhoListaCompleta);
  });

  it('deve filtrar por título ao chamar applyFilter', () => {
    const evento = { target: { value: 'hobbit' } } as unknown as Event;

    component.applyFilter(evento);

    expect(component.dataSource.data.length).toBe(2);
    expect(component.dataSource.filteredData.length).toBe(1);
    expect(component.dataSource.filteredData[0].titulo).toBe('Hobbit');
  });

  it('deve abrir modal com o objeto no initialState', () => {
    const livro = component.dataSource.data[0];
    component.abrirModal(livro as any, TipoModalEnum.Livro);

    const [compRef, config] = (
      modalService.show as jasmine.Spy
    ).calls.mostRecent().args;
    expect(compRef.name).toBe('ModalLivroComponent');
    expect(config.initialState).toEqual({ detalhesLivro: livro });
  });
});
