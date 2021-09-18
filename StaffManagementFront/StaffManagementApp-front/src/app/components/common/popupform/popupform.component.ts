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

  formType: string = "null"

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
    console.log((this.staffTypes.staffs))
    this.hide = "block"
  }

  closeForm() {
    this.hide = "none";
    this.staff = {
      staffId: "",
      staffName: "",
      staffAge: "",
      staffType: "",
      departmentName: "",
      department: ""
    }
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
    console.log(updatedStaff)

    this.staffServices.addUpdateStaffHelper(updatedStaff)
    this.closeForm()
  }


  ngOnInit(): void {
    this.staffTypes = this.staffServices.staffTypes;
    delete this.staffTypes.staffs.All
    console.log(this.staffTypes.staffs)
    this.staffServices.$popupEvent.subscribe(response => {
      if (response.staffId < 0) {
        this.formType = "Add Form"
        this.staff.staffType = "All";
        this.staff.staffId = response.staffId
      }
      else {
        this.formType = "Update Form"
        this.changeValue(response)
      }
      this.openForm();
    })
  }

}
