import { StaffServices } from 'src/app/services/staff.service';
import { Component, OnInit, SimpleChange } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  activeLink: string = "home";
  keyword!: string
  currentStaff: string = "All"

  constructor(private staffServices: StaffServices) { }

  ngOnInit(): void {
    this.currentStaff = this.staffServices.staffTypes.selected;
  }

  ngOnChanges(changes: SimpleChange) {
    console.log("changed", changes)
    console.log(this.currentStaff);
    console.log(this.staffServices.staffTypes.selected)
  }

  search(keyword: any) {

    this.staffServices.onKeyUpSerch(keyword.target.value)
  }
}
