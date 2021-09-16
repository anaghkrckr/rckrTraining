import { Staff } from 'src/app/model/staff';
import { Teacher } from './../../../model/teacher';
import { Component, Input, OnInit, Output, SimpleChange } from '@angular/core';
import { StaffServices } from 'src/app/services/staff.service';
import { StaffBase } from '../../staffbase/staff-base';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent extends StaffBase implements OnInit {
  teachers!: Teacher[];

  constructor(private services: StaffServices) {
    super(services)
  }


  ngOnInit(): void {

    this.services.staffTypes.selected = "Teacher"
    super.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.teachers = response;
    })

    this.services.$successEvent.subscribe(resp => {
      this.teachers = resp as Teacher[];
    })
  }



  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    subject: "Subject"
  }



  ngOnChanges(changes: SimpleChange) {
    if (this.teachers != undefined) {
      this.teachers.sort((a: any, b: any) => {
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

  addToDeleteList(staff: Teacher) {
    event?.stopImmediatePropagation()
    this.services.staffDeleteList.push(staff)
  }

  deleteStaff(staff: Teacher[]) {
    event?.stopImmediatePropagation()
    super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.currentSortKey = key;
    this.sortOrder[key] = !this.sortOrder[key];
    this.teachers.sort((a: any, b: any) => {
      return this.sortOrder[key] ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    });
  }



}
