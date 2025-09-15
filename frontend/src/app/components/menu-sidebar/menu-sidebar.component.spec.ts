import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuSidebarComponent } from './menu-sidebar.component';

describe('MenuSidebarComponent', () => {
  let component: MenuSidebarComponent;
  let fixture: ComponentFixture<MenuSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuSidebarComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('deve criar componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve renderizar valores padrão (versão, usuário, perfil)', () => {
    const el: HTMLElement = fixture.nativeElement;

    expect(el.querySelector('.version-text')?.textContent).toContain('v1.0.0');
    expect(el.querySelector('.user-name')?.textContent).toContain('Anonymous');
    expect(el.querySelector('.user-role')?.textContent).toContain('Visitante');
  });

  it('deve mostrar o ícone de seta no menu', () => {
    const el: HTMLElement = fixture.nativeElement;
    const icon = el.querySelector('mat-icon');

    expect(icon).toBeTruthy();
    expect(icon?.textContent?.trim()).toBe('keyboard_arrow_down');
  });

  it('deve exibir as imagens do logo e do avatar', () => {
    const el: HTMLElement = fixture.nativeElement;
    const imgs = el.querySelectorAll('img');
    const logo = imgs[0] as HTMLImageElement;
    const avatar = imgs[1] as HTMLImageElement;

    expect(logo?.getAttribute('alt')).toBe('Logo Terra Média');
    expect(avatar?.getAttribute('alt')).toBe('User Avatar');
    expect(logo?.getAttribute('src')).toContain('assets/images/logo.png');
    expect(avatar?.getAttribute('src')).toContain(
      'assets/images/user-avatar.png'
    );
  });
});
