import { ReturnStaff } from './pipes/returnStaff.pipe';
import { StaffListSortPipe } from './pipes/staffListSort.pipe';
import { StaffListFilterPipe } from './pipes/stafflistfilter.pipe';
import { StaffServices } from './services/staff.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { StaffComponent } from './components/staff/staff.component';
import { TeacherComponent } from './components/staff/teacher/teacher.component';
import { PopupformComponent } from './components/common/popupform/popupform.component';
import { AdministratorComponent } from './components/staff/administrator/administrator.component';
import { SupportComponent } from './components/staff/support/support.component';
import { NavbarComponent } from './components/common/navbar/navbar.component';
import { ConfirmationpopupComponent } from './components/common/confirmationpopup/confirmationpopup.component';
import { PaginateComponent } from './components/common/paginate/paginate.component'

@NgModule({
  declarations: [
    AppComponent,
    StaffComponent,
    TeacherComponent,
    PopupformComponent,
    AdministratorComponent,
    SupportComponent,
    NavbarComponent,
    ConfirmationpopupComponent,
    PaginateComponent,
    StaffListFilterPipe,
    StaffListSortPipe,
    ReturnStaff,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: "full" },
      { path: 'home', component: StaffComponent },
      { path: 'teacher', component: TeacherComponent },
      { path: 'administrator', component: AdministratorComponent },
      { path: 'support', component: SupportComponent },
    ])
  ],
  providers: [
    StaffServices
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
