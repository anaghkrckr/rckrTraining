import { StaffServices } from './services/staff.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { StaffComponent } from './components/staff/staff.component';
import { StaffTableComponent } from './components/staff-table/staff-table.component';
import { TeacherComponent } from './components/staff/teacher/teacher.component';
import { PopupformComponent } from './components/popupform/popupform.component';
import { AdministratorComponent } from './components/staff/administrator/administrator.component';
import { SupportComponent } from './components/staff/support/support.component';
import { NavbarComponent } from './components/navbar/navbar.component'

@NgModule({
  declarations: [
    AppComponent,
    StaffComponent,
    StaffTableComponent,
    TeacherComponent,
    PopupformComponent,
    AdministratorComponent,
    SupportComponent,
    NavbarComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: StaffComponent },
      { path: 'teacher', component: TeacherComponent },
      { path: 'administrator', component: AdministratorComponent },
      { path: 'support', component: SupportComponent }
    ])
  ],
  providers: [
    StaffServices
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
