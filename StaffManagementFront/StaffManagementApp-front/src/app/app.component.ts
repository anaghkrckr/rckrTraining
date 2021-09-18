import { StaffBase } from './components/staffbase/staff-base';
import { Staff } from 'src/app/model/staff';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent extends StaffBase {
  onSearch(keyword: string): void {
  }
  title = 'Staff Management App';
  value = "";
  confirmation: boolean = false;
  function!: Function;


  eventConfirmed(value: boolean) {
    this.confirmation = false
    if (value == true) {
      this.function()
    }
  }

  onEnter() {
    this.title = this.value;
  }
  addStaff() {
    super.addStaff()
  }

  deleteStaff() {
    this.function = () => super.deleteStaff();
    this.confirmation = true
  }


}
