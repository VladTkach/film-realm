import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.sass']
})
export class PaginationComponent implements OnChanges{
  @Input() totalItems: number = 0;
  @Input() pageSize: number = 10;
  @Output() pageChanged: EventEmitter<number> = new EventEmitter<number>();

  currentPage: number = 1;
  totalPages: number = 0;
  visiblePages: number[] = [];

  ngOnChanges(): void {
    this.calculateTotalPages();
    this.setPageNumbers();
    this.updateVisiblePages();
  }

  calculateTotalPages(): void {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
  }

  setPageNumbers(): void {
    this.visiblePages = [];
    for (let i = 1; i <= this.totalPages; i++) {
      this.visiblePages.push(i);
    }
  }

  updateVisiblePages(): void {
    const totalPagesToShow = Math.min(5, this.totalPages);
    let startPage: number;

    if (this.currentPage <= totalPagesToShow - 2) {
      startPage = 1;
    } else if (this.currentPage >= this.totalPages - 2) {
      startPage = this.totalPages - (totalPagesToShow - 1);
    } else {
      startPage = this.currentPage - 2;
    }

    this.visiblePages = Array.from({ length: totalPagesToShow }, (_, i) => startPage + i);
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.pageChanged.emit(this.currentPage);
    this.updateVisiblePages();
  }
}
