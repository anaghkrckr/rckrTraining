import { Component, Input, OnInit, SimpleChange } from '@angular/core';
import { Staff } from 'src/app/model/staff';
import { StaffServices } from 'src/app/services/staff.service';

@Component({
  selector: 'app-popupform',
  templateUrl: './popupform.component.html',
  styleUrls: ['./popupform.component.css']
})
export class PopupformComponent implements OnInit {

  constructor(private staffServices: StaffServices) { }

  staff: any = {
    staffId: "",
    staffName: "",
    staffAge: "",
    staffType: "",
    departmentName: "",
    department: ""
  };
  hide: string = "none";
  staffTypes!: any;

  openForm() {
    this.hide = "block"
  }

  closeForm() {
    this.hide = "none";
  }
  changeValue(staff: any) {
    if (staff != undefined) {
      this.staff = {
        staffId: staff.staffId,
        staffName: staff.staffName,
        staffAge: staff.staffAge,
        staffType: staff.staffType,
        departmentName: this.staffTypes.staffs[staff.staffType].name,
        department: (staff.subject || staff.supportDepartment || staff.administratorDepartment),
      };
    }
  }

  onStaffTypeChange(staffType: string) {
    this.staff.staffType = staffType;
    this.staff.departmentName = this.staffTypes.staffs[staffType].name
  }


  onSubmit(staff: any) {
    let updatedStaff = {
      staffId: staff.staffId,
      staffName: staff.staffName,
      staffAge: staff.staffAge,
      staffType: staff.staffType,
      [this.staffTypes.staffs[staff.staffType].objName]: staff.department,

    }
    if (updatedStaff.staffId > 0) {
      this.staffServices.updateStaff(updatedStaff).subscribe(Response => {
        this.staffServices.getStaff(this.staffServices.staffTypes.selected).subscribe(resp => {
          this.staffServices.onSuccessEvent(resp)
        })
      })
    } else {

      this.staffServices.addStaff(updatedStaff).subscribe(Response => {
        this.staffServices.getStaff(this.staffServices.staffTypes.selected).subscribe(resp => {
          this.staffServices.onSuccessEvent(resp)
        })
      })

    }

  }

  ngOnInit(): void {
    this.staffTypes = this.staffServices.staffTypes;
    this.staffServices.$popupEvent.subscribe(response => {
      this.changeValue(response)
      this.staffTypes.selected = response.staffType;
      this.openForm();
    })
  }

}
