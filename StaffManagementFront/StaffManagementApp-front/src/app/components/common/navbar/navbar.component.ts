import { StaffServices } from 'src/app/services/staff.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  keyword!: string

  constructor(private staffServices: StaffServices) { }

  ngOnInit(): void {
  }

  search(keyword: any) {

    this.staffServices.onKeyUpSerch(keyword.target.value)
  }
}
