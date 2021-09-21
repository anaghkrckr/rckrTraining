import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonComponents } from './../../common/commonComponents.module';
import { AdministratorComponent } from './administrator.component';


const routes: Routes = [
  { path: '', component: AdministratorComponent }
];

@NgModule({
  declarations: [
    AdministratorComponent,

  ],
  imports: [
    CommonComponents,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class AdministratorModule { }
