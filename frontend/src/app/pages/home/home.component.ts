import { Component } from '@angular/core';
import { MenuSidebarComponent } from '../../components/menu-sidebar/menu-sidebar.component';
import { LivrosComponent } from '../../components/livros/livros.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MenuSidebarComponent, LivrosComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  titulo: string = 'Livros de J.R.R. Tolkien';
  subtitulo: string =
    'Descubra a magia da Terra-Média através das obras do mestre da fantasia';
}
