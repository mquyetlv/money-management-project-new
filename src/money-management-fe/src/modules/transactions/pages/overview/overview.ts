import { Component, inject } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Table } from '../../../../core/components';
import { ColumnTable } from '../../../../core/interfaces';

@Component({
  selector: 'app-overview',
  imports: [
    Table,
  ],
  templateUrl: './overview.html',
  styleUrl: './overview.css'
})
export class Overview {
  domSantize = inject(DomSanitizer);

  columns: ColumnTable[] = [
    { headerName: "User name", key: "" }
  ]

  ngOnInit() {
    this.domSantize
  }
}
