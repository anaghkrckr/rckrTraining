import { Pipe, PipeTransform } from "@angular/core"

@Pipe({
    name: "staffListSort"
})
export class StaffListSortPipe implements PipeTransform {
    transform(staffList: any, sortKey: any, sortOrder: any) {
        return staffList ? staffList.sort((a: any, b: any) => {
            return sortOrder ? ((b[sortKey] > a[sortKey]) ? 1 : ((a[sortKey] > b[sortKey]) ? -1 : 0)) : ((a[sortKey] > b[sortKey]) ? 1 : ((b[sortKey] > a[sortKey]) ? -1 : 0));
        }) : [];
    }

}