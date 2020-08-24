import { SharedModule } from './../shared/shared.module';
import { CustomerBasketRoutingModule } from './customer-basket-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerBasketComponent } from './customer-basket.component';

@NgModule({
  declarations: [CustomerBasketComponent],
  imports: [CommonModule, CustomerBasketRoutingModule, SharedModule],
})
export class CustomerBasketModule {}
