import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "staffListFilter",
})
export class StaffListFilterPipe implements PipeTransform {
    transform(staffList: any, keyword: string) {
        return staffList ? staffList.filter((item: any) => item.staffName.search(new RegExp(keyword, 'i')) > -1) : [];
    }

}