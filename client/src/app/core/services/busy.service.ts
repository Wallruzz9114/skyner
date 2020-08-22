import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  public busyRequestsCount = 0;

  constructor(private spinnerService: NgxSpinnerService) {}

  public busy(): void {
    this.busyRequestsCount++;
    this.spinnerService.show(undefined, {
      type: 'timer',
      bdColor: 'rgba(255, 255, 255, 0.7)',
      color: '#333333',
    });
  }

  public idle(): void {
    this.busyRequestsCount--;

    if (this.busyRequestsCount <= 0) {
      this.busyRequestsCount = 0;
      this.spinnerService.hide();
    }
  }
}
