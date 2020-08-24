import { ICustomerBasket } from './../../models/customer-basket';
import { Observable } from 'rxjs';
import { CustomerBasketService } from './../../../customer-basket/customer-basket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  public customerBasket$: Observable<ICustomerBasket>;

  constructor(private customerBasketService: CustomerBasketService) {}

  ngOnInit(): void {
    this.customerBasket$ = this.customerBasketService.customerBasket$;
  }
}
