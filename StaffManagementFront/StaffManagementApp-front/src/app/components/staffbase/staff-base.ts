import { Directive } from "@angular/core";
import { Administrator } from "src/app/model/administrator";
import { Staff } from "src/app/model/staff";
import { Support } from "src/app/model/support";
import { Teacher } from "src/app/model/teacher";
import { StaffServices } from "src/app/services/staff.service";

@Directive()
export abstract class StaffBase {
    constructor(private staffServices: StaffServices) {
        console.log(this.staffServices.staffTypes)
        this.staffTypes = this.staffServices.staffTypes;
        this.eventListener();
    }

    staffs: any;
    Teacher!: Teacher[];
    Administrator!: Administrator[];
    Support!: Support[];

    staffTypes!: any;

    staffDeleteList: Object[] = []

    abstract onSearch(keyword: string): void;

    eventListener() {
        this.staffServices.$serchEvent.subscribe(resp => {
            this.onSearch(resp)
        })
    }


    addStaff() {
        console.log("add")
        let staff: Staff = {
            staffId: -1,
            staffName: "",
            staffAge: 0,
            staffType: this.staffServices.staffTypes.selected
        };
        this.staffServices.onPopupClick(staff)

    }


    onDeleteClick(staff: any[]) {
        this.staffServices.onDeleteClick(staff)
    }

    getStaff(staffType: string) {
        return this.staffServices.getStaff(staffType)
    }

    addToDeleteList(staff: any) {
        this.staffDeleteList.push(staff)
        console.log(this.staffDeleteList);
    }

    deleteStaff(staff?: any[]) {
        console.log("executed")
        this.staffServices.deleteStaffHelper(staff)
    }

}
