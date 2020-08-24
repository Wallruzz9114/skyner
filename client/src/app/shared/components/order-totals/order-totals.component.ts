import { CustomerBasketService } from './../../../customer-basket/customer-basket.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { IBasketTotals } from '../../models/basket-totals';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
  public customerBasketTotal$: Observable<IBasketTotals>;

  constructor(private customerBasketService: CustomerBasketService) {}

  ngOnInit(): void {
    this.customerBasketTotal$ = this.customerBasketService.customerBasketTotal$;
  }
}
