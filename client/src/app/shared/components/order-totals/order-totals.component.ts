import { CartService } from './../../../cart/cart.service';
import { ICartTotals } from './../../models/cart-totals';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
  public cartTotal$: Observable<ICartTotals>;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartTotal$ = this.cartService.cartTotal$;
  }
}
