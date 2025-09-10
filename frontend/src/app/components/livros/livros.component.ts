import { Component, OnInit, ViewChild } from '@angular/core';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DocsResponse } from '../../core/models/search-custom-response.model';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-livros',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
  ],
  templateUrl: './livros.component.html',
  styleUrl: './livros.component.scss',
})
export class LivrosComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<DocsResponse>([]);
  displayedColumns: string[] = ['titulo', 'autor', 'anoPublicacao', 'acoes'];

  constructor(private openLibraryService: OpenLibraryService) {}

  ngOnInit() {
    this.openLibraryService.searchBooksByAuthor().subscribe({
      next: (response) => {
        this.dataSource.data = response.docs;
      },
      error: (error) => {
        console.error('Erro ao carregar livros:', error);
      },
      complete: () => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.filterPredicate = function (
          data,
          filter: string
        ): boolean {
          return (
            data.titulo.toLowerCase().includes(filter) ||
            data.autor.join(', ').toLowerCase().includes(filter)
          );
        };
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
