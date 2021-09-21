import { StaffServices } from 'src/app/services/staff.service';
import { Component, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-notification-popup',
  templateUrl: './notification-popup.component.html',
  styleUrls: ['./notification-popup.component.css'],
  animations: [
    trigger('fade', [
      transition('*=>void', [
        animate(1000, style({
          opacity: 0
        })),
      ]),
    ])
  ]
})
export class NotificationPopupComponent implements OnInit {
  message: string = "";

  constructor(private staffServices: StaffServices) { }

  ngOnInit(): void {
    this.staffServices.$notificationevent.subscribe(message => {
      this.message = message
      setTimeout(() => {
        this.message = ""
      }, 1700);
    })
  }

}
