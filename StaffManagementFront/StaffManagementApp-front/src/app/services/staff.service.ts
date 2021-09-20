import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Staff } from '../model/staff';

@Injectable({
  providedIn: 'root'
})


export class StaffServices {
  constructor(private http: HttpClient) { }


  private _popupEvent: Subject<Staff> = new Subject();
  public $popupEvent: Observable<Staff> = this._popupEvent.asObservable();

  private _formSubmitEvent: Subject<any> = new Subject();
  public $formSubmitEvent: Observable<any> = this._formSubmitEvent.asObservable();

  private _staffdeleteClickEvent: Subject<Staff[]> = new Subject();
  public $staffdeleteClickEvent: Observable<Staff[]> = this._staffdeleteClickEvent.asObservable();

  private _successEvent: Subject<Staff[]> = new Subject();
  public $successEvent: Observable<Staff[]> = this._successEvent.asObservable();

  private _serchEvent: Subject<string> = new Subject();
  public $serchEvent: Observable<string> = this._serchEvent.asObservable();

  onKeyUpSerch(keyword: string) {
    this._serchEvent.next(keyword);
  }

  onDeleteClick(staff: Staff[]) {
    this._staffdeleteClickEvent.next(staff)
  }

  onPopupClick(staff: Staff) {
    this._popupEvent.next(staff);
  }

  onSubmitClick(func: Function) {
    this._formSubmitEvent.next(func)
  }

  onSuccessEvent(staff: any[]) {
    this._successEvent.next(staff)
  }

  staffs: Object = [];
  staffTypes = {
    staffs: {
      All: { objName: "department", name: "Department" },
      Teacher: { objName: "subject", name: "Subject" },
      Administrator: { objName: "administratorDepartment", name: "Administrator Department" },
      Support: { objName: "supportDepartment", name: "Support Department" }
    },
    selected: "All"
  }

  staffDeleteList: Object[] = []


  getStaffs(): Observable<any> {
    return this.http.get('https://localhost:44332/api/staff')
  }

  getStaff(staffType: string): Observable<any> {
    return this.http.get('https://localhost:44332/api/staff/' + staffType)
  }



  deleteStaffHelper(staff?: any[]) {
    if (staff != null) {
      this.staffDeleteList = staff;
    }

    if (this.staffDeleteList.length == 0) {
      console.log("pls add staffs")
      return;
    }

    this.deleteStaff(this.staffDeleteList).subscribe(resp => {
      this.staffDeleteList = []
      if (this.staffTypes.selected == "All") {
        this.getStaffs().subscribe(staffs => {
          this.onSuccessEvent(staffs);
        })
      } else {
        this.getStaff(this.staffTypes.selected).subscribe(response => {
          switch (this.staffTypes.selected) {
            case "Teacher":
              this.onSuccessEvent(response);
              break;
            case "Administrator":
              this.onSuccessEvent(response);
              break;
            case "Support":
              this.onSuccessEvent(response);
              break;
          }
        })
      }
    })
  }

  addUpdateStaffHelper(staff: Staff) {
    if (staff.staffId > 0) {
      this.updateStaff(staff).subscribe(Response => {
        if (this.staffTypes.selected == "All") {
          this.getStaffs().subscribe(staffs => {
            this.onSuccessEvent(staffs)
          })
        } else {

          this.getStaff(this.staffTypes.selected).subscribe(resp => {
            this.onSuccessEvent(resp)
          })
        }
      })
    } else {
      this.addStaff(staff).subscribe(Response => {
        if (this.staffTypes.selected == "All") {
          this.getStaffs().subscribe(staffs => {
            this.onSuccessEvent(staffs)
          })
        } else {

          this.getStaff(this.staffTypes.selected).subscribe(resp => {
            this.onSuccessEvent(resp)
          })
        }
      })

    }

  }


  deleteStaff(staffs: any[]) {
    const headers = { 'Content-Type': 'application/json' }
    const body = staffs;
    const url = "https://localhost:44332/api/staff";
    return this.http.delete(url,
      ({
        body: body,
        headers: headers
      }))
  }

  updateStaff(staff: any) {
    const headers = { 'Content-Type': 'application/json' }
    const body = { ...staff }
    const url = "https://localhost:44332/api/staff/" + staff.staffType + "/" + staff.staffId;
    return this.http.put(url,
      body,
      { headers }
    )
  }

  addStaff(staff: any) {
    const headers = { 'Content-Type': 'application/json' }
    const body = { ...staff }
    const url = "https://localhost:44332/api/staff";
    return this.http.post(url,
      body,
      { headers }
    )
  }


}
