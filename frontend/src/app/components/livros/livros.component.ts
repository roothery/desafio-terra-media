import { Component, OnInit, ViewChild } from '@angular/core';
import { OpenLibraryService } from '../../core/services/http/open-library.service';
import { MatCardModule } from '@angular/material/card';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DocsResponse } from '../../core/models/search-custom-response.model';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-livros',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatPaginatorModule, MatTableModule],
  templateUrl: './livros.component.html',
  styleUrl: './livros.component.scss',
})
export class LivrosComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<DocsResponse>([]);
  displayedColumns: string[] = ['titulo', 'autor', 'anoPublicacao'];

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
        this.paginator._intl.itemsPerPageLabel = 'Itens por página:';
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
    });
  }
}
