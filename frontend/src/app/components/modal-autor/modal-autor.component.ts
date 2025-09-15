import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { OpenLibraryService } from '../../core/services/http/open-library.service';

@Component({
  selector: 'app-modal-autor',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './modal-autor.component.html',
  styleUrl: './modal-autor.component.scss',
})
export class ModalAutorComponent {
  codigoAutor: string | null = null;
  nomeAutor: string | null = null;
  nome: string = '';
  bio: string = '';

  constructor(
    public bsModalRef: BsModalRef,
    private openLibraryService: OpenLibraryService
  ) {}

  ngOnInit() {
    this.loadAuthorBiography();
  }

  loadAuthorBiography() {
    if (this.codigoAutor) {
      this.openLibraryService.getAuthorBiography(this.codigoAutor).subscribe({
        next: (response) => {
          this.nome =
            response.name === 'Desconhecido'
              ? this.nomeAutor || 'Desconhecido'
              : response.name;
          this.bio = this.formatarTexto(response.bio);
        },
        error: (error) => {
          console.error('Erro ao carregar biografia do autor:', error);
        },
      });
    }
  }

  formatarTexto(texto: string): string {
    return texto.replace(/\r\n/g, '<p></p>').replace(/\n/g, '<p></p>');
  }

  close() {
    this.bsModalRef.hide();
  }
}
