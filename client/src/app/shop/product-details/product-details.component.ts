import { CartService } from './../../cart/cart.service';
import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  public product: IProduct;
  public quantity = 1;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private cartService: CartService
  ) {
    this.breadcrumbService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  public addItemToCart(): void {
    this.cartService.addItemToCart(this.product, this.quantity);
  }

  public incrementQuantity(): void {
    this.quantity++;
  }

  public decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  public loadProduct(): void {
    this.shopService
      .getProduct(+this.activatedRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product: IProduct) => {
          this.product = product;
          this.breadcrumbService.set('@productDetails', product.name);
        },
        (error: any) => console.log(error)
      );
  }
}
