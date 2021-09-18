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
  pageStart: number = 0
  pageEnd: number = 5
  pageData = { page: 1 };
  searchKeyword!: string;
  function!: Function
  confirmation: boolean = false;


  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    subject: "Subject"
  }


  sorting: any = {
    currentSortKey: "staffId",
    sortOrder: {}
  }


  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }


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

  changePage(pageData: any) {
    this.pageStart = (pageData.page - 1) * pageData.itemsPerPage
    this.pageEnd = pageData.page * pageData.itemsPerPage
  }

  eventConfirmed(value: any) {
    this.confirmation = false;
    if (value) {
      this.function()
    }
  }

  onSearch(keyword: string): void {
    this.searchKeyword = keyword;
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
    this.confirmation = true
    this.function = () => super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder[key] = !this.sorting.sortOrder[key];
  }



}
