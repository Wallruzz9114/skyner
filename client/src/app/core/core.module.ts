import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { SectionHeaderComponent } from './section-header/section-header.component';

@NgModule({
  imports: [
    CommonModule,
    BreadcrumbModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  declarations: [
    ServerErrorComponent,
    NotFoundComponent,
    SectionHeaderComponent,
  ],
  exports: [SectionHeaderComponent],
})
export class CoreModule {}
