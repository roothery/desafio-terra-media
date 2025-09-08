import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-menu-sidebar',
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './menu-sidebar.component.html',
  styleUrl: './menu-sidebar.component.scss',
})
export class MenuSidebarComponent {
  versao: string = 'v1.0.0';
  usuario: string = 'Anonymous';
  perfilDoUsuario: string = 'Visitante';
}
