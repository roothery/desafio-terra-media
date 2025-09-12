import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalAutorComponent } from './modal-autor.component';

describe('ModalAutorComponent', () => {
  let component: ModalAutorComponent;
  let fixture: ComponentFixture<ModalAutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalAutorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalAutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
