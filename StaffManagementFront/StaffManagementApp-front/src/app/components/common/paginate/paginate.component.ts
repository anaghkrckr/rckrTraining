import { Component, EventEmitter, Input, OnInit, Output, SimpleChange } from '@angular/core';

@Component({
  selector: 'app-paginate',
  templateUrl: './paginate.component.html',
  styleUrls: ['./paginate.component.css']
})
export class PaginateComponent implements OnInit {


  @Input() totalItems!: any;
  totalPages: number = 1
  itemsPerPage: number = 9;
  pages: number[] = Array.from(Array(this.totalPages), (x, i) => i + 1)
  @Output() activePage = new EventEmitter();
  currentPage: number = 1;


  constructor() { }

  ngOnInit(): void {
    this.activePage.emit({ page: this.currentPage, itemsPerPage: this.itemsPerPage })
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage)
    this.pages = Array.from(Array(this.totalPages), (x, i) => i + 1)
  }

  ngOnChanges(changes: SimpleChange) {
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage)
    this.pages = Array.from(Array(this.totalPages), (x, i) => i + 1)
    if (this.currentPage > this.totalPages && this.totalItems >= this.itemsPerPage) {
      this.currentPage = this.currentPage - 1
      this.activePage.emit({ page: this.currentPage, itemsPerPage: this.itemsPerPage })
    }
  }

  openPage(page: number) {
    this.currentPage = page
    this.activePage.emit({ page, itemsPerPage: this.itemsPerPage })
  }

}
