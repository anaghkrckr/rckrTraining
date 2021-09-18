import { StaffBase } from './../../staffbase/staff-base';
import { Component, Input, OnInit, Output, SimpleChange } from '@angular/core';
import { Administrator } from 'src/app/model/administrator';
import { Staff } from 'src/app/model/staff';
import { StaffServices } from 'src/app/services/staff.service';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent extends StaffBase implements OnInit {




  administrators!: Administrator[];
  pageStart: number = 0
  pageEnd: number = 5
  pageData = { page: 1 };

  searchKeyword!: string;
  function!: Function
  confirmation: boolean = false;

  constructor(private services: StaffServices) {
    super(services)
  }

  sort = false

  ngOnInit(): void {
    this.services.staffTypes.selected = "Administrator"
    super.getStaff(this.services.staffTypes.selected).subscribe(response => {
      this.administrators = response;
    })

    this.services.$successEvent.subscribe(resp => {
      this.administrators = resp as Administrator[];
    })
  }

  changePage(pageData: any) {
    this.pageStart = (pageData.page - 1) * pageData.itemsPerPage
    this.pageEnd = pageData.page * pageData.itemsPerPage
  }

  onSearch(searchKeyword: string): void {
    this.searchKeyword = searchKeyword;
  }

  eventConfirmed(value: any) {
    this.confirmation = false;
    if (value) {
      this.function()
    }
  }


  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    administratorDepartment: "Administrator Department"
  }

  sorting: any = {
    currentSortKey: "staffId",
    sortOrder: {}
  }


  sortIcons: any = {
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
    this.confirmation = true
    this.function = () => super.deleteStaff(staff)
  }

  sortTable(key: any) {
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder[key] = !this.sorting.sortOrder[key];
  }

}
