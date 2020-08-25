import { ICartItem } from './../shared/models/cart-item';
import { CartService } from './cart.service';
import { ICart } from './../shared/models/cart';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  public cart$: Observable<ICart>;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  public removeItem(cartItem: ICartItem): void {
    this.cartService.removeItemFromCart(cartItem);
  }

  public incrementItemQuantity(cartItem: ICartItem): void {
    this.cartService.incrementCartItemQuantity(cartItem);
  }

  public decrementItemQuantity(cartItem: ICartItem): void {
    this.cartService.decrementCartItemQuantity(cartItem);
  }
}
