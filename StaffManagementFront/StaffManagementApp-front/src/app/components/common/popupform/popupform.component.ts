import { Support } from './../../../model/support';
import { Administrator } from './../../../model/administrator';
import { Component, Input, OnInit, SimpleChange } from '@angular/core';
import { Staff } from 'src/app/model/staff';
import { Teacher } from 'src/app/model/teacher';
import { StaffServices } from 'src/app/services/staff.service';

@Component({
  selector: 'app-popupform',
  templateUrl: './popupform.component.html',
  styleUrls: ['./popupform.component.css']
})
export class PopupformComponent implements OnInit {

  constructor(private staffServices: StaffServices) { }

  formType: string = "null"

  validationErrors: any = {

    fieldErrors: {
      staffName: {
        error: 1,
        errorMessage: ""
      },
      staffAge: {
        error: 1,
        errorMessage: ""
      },
      department: {
        error: 1,
        errorMessage: ""
      }
    },
    error: 1
  }

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

  validation(event: any, feildType: string) {
    let value = event.target.value;
    switch (feildType) {
      case "staffName":
        if (!isNaN(value)) {
          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Name should not be a number"
        }
        else if (value.length < 3) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "name length should be greater than 3"
        }
        else if (value.length > 30) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "name length should be less than 30"
        }
        else {

          this.validationErrors.fieldErrors[feildType].error = 0
          this.validationErrors.fieldErrors[feildType].errorMessage = ""
        }
        break;
      case "staffAge":
        if (value < 20) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Age should be greater than 20"
        } else if (value > 80) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Age should be less than 80"
        }
        else {

          this.validationErrors.fieldErrors[feildType].error = 0
          this.validationErrors.fieldErrors[feildType].errorMessage = ""
        }

        break;
      case "department":
        if (!isNaN(value)) {
          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Department name should not be a number"
        }
        else if (value.length < 3) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Department name length should be greater than 3"
        }
        else if (value.length > 30) {

          this.validationErrors.fieldErrors[feildType].error = 1
          this.validationErrors.fieldErrors[feildType].errorMessage = "Department name length should be less than 30"
        }
        else {

          this.validationErrors.fieldErrors[feildType].error = 0
          this.validationErrors.fieldErrors[feildType].errorMessage = ""
        }

        break;

    }
    this.validationErrors.error = this.validationErrors.fieldErrors.staffName.error || this.validationErrors.fieldErrors.staffAge.error || this.validationErrors.fieldErrors.department.error
  }


  onSubmit(staff: any) {
    let updated: Staff = {
      staffId: staff.staffId,
      staffAge: staff.staffAge,
      staffName: staff.staffName,
      staffType: staff.staffType
    }
    switch (updated.staffType) {
      case "Teacher":
        (updated as Teacher).subject = staff.department;
        break;
      case "Administrator":
        (updated as Administrator).administratorDepartment = staff.department
        break;
      case "Support":
        (updated as Support).supportDepartment = staff.department
        break;

    }
    this.staffServices.addUpdateStaffHelper(updated)

    this.closeForm()
  }


  ngOnInit(): void {
    this.staffTypes = this.staffServices.staffTypes;
    delete this.staffTypes.staffs.All
    this.staffServices.$popupEvent.subscribe(response => {
      if (response.staffId < 0) {
        this.formType = "Add Form"
        this.staff.staffType = response.staffType;
        this.staff.staffId = response.staffId
        this.staff.departmentName = this.staffTypes.staffs[response.staffType]?.name
      }
      else {
        this.formType = "Update Form"
        this.changeValue(response)
      }
      this.openForm();
    })
  }

}
