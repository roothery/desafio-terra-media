import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalLivroComponent } from './modal-livro.component';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import lottie from 'lottie-web';

class OpenLibraryServiceMock {
  getBookCoverUrl = jasmine.createSpy('getBookCoverUrl');
}
class BsModalRefMock {
  hide = jasmine.createSpy('hide');
}

describe('ModalLivroComponent', () => {
  let component: ModalLivroComponent;
  let fixture: ComponentFixture<ModalLivroComponent>;
  let service: OpenLibraryServiceMock;
  let bsRef: BsModalRefMock;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalLivroComponent],
      providers: [
        { provide: OpenLibraryService, useClass: OpenLibraryServiceMock },
        { provide: BsModalRef, useClass: BsModalRefMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ModalLivroComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(
      OpenLibraryService
    ) as unknown as OpenLibraryServiceMock;
    bsRef = TestBed.inject(BsModalRef) as unknown as BsModalRefMock;
    (lottie as any).loadAnimation = jasmine.createSpy('loadAnimation');
    fixture.detectChanges();
  });

  it('deve criar componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve mostrar o título do modal', () => {
    const el: HTMLElement = fixture.nativeElement;

    expect(el.querySelector('.modal-title')?.textContent).toContain(
      'Propriedades do Livro'
    );
  });

  it('deve mostrar imagem padrão quando detalhesLivro é null', () => {
    const imagemPadrao = 'assets/images/livro-nao-encontrado.png';
    component.detalhesLivro = null;

    expect(service.getBookCoverUrl).not.toHaveBeenCalled();
    expect(component.imagemLivro).toBe(imagemPadrao);
  });

  it('deve fechar o modal', () => {
    component.close();
    expect(bsRef.hide).toHaveBeenCalled();
  });
});
