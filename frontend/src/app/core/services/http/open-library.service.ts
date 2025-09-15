import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, map, Observable, of } from 'rxjs';
import {
  DocsResponse,
  SearchCustomResponse,
} from '../../models/search-custom-response.model';
import { LoadingService } from '../loading.service';
import { AuthorResponse } from '../../models/authorResponse.model';

@Injectable({
  providedIn: 'root',
})
export class OpenLibraryService {
  private readonly baseUrl = 'https://openlibrary.org';
  private readonly jrrTolkienSearch = 'OL26320A';

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService
  ) {}

  searchBooksByAuthor(
    authorKey: string | null = null
  ): Observable<SearchCustomResponse> {
    this.loadingService.show();
    const searchKey = authorKey || this.jrrTolkienSearch;
    const url = `${this.baseUrl}/search.json?author=${searchKey}&fields=key,title,author_name,author_key,cover_edition_key,cover_i,first_publish_year&sort=new`;
    return this.http.get<any>(url).pipe(
      map((response) => {
        return {
          docs: response.docs.map(
            (doc: any) =>
              ({
                titulo: doc.title,
                autor: doc.author_name,
                autorFormatado: doc.author_name.join(', '),
                codigoAutor: doc.author_key,
                anoPublicacao: doc.first_publish_year,
                codigoCapa: doc.cover_edition_key || null,
                capaId: doc.cover_i || null,
              } as DocsResponse)
          ),
          total: response.docs.length,
        } as SearchCustomResponse;
      }),
      catchError((error) => {
        console.error('Erro ao obter livros:', error);
        return of({ docs: [], total: 0 } as SearchCustomResponse);
      }),
      finalize(() => this.loadingService.hide())
    );
  }

  getAuthorBiography(authorId: string): Observable<AuthorResponse> {
    this.loadingService.show();
    const url = `${this.baseUrl}/authors/${authorId}.json`;
    return this.http.get<any>(url).pipe(
      map((response) => {
        return {
          name: response.name,
          bio: response.bio.value,
        } as AuthorResponse;
      }),
      catchError((error) => {
        console.error('Erro ao obter biografia do autor:', error);
        return of({ name: 'Desconhecido', bio: 'Biografia não disponível' });
      }),
      finalize(() => this.loadingService.hide())
    );
  }

  getBookCoverUrl(
    coverId: string,
    coverCode: string,
    size: 'S' | 'M' | 'L' = 'L'
  ): Observable<string> {
    this.loadingService.show();
    const baseUrl = 'https://covers.openlibrary.org/b';
    const imagemDefault = 'assets/images/livro-nao-encontrado.png';
    let coverUrl: string;

    if (coverId) {
      coverUrl = `${baseUrl}/id/${coverId}-${size}.jpg`;
    } else if (coverCode) {
      coverUrl = `${baseUrl}/olid/${coverCode}-${size}.jpg`;
    } else {
      coverUrl = imagemDefault;
    }

    return this.http.get(coverUrl, { responseType: 'blob' }).pipe(
      map(() => coverUrl),
      catchError((error) => {
        console.error('Erro ao obter capa do livro:', error);
        return of(imagemDefault);
      }),
      finalize(() => this.loadingService.hide())
    );
  }
}
