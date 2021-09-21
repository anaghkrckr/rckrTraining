import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonComponents } from '../../common/commonComponents.module';
import { TeacherComponent } from './teacher.component';


const routes: Routes = [
  { path: '', component: TeacherComponent }
];

@NgModule({
  declarations: [
    TeacherComponent,

  ],
  imports: [
    CommonComponents,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class TeacherModule { }
