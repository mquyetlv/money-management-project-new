import { Component, inject } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-overview',
  imports: [
  ],
  templateUrl: './overview.html',
  styleUrl: './overview.css'
})
export class Overview {
  domSantize = inject(DomSanitizer);

  ngOnInit() {
    this.domSantize
  }
}
