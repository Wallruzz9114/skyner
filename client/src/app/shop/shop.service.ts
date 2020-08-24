import { environment } from './../../environments/environment';
import { SortParameters } from './../shared/models/sort-parameters';
import { IProductType } from './../shared/models/product-type';
import { IProductBrand } from './../shared/models/product-brand';
import { IPaginatedProductsResult } from '../shared/models/paginated-products-result';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  public baseURL = environment.apiURL;

  constructor(private http: HttpClient) {}

  public getProducts(
    sortParameters: SortParameters
  ): Observable<IPaginatedProductsResult> {
    let parameters = new HttpParams();

    if (sortParameters.selectedBrandId !== 0) {
      parameters = parameters.append(
        'brandId',
        sortParameters.selectedBrandId.toString()
      );
    }

    if (sortParameters.selectedTypeId !== 0) {
      parameters = parameters.append(
        'typeId',
        sortParameters.selectedTypeId.toString()
      );
    }

    if (sortParameters.search) {
      parameters = parameters.append('search', sortParameters.search);
    }

    parameters = parameters.append('sort', sortParameters.selectedSortMethod);
    parameters = parameters.append(
      'pageIndex',
      sortParameters.pageIndex.toString()
    );
    parameters = parameters.append(
      'pageSize',
      sortParameters.pageSize.toString()
    );

    return this.http
      .get<IPaginatedProductsResult>(this.baseURL + 'product/all', {
        observe: 'response',
        params: parameters,
      })
      .pipe(map((httpResponse) => httpResponse.body));
  }

  public getProduct(productId: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.baseURL + 'product/' + productId);
  }

  public getProductBrands(): Observable<IProductBrand[]> {
    return this.http.get<IProductBrand[]>(this.baseURL + 'product/brands');
  }

  public getProductTypes(): Observable<IProductType[]> {
    return this.http.get<IProductType[]>(this.baseURL + 'product/types');
  }
}
