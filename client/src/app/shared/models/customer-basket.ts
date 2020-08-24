import { v4 as uuidv4 } from 'uuid';
import { IBasketItem } from './basket-tem';

export interface ICustomerBasket {
  id: string;
  items: IBasketItem[];
}

export class CustomerBasket implements ICustomerBasket {
  id: string = uuidv4();
  items: IBasketItem[] = [];
}
