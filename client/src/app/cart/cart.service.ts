import { ICartItem } from './../shared/models/cart-item';
import { ICart, Cart } from './../shared/models/cart';
import { ICartTotals } from './../shared/models/cart-totals';
import { IProduct } from './../shared/models/product';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  public baseURL = environment.apiURL;
  private cartSource: BehaviorSubject<ICart> = new BehaviorSubject<ICart>(null);
  public cart$: Observable<ICart> = this.cartSource.asObservable();
  private cartTotalSource: BehaviorSubject<ICartTotals> = new BehaviorSubject<
    ICartTotals
  >(null);
  public cartTotal$ = this.cartTotalSource.asObservable();

  constructor(private httpClient: HttpClient) {}

  public getCartFromServer(id: string): Observable<void> {
    return this.httpClient.get<ICart>(this.baseURL + `cart?id=${id}`).pipe(
      map((cart: ICart) => {
        this.cartSource.next(cart);
        this.calculateTotals();
      })
    );
  }

  public setCart(cart: ICart): Subscription {
    return this.httpClient.post<ICart>(this.baseURL + 'cart', cart).subscribe(
      (latestCart: ICart) => {
        this.cartSource.next(latestCart);
        this.calculateTotals();
      },
      (error: any) => console.log(error)
    );
  }

  public getCurrentCart(): ICart {
    return this.cartSource.value;
  }

  public addItemToCart(product: IProduct, quantity = 1): void {
    const cartItem: ICartItem = this.mapProductToCartItem(product, quantity);
    const cart = this.getCurrentCart() ?? this.createCart();

    cart.items = this.addOrUpdatecartItem(cart.items, cartItem, quantity);

    this.setCart(cart);
  }

  public incrementCartItemQuantity(cartItem: ICartItem): void {
    const currentCart: ICart = this.getCurrentCart();
    const foundCartItemIndex = currentCart.items.findIndex(
      (item) => item.id === cartItem.id
    );
    currentCart.items[foundCartItemIndex].quantity++;

    this.setCart(currentCart);
  }

  public decrementCartItemQuantity(cartItem: ICartItem): void {
    const currentCart: ICart = this.getCurrentCart();
    const foundCartItemIndex = currentCart.items.findIndex(
      (item) => item.id === cartItem.id
    );

    if (currentCart.items[foundCartItemIndex].quantity > 1) {
      currentCart.items[foundCartItemIndex].quantity--;
      this.setCart(currentCart);
    } else {
      this.removeItemFromCart(currentCart.items[foundCartItemIndex]);
    }
  }

  public removeItemFromCart(cartItem: ICartItem): void {
    const currentCart: ICart = this.getCurrentCart();

    if (currentCart.items.some((item) => item.id === cartItem.id)) {
      currentCart.items = currentCart.items.filter(
        (item) => item.id !== cartItem.id
      );

      if (currentCart.items.length > 0) {
        this.setCart(currentCart);
      } else {
        this.emptyCart(currentCart);
      }
    }
  }

  public emptyCart(cart: ICart): Subscription {
    return this.httpClient
      .delete<void>(this.baseURL + `cart?id=${cart.id}`)
      .subscribe(
        () => {
          this.cartSource.next(null);
          this.cartTotalSource.next(null);
          localStorage.removeItem('cart_id');
        },
        (error: any) => console.log(error)
      );
  }

  private addOrUpdatecartItem(
    cartItems: ICartItem[],
    cartItemToAdd: ICartItem,
    quantity: number
  ): ICartItem[] {
    const itemIndex = cartItems.findIndex(
      (cartItem: ICartItem) => cartItem.id === cartItemToAdd.id
    );

    if (itemIndex === -1) {
      cartItemToAdd.quantity = quantity;
      cartItems.push(cartItemToAdd);
    } else {
      cartItems[itemIndex].quantity += quantity;
    }

    return cartItems;
  }

  private createCart(): ICart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }

  private calculateTotals(): void {
    const cart = this.getCurrentCart();
    const shipping = 0;
    const subTotal = cart.items.reduce(
      (cartItemsTotal, nextcartItem) =>
        nextcartItem.price * nextcartItem.quantity + cartItemsTotal,
      0
    );
    const total = subTotal + shipping;
    this.cartTotalSource.next({ shipping, total, subTotal });
  }

  private mapProductToCartItem(product: IProduct, quantity: number): ICartItem {
    return {
      id: product.id,
      productName: product.name,
      price: product.price,
      pictureURL: product.pictureURL,
      quantity,
      brand: product.productBrand,
      type: product.productType,
    };
  }
}
