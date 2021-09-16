import { StaffBase } from './../../staffbase/staff-base';
import { Component, Input, OnInit, Output, SimpleChange } from '@angular/core';
import { Administrator } from 'src/app/model/administrator';
import { Staff } from 'src/app/model/staff';
import { StaffServices } from 'src/app/services/staff.service';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent extends StaffBase implements OnInit {

  administrators!: Administrator[];
  constructor(private services: StaffServices) {
    super(services)
  }

  ngOnInit(): void {
    this.services.staffTypes.selected = "Administrator"
    super.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.administrators = response;
    })

    this.services.$successEvent.subscribe(resp => {
      this.administrators = resp as Administrator[];
    })
  }


  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    administratorDepartment: "Administrator Department"
  }
  ngOnChanges(changes: SimpleChange) {
    if (this.administrators != undefined) {
      this.administrators.sort((a: any, b: any) => {
        return this.sortOrder[this.currentSortKey] ? ((b[this.currentSortKey] > a[this.currentSortKey]) ? 1 : ((a[this.currentSortKey] > b[this.currentSortKey]) ? -1 : 0)) : ((a[this.currentSortKey] > b[this.currentSortKey]) ? 1 : ((b[this.currentSortKey] > a[this.currentSortKey]) ? -1 : 0));
      });
    }
  }

  currentSortKey: string = "Id";

  sortOrder: any = { Id: 0, Name: 0, Age: 0, Type: 0 }

  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }

  preserveTableHeadOrder() {
    return 0;
  }


  updateStaff(staff: Staff) {
    this.services.onPopupClick(staff)
  }

  addToDeleteList(staff: Administrator) {
    event?.stopImmediatePropagation()
    this.services.staffDeleteList.push(staff)
  }


  deleteStaff(staff: Administrator[]) {
    event?.stopImmediatePropagation()
    super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.currentSortKey = key;
    this.sortOrder[key] = !this.sortOrder[key];
    this.administrators.sort((a: any, b: any) => {
      return this.sortOrder[key] ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    });

  }



}
