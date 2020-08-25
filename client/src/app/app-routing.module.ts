import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: 'server-error',
    component: ServerErrorComponent,
    data: { breadcrumb: 'Server Error' },
  },
  {
    path: 'not-found',
    component: NotFoundComponent,
    data: { breadcrumb: 'Not Found' },
  },
  {
    path: 'shop',
    loadChildren: () =>
      import('./shop/shop.module').then((module) => module.ShopModule),
    data: { breadcrumb: 'Shop' },
  },
  {
    path: 'cart',
    loadChildren: () =>
      import('./cart/cart.module').then((module) => module.CartModule),
    data: { breadcrumb: 'Cart' },
  },
  {
    path: 'checkout',
    loadChildren: () =>
      import('./checkout/checkout.module').then(
        (module) => module.CheckoutModule
      ),
    data: { breadcrumb: 'Cart' },
  },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
