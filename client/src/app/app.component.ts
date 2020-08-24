import { CustomerBasketService } from './customer-basket/customer-basket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public title = 'SkynER';

  constructor(private customerBasketService: CustomerBasketService) {}

  ngOnInit(): void {
    const basketId: string = localStorage.getItem('basket_id');

    if (basketId) {
      this.customerBasketService
        .getCustomerBasketFromServer(basketId)
        .subscribe(
          () => console.log('Initialised basket'),
          (error: any) => console.log(error)
        );
    }
  }
}
