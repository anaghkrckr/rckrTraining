import { Support } from './../../model/support';
import { Administrator } from './../../model/administrator';
import { Teacher } from './../../model/teacher';
import { StaffServices } from '../../services/staff.service';
import { Component, Input, IterableDiffers, OnInit, Output } from '@angular/core';
import { Staff } from 'src/app/model/staff';
import { StaffBase } from '../staffbase/staff-base';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent extends StaffBase implements OnInit {

  staffs!: Staff[];
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
    staffType: "staff Type"
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

    this.services.staffTypes.selected = "All"
    this.services.getStaffs().subscribe(response => {
      this.staffs = response;
    })

    this.services.$successEvent.subscribe(resp => {
      this.staffs = resp as Staff[];
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
