import { CartRoutingModule } from './cart-routing.module';
import { CartComponent } from './cart.component';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [CartComponent],
  imports: [CommonModule, CartRoutingModule, SharedModule],
})
export class CartModule {}
