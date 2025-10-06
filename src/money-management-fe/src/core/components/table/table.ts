import { Component, Input } from '@angular/core';
import { ColumnTable } from '../../interfaces';
import { Pagination } from '../pagination/pagination';

@Component({
  selector: 'mon-table',
  imports: [
    Pagination,
  ],
  templateUrl: './table.html',
  styleUrl: './table.css'
})
export class Table {
  @Input() colums: ColumnTable[] = [];
  @Input() data: any[] = [];
  @Input() showCheckbox = false;

  
}
