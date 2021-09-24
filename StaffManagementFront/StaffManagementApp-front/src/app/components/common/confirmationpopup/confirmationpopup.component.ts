import { trigger } from '@angular/animations';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StaffServices } from 'src/app/services/staff.service';

@Component({
  selector: 'app-confirmationpopup',
  templateUrl: './confirmationpopup.component.html',
  styleUrls: ['./confirmationpopup.component.css'],

})
export class ConfirmationpopupComponent implements OnInit {

  constructor(private staffServices: StaffServices) {

  }
  ngOnInit(): void {
  }


  @Input() popupType: string = "Delete";
  @Input() popupMessage: string = "Are you sure, do you want to delete?";
  @Output() confirmed = new EventEmitter();





  executeFuction(value: boolean) {
    this.confirmed.emit(value)
  }


}
