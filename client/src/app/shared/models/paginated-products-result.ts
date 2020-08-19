import { IProduct } from './product';

export interface IPaginatedProductsResult {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: IProduct[];
}
