import { Component } from '@angular/core';
import { LoginService } from '../login.service'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers: [LoginService]
})
export class NavMenuComponent {
  isExpanded = false;
  loggedIn = false;

  constructor(private LoginService : LoginService){

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
