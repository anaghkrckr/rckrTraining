import { StaffBase } from './../../staffbase/staff-base';
import { Component, Input, OnInit, Output, EventEmitter, SimpleChange } from '@angular/core';
import { Staff } from 'src/app/model/staff';
import { Support } from 'src/app/model/support';
import { StaffServices } from 'src/app/services/staff.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent extends StaffBase implements OnInit {
  supporters!: Support[];

  constructor(private services: StaffServices) {
    super(services)
  }


  ngOnInit(): void {
    this.services.staffTypes.selected = "Support"
    this.services.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.supporters = response;
    })
    this.services.$successEvent.subscribe(resp => {
      this.supporters = resp as Support[];
    })
  }



  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    supportDepartment: "Support Department"
  }

  sortOrder: any = { Id: 0, Name: 0, Age: 0, Type: 0 }

  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }

  preserveTableHeadOrder() {
    return 0;
  }

  ngOnChanges(changes: SimpleChange) {
    if (this.supporters != undefined) {
      this.supporters.sort((a: any, b: any) => {
        return this.sortOrder[this.currentSortKey] ? ((b[this.currentSortKey] > a[this.currentSortKey]) ? 1 : ((a[this.currentSortKey] > b[this.currentSortKey]) ? -1 : 0)) : ((a[this.currentSortKey] > b[this.currentSortKey]) ? 1 : ((b[this.currentSortKey] > a[this.currentSortKey]) ? -1 : 0));
      });
    }
  }

  currentSortKey: string = "Id";



  updateStaff(staff: Staff) {
    this.services.onPopupClick(staff)
  }


  addToDeleteList(staff: Support) {
    event?.stopImmediatePropagation()
    this.services.staffDeleteList.push(staff)
  }

  deleteStaff(staff: Support[]) {
    event?.stopImmediatePropagation()
    super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.currentSortKey = key;
    this.sortOrder[key] = !this.sortOrder[key];
    this.supporters.sort((a: any, b: any) => {
      return this.sortOrder[key] ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    });

  }


}
