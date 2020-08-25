import { v4 as uuidv4 } from 'uuid';
import { ICartItem } from './cart-item';

export interface ICart {
  id: string;
  items: ICartItem[];
}

export class Cart implements ICart {
  id: string = uuidv4();
  items: ICartItem[] = [];
}
