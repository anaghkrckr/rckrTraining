import { NotificationPopupComponent } from './components/common/notification-popup/notification-popup.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CommonComponents } from './components/common/commonComponents.module';
import { NavbarComponent } from './components/common/navbar/navbar.component';
import { PopupformComponent } from './components/common/popupform/popupform.component';
import { StaffComponent } from './components/staff/staff.component';
import { StaffServices } from './services/staff.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    StaffComponent,
    PopupformComponent,
    NavbarComponent,

  ],
  imports: [
    CommonComponents,
    CommonModule,
    BrowserAnimationsModule,
    // BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: "full" },
      { path: 'home', component: StaffComponent },
      { path: 'teacher', loadChildren: () => import('./components/staff/teacher/teacher.module').then(m => m.TeacherModule) },
      { path: 'administrator', loadChildren: () => import('./components/staff/administrator/administrator.module').then(m => m.AdministratorModule) },
      { path: 'support', loadChildren: () => import('./components/staff/support/support.module').then(m => m.SupportModule) },
    ])
  ],
  exports: [
  ],
  providers: [
    StaffServices
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
