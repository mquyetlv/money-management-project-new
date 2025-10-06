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
    { headerName: "User name", key: "username" },
    { headerName: "Full name", key: "fullname" },
    { headerName: "Email", key: "email" },
    { headerName: "Phone number", key: "phoneNumber", align: 'right' },
    { headerName: "Something", key: "something" },
  ];

  data: any = [
    { username: "quyetlv1", fullname: "Le Van Quyet1", email: "lequyet1@gmail.com" , phoneNumber: "09432432424", something: "something1" },
    { username: "quyetlv2", fullname: "Le Van Quyet2", email: "lequyet2@gmail.com", phoneNumber: "094324324242", something: "something2" },
    { username: "quyetlv3", fullname: "Le Van Quyet3", email: "lequyet3@gmail.com", phoneNumber: "094324324243", something: "something3" },
    { username: "quyetlv4", fullname: "Le Van Quyet4", email: "lequyet4@gmail.com", phoneNumber: "094324324244", something: "something4" },
    { username: "quyetlv4", fullname: "Le Van Quyet1", email: "lequyet1@gmail.com", phoneNumber: "094324324241", something: "something1" },
    { username: "quyetlv5", fullname: "Le Van Quyet5", email: "lequyet5@gmail.com", phoneNumber: "094324324245", something: "something5" },
    { username: "quyetlv6", fullname: "Le Van Quyet6", email: "lequyet6@gmail.com", phoneNumber: "094324324246", something: "something6" },
    { username: "quyetlv7", fullname: "Le Van Quyet7", email: "lequyet7@gmail.com", phoneNumber: "094324324247", something: "something7" },
    { username: "quyetlv8", fullname: "Le Van Quyet8", email: "lequyet8@gmail.com", phoneNumber: "094324324248", something: "something8" },
    { username: "quyetlv9", fullname: "Le Van Quyet9", email: "lequyet9@gmail.com", phoneNumber: "094324324249", something: "something9" },
    { username: "quyetlv10", fullname: "Le Van Quyet10", email: "lequyet10@gmail.com", phoneNumber: "0943243242410", something: "something10" },
    { username: "quyetlv11", fullname: "Le Van Quyet11", email: "lequyet11@gmail.com", phoneNumber: "0943243242411", something: "something11" },
  ]

  ngOnInit() {
    this.domSantize
  }
}
