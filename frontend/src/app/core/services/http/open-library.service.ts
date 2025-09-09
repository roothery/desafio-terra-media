import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, map, Observable, of } from 'rxjs';
import { SearchCustomResponse } from '../../models/search-custom-response.model';
import { LoadingService } from '../loading.service';

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
          docs: response.docs.map((doc: any) => ({
            titulo: doc.title,
            autor: doc.author_name,
            codigoAutor: doc.author_key,
            anoPublicacao: doc.first_publish_year,
            codigoCapa: doc.cover_edition_key || null,
            capaId: doc.cover_i || null,
          })),
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
}
