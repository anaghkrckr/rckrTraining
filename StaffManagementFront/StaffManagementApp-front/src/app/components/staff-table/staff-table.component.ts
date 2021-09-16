import { KeyValue } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-staff-table',
  templateUrl: './staff-table.component.html',
  styleUrls: ['./staff-table.component.css']
})
export class StaffTableComponent implements OnInit {

  constructor() { }

  @Input() staffs: any;
  @Input() department: string = "";

  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    staffType: "Type",
    department: ""
  }

  sortOrder: any = { Id: 0, Name: 0, Age: 0, Type: 0 }

  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }

  preserveTableHeadOrder() {
    return 0;
  }


  ngOnChanges() {
  }

  updateStaff(staff: Object) {
    console.log(staff)
  }

  deleteStaff(staffId: number) {
    let index = this.staffs.findIndex((staff: any) => staff.staffId == staffId)
    this.staffs.splice(index, 1)
  }

  sortTable(key: any) {
    this.sortOrder[key] = !this.sortOrder[key];
    if (key == "department") {
      this.staffs.sort((a: any, b: any) => {
        return this.sortOrder[key] ? (((b.subject || b.administratorDepartment || b.supportDepartment) > (a.subject || a.administratorDepartment || a.supportDepartment)) ? 1 : (((a.subject || a.administratorDepartment || a.supportDepartment) > (b.subject || b.administratorDepartment || b.supportDepartment)) ? -1 : 0)) : (((a.subject || a.administratorDepartment || a.supportDepartment) > (b.subject || b.administratorDepartment || b.supportDepartment)) ? 1 : (((b.subject || b.administratorDepartment || b.supportDepartment) > (a.subject || a.administratorDepartment || a.supportDepartment)) ? -1 : 0));
      });
    } else {
      this.staffs.sort((a: any, b: any) => {
        return this.sortOrder[key] ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
      });
    }
  }


  ngOnInit(): void {
    console.log(this.staffs)
    this.tableHead.department = this.department;
  }

}
