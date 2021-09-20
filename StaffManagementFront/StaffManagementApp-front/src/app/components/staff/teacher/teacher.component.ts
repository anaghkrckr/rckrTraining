import { Staff } from 'src/app/model/staff';
import { Teacher } from './../../../model/teacher';
import { ChangeDetectorRef, Component, Input, OnInit, Output, SimpleChange } from '@angular/core';
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
    sortOrder: false
  }

  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }
  staffToDelete!: Teacher[];


  constructor(private services: StaffServices, private cdref: ChangeDetectorRef) {
    super(services)
  }




  ngOnInit(): void {

    this.services.staffTypes.selected = "Teacher"
    super.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.teachers = response;
    })

    this.services.$successEvent.subscribe(resp => {
      this.teachers = resp as Teacher[];
      this.sortList(this.sorting.currentSortKey)
    })
  }

  ngAfterContentChecked() {
    this.cdref.detectChanges()
  }

  changePage(pageData: any) {
    this.pageStart = (pageData.page - 1) * pageData.itemsPerPage
    this.pageEnd = pageData.page * pageData.itemsPerPage
  }

  eventConfirmed(value: any) {
    this.confirmation = false;
    if (value) {
      super.deleteStaff(this.staffToDelete as any)
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
    this.staffToDelete = staff;
    this.confirmation = true
  }

  sortList(key: any) {
    let sortStaff = [...this.teachers]
    sortStaff?.sort((a: any, b: any) => {
      return this.sorting.sortOrder ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    })
    this.teachers = [...sortStaff];
  }


  sortTable(key: any) {
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder = !this.sorting.sortOrder;
    this.sortList(key)
  }



}
