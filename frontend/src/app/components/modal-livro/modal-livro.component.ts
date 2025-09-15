import { AfterViewInit, Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { DocsResponse } from '../../core/models/search-custom-response.model';
import lottie from 'lottie-web';

@Component({
  selector: 'app-modal-livro',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatTabsModule],
  templateUrl: './modal-livro.component.html',
  styleUrl: './modal-livro.component.scss',
})
export class ModalLivroComponent implements OnInit, AfterViewInit {
  detalhesLivro: DocsResponse | null = null;
  imagemLivro: string = 'assets/images/livro-nao-encontrado.png';

  constructor(
    public bsModalRef: BsModalRef,
    private openLibraryService: OpenLibraryService
  ) {}

  ngOnInit() {
    if (this.detalhesLivro !== null) {
      this.loadBookCover();
    }
  }

  ngAfterViewInit() {
    setTimeout(() => {
      lottie.loadAnimation({
        container: document.getElementById('animacao')!,
        renderer: 'svg',
        loop: true,
        autoplay: true,
        path: 'assets/images/em-construcao.json',
      });
    });
  }

  loadBookCover() {
    this.openLibraryService
      .getBookCoverUrl(
        this.detalhesLivro!.capaId,
        this.detalhesLivro!.codigoCapa
      )
      .subscribe({
        next: (response) => {
          this.imagemLivro = response;
        },
        error: (error) => {
          console.error('Erro ao carregar imagem do livro:', error);
        },
      });
  }

  close() {
    this.bsModalRef.hide();
  }
}
