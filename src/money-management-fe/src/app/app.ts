import { Component, CUSTOM_ELEMENTS_SCHEMA, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from '../layouts/header/header';
import { SideBar } from '../layouts/side-bar/side-bar';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    Header,
    SideBar,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('money-management-fe');
}
