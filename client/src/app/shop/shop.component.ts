import { SortParameters } from './../shared/models/sort-parameters';
import { SortOption } from './../shared/models/sort-option';
import { IProductType } from './../shared/models/product-type';
import { IProductBrand } from './../shared/models/product-brand';
import { ShopService } from './shop.service';
import { IProduct } from './../shared/models/product';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm: ElementRef;

  public products: IProduct[];
  public productBrands: IProductBrand[];
  public productTypes: IProductType[];
  public sortParameters = new SortParameters();
  public sortOptions: SortOption[];
  public totalCount: number;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.loadSortOptions();
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  public loadSortOptions(): void {
    this.sortOptions = [
      { name: 'Alphabetical', value: 'name' },
      { name: 'Price: Low to High', value: 'priceAsc' },
      { name: 'Price: High to Low', value: 'priceDesc' },
    ];
  }

  public getProducts(): void {
    this.shopService.getProducts(this.sortParameters).subscribe(
      (paginatedProducts) => {
        this.products = paginatedProducts.data;
        this.sortParameters.pageIndex = paginatedProducts.pageIndex;
        this.sortParameters.pageSize = paginatedProducts.pageSize;
        this.totalCount = paginatedProducts.count;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  public getProductBrands(): void {
    this.shopService.getProductBrands().subscribe(
      (productBrands) => {
        this.productBrands = [{ id: 0, name: 'All' }, ...productBrands];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  public getProductTypes(): void {
    this.shopService.getProductTypes().subscribe(
      (productTypes) => {
        this.productTypes = [{ id: 0, name: 'All' }, ...productTypes];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  public getProductsByBrand(brandId: number): void {
    this.sortParameters.selectedBrandId = brandId;
    this.sortParameters.pageIndex = 1;
    this.getProducts();
  }

  public getProductsByType(typeId: number): void {
    this.sortParameters.selectedTypeId = typeId;
    this.sortParameters.pageIndex = 1;
    this.getProducts();
  }

  public getSortedProducts(sortBy: string): void {
    this.sortParameters.selectedSortMethod = sortBy;
    this.getProducts();
  }

  public changePage(pageIndex: number): void {
    if (this.sortParameters.pageIndex !== pageIndex) {
      this.sortParameters.pageIndex = pageIndex;
      this.getProducts();
    }
  }

  public searchProducts(): void {
    this.sortParameters.search = this.searchTerm.nativeElement.value;
    this.sortParameters.pageIndex = 1;
    this.getProducts();
  }

  public resetSearch(): void {
    this.searchTerm.nativeElement.value = '';
    this.sortParameters = new SortParameters();
    this.getProducts();
  }
}
