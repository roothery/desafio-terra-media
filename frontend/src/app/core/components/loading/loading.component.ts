import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoadingService } from '../../services/loading.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule, AsyncPipe, MatProgressSpinnerModule],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss',
})
export class LoadingComponent {
  loading$: Observable<boolean>;

  constructor(private loadingService: LoadingService) {
    this.loading$ = this.loadingService.loading$;
  }
}
