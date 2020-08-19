import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { PaginationComponent } from './components/pagination/pagination.component';

@NgModule({
  declarations: [
    NavBarComponent,
    PaginationHeaderComponent,
    PaginationComponent,
  ],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [
    PaginationModule,
    NavBarComponent,
    PaginationHeaderComponent,
    PaginationComponent,
  ],
})
export class SharedModule {}
