import { StaffBase } from './../../staffbase/staff-base';
import { Component, Input, OnInit, Output, EventEmitter, SimpleChanges, SimpleChange, ChangeDetectorRef } from '@angular/core';
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
  pageStart!: number
  pageEnd!: number
  pageData = { page: 1 };
  searchKeyword!: string;
  staffToDelete!: Support[];
  confirmation: boolean = false;

  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    supportDepartment: "Support Department"
  }



  sorting: any = {
    currentSortKey: "staffId",
    sortOrder: false
  }

  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }

  constructor(private services: StaffServices, private cdref: ChangeDetectorRef) {
    super(services)
  }

  eventConfirmed(value: any) {
    this.confirmation = false;
    if (value) {
      super.deleteStaff(this.staffToDelete as any)
    }
  }


  ngOnInit(): void {
    this.services.staffTypes.selected = "Support"
    this.services.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.supporters = response;

    })
    this.services.$successEvent.subscribe(resp => {
      this.supporters = resp as Support[];
      this.sortList(this.sorting.currentSortKey)

    })

  }

  ngAfterContentChecked() {
    this.cdref.detectChanges()
  }

  onSearch(searchKeyword: string): void {
    this.searchKeyword = searchKeyword;
  }

  preserveTableHeadOrder() {
    return 0;
  }

  changePage(pageData: any) {
    this.pageStart = (pageData.page - 1) * pageData.itemsPerPage
    this.pageEnd = pageData.page * pageData.itemsPerPage
  }


  updateStaff(staff: Staff) {
    this.services.onPopupClick(staff)
  }


  addToDeleteList(staff: Support) {
    event?.stopImmediatePropagation()
    this.services.staffDeleteList.push(staff)
  }

  deleteStaff(staff: Support[]) {
    event?.stopImmediatePropagation()
    this.staffToDelete = staff;
    this.confirmation = true
  }

  sortList(key: any) {
    let sortStaff = [...this.supporters]
    sortStaff?.sort((a: any, b: any) => {
      return this.sorting.sortOrder ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    })
    this.supporters = [...sortStaff];
    console.log(this.staffs)
  }


  sortTable(key: any) {
    console.log((key))
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder = !this.sorting.sortOrder;
    this.sortList(key)
  }


}
