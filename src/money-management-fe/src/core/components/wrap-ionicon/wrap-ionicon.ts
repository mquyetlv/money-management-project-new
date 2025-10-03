import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';

@Component({
  selector: 'mon-wrap-ionicon',
  imports: [],
  templateUrl: './wrap-ionicon.html',
  styleUrl: './wrap-ionicon.css',
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class WrapIonicon {
  @Input({ required: true} ) iconName: string = '';
  @Input() fontSize = '16px';
}
