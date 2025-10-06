import { Component, Input } from '@angular/core';
import { ColumnTable } from '../../interfaces';

@Component({
  selector: 'mon-table',
  imports: [],
  templateUrl: './table.html',
  styleUrl: './table.css'
})
export class Table {
  @Input() colums: ColumnTable[] = [];
  @Input() data: any[] = [];
  @Input() showCheckbox = false;

  
}
