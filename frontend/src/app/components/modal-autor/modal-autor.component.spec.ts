import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalAutorComponent } from './modal-autor.component';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { of } from 'rxjs';

class OpenLibraryServiceMock {
  getAuthorBiography = jasmine.createSpy('getAuthorBiography');
}
class BsModalRefMock {
  hide = jasmine.createSpy('hide');
}

describe('ModalAutorComponent', () => {
  let component: ModalAutorComponent;
  let fixture: ComponentFixture<ModalAutorComponent>;
  let service: OpenLibraryServiceMock;
  let bsRef: BsModalRefMock;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalAutorComponent],
      providers: [
        { provide: OpenLibraryService, useClass: OpenLibraryServiceMock },
        { provide: BsModalRef, useClass: BsModalRefMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ModalAutorComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(
      OpenLibraryService
    ) as unknown as OpenLibraryServiceMock;
    bsRef = TestBed.inject(BsModalRef) as unknown as BsModalRefMock;
    fixture.detectChanges();
  });

  it('deve criar componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve mostrar o título do modal', () => {
    const el: HTMLElement = fixture.nativeElement;

    expect(el.querySelector('.modal-title')?.textContent).toContain(
      'Propriedades do Autor'
    );
  });

  it('deve ignorar o serviço quando codigoAutor é null', () => {
    component.codigoAutor = null;
    component.ngOnInit();

    expect(service.getAuthorBiography).not.toHaveBeenCalled();
  });

  it('deve carregar nome e formatar bio quando codigoAutor existe', () => {
    const codigoAutor = 'OL26320A';
    const nomeAutor = 'J. R. R. Tolkien';
    const biografia = 'Teste de biografia';

    component.codigoAutor = codigoAutor;
    component.nomeAutor = 'Desconhecido';
    service.getAuthorBiography.and.returnValue(
      of({
        name: nomeAutor,
        bio: biografia,
      })
    );

    component.ngOnInit();

    expect(service.getAuthorBiography).toHaveBeenCalledWith(codigoAutor);
    expect(component.nome).toBe(nomeAutor);
    expect(component.bio).toBe(biografia);
  });
});
