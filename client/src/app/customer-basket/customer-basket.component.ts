import { IBasketItem } from './../shared/models/basket-tem';
import { CustomerBasketService } from './customer-basket.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ICustomerBasket } from '../shared/models/customer-basket';

@Component({
  selector: 'app-customer-basket',
  templateUrl: './customer-basket.component.html',
  styleUrls: ['./customer-basket.component.scss'],
})
export class CustomerBasketComponent implements OnInit {
  public customerBasket$: Observable<ICustomerBasket>;

  constructor(private customerBasketService: CustomerBasketService) {}

  ngOnInit(): void {
    this.customerBasket$ = this.customerBasketService.customerBasket$;
  }

  public removeItem(basketItem: IBasketItem): void {
    this.customerBasketService.removeItemFromCustomerBasket(basketItem);
  }

  public incrementItemQuantity(basketItem: IBasketItem): void {
    this.customerBasketService.incrementBasketItemQuantity(basketItem);
  }

  public decrementItemQuantity(basketItem: IBasketItem): void {
    this.customerBasketService.decrementBasketItemQuantity(basketItem);
  }
}
