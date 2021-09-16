import { Support } from './../../model/support';
import { Administrator } from './../../model/administrator';
import { Teacher } from './../../model/teacher';
import { StaffServices } from '../../services/staff.service';
import { Component, Input, IterableDiffers, OnInit, Output } from '@angular/core';
import { Staff } from 'src/app/model/staff';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent extends Staff implements OnInit {

  constructor(private staffServices: StaffServices) {
    super()
  }

  ngOnInit(): void {



  }

}
