import { Teacher } from './../../model/teacher';
import { StaffServices } from '../../services/staff.service';
import { ChangeDetectorRef, Component, Input, IterableDiffers, OnInit, Output } from '@angular/core';
import { Staff } from 'src/app/model/staff';
import { StaffBase } from '../staffbase/staff-base';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent extends StaffBase implements OnInit {

  updateStaffId!: number;
  staffs!: Staff[];
  pageStart: number = 0
  pageEnd: number = 5
  pageData = { page: 1 };
  searchKeyword!: string;
  confirmation: boolean = false;


  tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    staffType: "staff Type"
  }


  sorting: any = {
    currentSortKey: "staffId",
    sortOrder: false
  }


  sortIcons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
  }
  staffToDelete!: Staff[];



  constructor(private services: StaffServices, private cdref: ChangeDetectorRef) {
    super(services)
  }

  ngOnInit(): void {

    this.services.staffTypes.selected = "All"
    this.services.getStaffs().subscribe(response => {
      this.staffs = response;
      // this.services.onNotification("staff addded")
    })

    this.services.$successEvent.subscribe(resp => {
      this.staffs = resp as Staff[];
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
  sortKey: boolean = false;


  sortList(key: any) {
    let sortStaff = [...this.staffs]
    sortStaff?.sort((a: any, b: any) => {
      return this.sorting.sortOrder ? ((b[key] > a[key]) ? 1 : ((a[key] > b[key]) ? -1 : 0)) : ((a[key] > b[key]) ? 1 : ((b[key] > a[key]) ? -1 : 0));
    })
    this.staffs = [...sortStaff];
  }


  sortTable(key: any) {
    this.sorting.currentSortKey = key;
    this.sorting.sortOrder = !this.sorting.sortOrder;
    this.sortList(key)
  }
}
