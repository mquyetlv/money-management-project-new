import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MonInout } from '../../core/components/input/input';

@Component({
  selector: 'app-header',
  imports: [
    MonInout,
  ],
  templateUrl: './header.html',
  styleUrl: './header.css',
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class Header {

}
