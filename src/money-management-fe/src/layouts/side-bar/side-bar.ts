import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { WrapIonicon } from '../../core/components/wrap-ionicon/wrap-ionicon';
import { Menu } from '../../core/interfaces';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  imports: [
    CommonModule,
    RouterLink,
    WrapIonicon,
    RouterLinkActive,
],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css'
})
export class SideBar {
  menus: Menu[] = [
    {
      id: '1',
      name: "Tổng quan",
      url: "/overview",
      icon: "grid-outline",
      openning : false,
    },
    {
      id: '2',
      name: "Quản trị",
      url: "/management",
      icon: "settings-outline",
      openning : false,
      children: [
        { id: '21', name: "User", icon: "people-outline", url: "/management/users", openning : false, },
        { id: '22', name: "Function", icon: "build-outline", url: "/management/functions", openning : false, },
        {
          id: '3',
          name: "Menu cấp 3",
          icon: "timer-outline",
          openning : false,
          children: [
            { id: '31', name: "Cấp 3.1", icon: "grid-outline", url: "/management/level3/test1", openning : false, },
            { id: '32', name: "Cấp 3.2", icon: "grid-outline", url: "/management/level3/test2", openning : false, },
            { id: '33', name: "Cấp 3.3", icon: "grid-outline", url: "/management/level3/test3", openning : false, },
          ]
        }
      ]
    },
    {
      id: '4',
      name: "Giao dịch",
      url: "/transactions",
      icon: "grid-outline",
      openning : false,
    },
    {
      id: '5',
      name: "Ví",
      url: "/wallet",
      icon: "wallet-outline",
      openning : false,
    },
  ]
}
