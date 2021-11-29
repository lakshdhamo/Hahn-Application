import { Asset } from './asset';
export class User{
  id: number;
  age: number;
  firstName: string;
  lastName: string;
  address: string;
  email: string;
  assets: Asset[];
}
