import { CartService } from './cart/cart.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public title = 'SkynER';

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    const cartId: string = localStorage.getItem('cart_id');

    if (cartId) {
      this.cartService.getCartFromServer(cartId).subscribe(
        () => console.log('Initialised cart'),
        (error: any) => console.log(error)
      );
    }
  }
}
