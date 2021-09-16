import { StaffBase } from './components/staffbase/staff-base';
import { Staff } from 'src/app/model/staff';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent extends StaffBase {
  title = 'Staff Management App';
  value = "";

  onEnter() {
    this.title = this.value;
  }
  addStaff() {
    super.addStaff()
  }

  deleteStaff() {
    super.deleteStaff();
  }

}
