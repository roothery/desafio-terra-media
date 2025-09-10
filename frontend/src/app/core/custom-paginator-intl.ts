import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';

@Injectable()
export class CustomMatPaginatorIntl extends MatPaginatorIntl {
  constructor() {
    super();
    this.getAndSetInternationalization();
  }

  getAndSetInternationalization() {
    this.itemsPerPageLabel = 'Itens por página:';
    this.firstPageLabel = 'Primeira página';
    this.lastPageLabel = 'Última página';
    this.nextPageLabel = 'Página seguinte';
    this.previousPageLabel = 'Página anterior';
    this.getRangeLabel = (page: number, pageSize: number, length: number) => {
      return `${page * pageSize + 1} - ${Math.min(
        length,
        (page + 1) * pageSize
      )} de ${length}`;
    };
  }
}
