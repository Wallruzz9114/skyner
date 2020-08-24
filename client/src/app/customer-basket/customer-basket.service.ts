import { IBasketTotals } from './../shared/models/basket-totals';
import { IProduct } from './../shared/models/product';
import { IBasketItem } from './../shared/models/basket-tem';
import { map } from 'rxjs/operators';
import {
  ICustomerBasket,
  CustomerBasket,
} from './../shared/models/customer-basket';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerBasketService {
  public baseURL = environment.apiURL;
  private customerBasketSource: BehaviorSubject<
    ICustomerBasket
  > = new BehaviorSubject<ICustomerBasket>(null);
  public customerBasket$: Observable<
    ICustomerBasket
  > = this.customerBasketSource.asObservable();
  private customerBasketTotalSource: BehaviorSubject<
    IBasketTotals
  > = new BehaviorSubject<IBasketTotals>(null);
  public customerBasketTotal$ = this.customerBasketTotalSource.asObservable();

  constructor(private httpClient: HttpClient) {}

  public getCustomerBasketFromServer(id: string): Observable<void> {
    return this.httpClient
      .get<ICustomerBasket>(this.baseURL + `customerbasket?id=${id}`)
      .pipe(
        map((basket: ICustomerBasket) => {
          this.customerBasketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  public setCustomerBasket(customerBasket: ICustomerBasket): Subscription {
    return this.httpClient
      .post<ICustomerBasket>(this.baseURL + 'customerbasket', customerBasket)
      .subscribe(
        (basket: ICustomerBasket) => {
          this.customerBasketSource.next(basket);
          this.calculateTotals();
        },
        (error: any) => console.log(error)
      );
  }

  public getCurrentCustomerBasket(): ICustomerBasket {
    return this.customerBasketSource.value;
  }

  public addItemToCustomerBasket(product: IProduct, quantity = 1): void {
    const basketItem: IBasketItem = this.mapProductToBasketItem(
      product,
      quantity
    );
    const customerBasket =
      this.getCurrentCustomerBasket() ?? this.createCustomerBasket();

    customerBasket.items = this.addOrUpdateBasketItem(
      customerBasket.items,
      basketItem,
      quantity
    );

    this.setCustomerBasket(customerBasket);
  }

  public incrementBasketItemQuantity(basketItem: IBasketItem): void {
    const currentCustomerBasket: ICustomerBasket = this.getCurrentCustomerBasket();
    const foundBasketItemIndex = currentCustomerBasket.items.findIndex(
      (item) => item.id === basketItem.id
    );
    currentCustomerBasket.items[foundBasketItemIndex].quantity++;

    this.setCustomerBasket(currentCustomerBasket);
  }

  public decrementBasketItemQuantity(basketItem: IBasketItem): void {
    const currentCustomerBasket: ICustomerBasket = this.getCurrentCustomerBasket();
    const foundBasketItemIndex = currentCustomerBasket.items.findIndex(
      (item) => item.id === basketItem.id
    );

    if (currentCustomerBasket.items[foundBasketItemIndex].quantity > 1) {
      currentCustomerBasket.items[foundBasketItemIndex].quantity--;
      this.setCustomerBasket(currentCustomerBasket);
    } else {
      this.removeItemFromCustomerBasket(
        currentCustomerBasket.items[foundBasketItemIndex]
      );
    }
  }

  public removeItemFromCustomerBasket(basketItem: IBasketItem): void {
    const currentCustomerBasket: ICustomerBasket = this.getCurrentCustomerBasket();

    if (currentCustomerBasket.items.some((item) => item.id === basketItem.id)) {
      currentCustomerBasket.items = currentCustomerBasket.items.filter(
        (item) => item.id !== basketItem.id
      );

      if (currentCustomerBasket.items.length > 0) {
        this.setCustomerBasket(currentCustomerBasket);
      } else {
        this.deleteCustomerBasket(currentCustomerBasket);
      }
    }
  }

  public deleteCustomerBasket(customerBasket: ICustomerBasket): Subscription {
    return this.httpClient
      .delete<void>(this.baseURL + `customerbasket?id=${customerBasket.id}`)
      .subscribe(
        () => {
          this.customerBasketSource.next(null);
          this.customerBasketTotalSource.next(null);
          localStorage.removeItem('basket_id');
        },
        (error: any) => console.log(error)
      );
  }

  private addOrUpdateBasketItem(
    basketItems: IBasketItem[],
    basketItemToAdd: IBasketItem,
    quantity: number
  ): IBasketItem[] {
    const itemIndex = basketItems.findIndex(
      (basketItem: IBasketItem) => basketItem.id === basketItemToAdd.id
    );

    if (itemIndex === -1) {
      basketItemToAdd.quantity = quantity;
      basketItems.push(basketItemToAdd);
    } else {
      basketItems[itemIndex].quantity += quantity;
    }

    return basketItems;
  }

  private createCustomerBasket(): ICustomerBasket {
    const customerBasket = new CustomerBasket();
    localStorage.setItem('basket_id', customerBasket.id);
    return customerBasket;
  }

  private calculateTotals(): void {
    const customerbasket = this.getCurrentCustomerBasket();
    const shipping = 0;
    const subTotal = customerbasket.items.reduce(
      (basketItemsTotal, nextBasketItem) =>
        nextBasketItem.price * nextBasketItem.quantity + basketItemsTotal,
      0
    );
    const total = subTotal + shipping;
    this.customerBasketTotalSource.next({ shipping, total, subTotal });
  }

  private mapProductToBasketItem(
    product: IProduct,
    quantity: number
  ): IBasketItem {
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
