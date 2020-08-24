import { RouterModule } from '@angular/router';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';

@NgModule({
  declarations: [
    NavBarComponent,
    PaginationHeaderComponent,
    PaginationComponent,
    OrderTotalsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
  ],
  exports: [
    PaginationModule,
    NavBarComponent,
    PaginationHeaderComponent,
    PaginationComponent,
    CarouselModule,
    OrderTotalsComponent,
  ],
})
export class SharedModule {}
