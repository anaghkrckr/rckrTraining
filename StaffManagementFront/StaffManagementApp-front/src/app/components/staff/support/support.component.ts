import { StaffBase } from './../../staffbase/staff-base';
import { Component, Input, OnInit, Output, EventEmitter, SimpleChanges, SimpleChange } from '@angular/core';
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
  function!: Function
  confirmation: boolean = false;

  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    supportDepartment: "Support Department"
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

  eventConfirmed(value: any) {
    this.confirmation = false;
    if (value) {
      this.function()
    }
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
    this.confirmation = true
    this.function = () => super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder[key] = !this.sorting.sortOrder[key];
  }


}
