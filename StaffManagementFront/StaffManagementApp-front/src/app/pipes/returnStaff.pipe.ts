import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "returnStaffPipe",
})
export class ReturnStaff implements PipeTransform {
    transform(value: any, ...args: any[]) {
        return value;
    }


}