import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReturnStaff } from 'src/app/pipes/returnStaff.pipe';
import { StaffListFilterPipe } from 'src/app/pipes/stafflistfilter.pipe';
import { StaffListSortPipe } from 'src/app/pipes/staffListSort.pipe';
import { ConfirmationpopupComponent } from './confirmationpopup/confirmationpopup.component';
import { PaginateComponent } from './paginate/paginate.component';
import { NotificationPopupComponent } from './notification-popup/notification-popup.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'



@NgModule({
  declarations: [
    ConfirmationpopupComponent,
    PaginateComponent,
    StaffListFilterPipe,
    StaffListSortPipe,
    ReturnStaff,
    NotificationPopupComponent,
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
  ],
  exports: [
    ConfirmationpopupComponent,
    PaginateComponent,
    StaffListFilterPipe,
    StaffListSortPipe,
    ReturnStaff,
    NotificationPopupComponent,
  ],
})
export class CommonComponents { }
