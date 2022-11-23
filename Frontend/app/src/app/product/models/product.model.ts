import { ContractProduct } from 'src/app/contract/models';
import { BaseModel } from '../../base/models';
export interface Product extends BaseModel {
  price?: number;
  unit?: string;
  size?: string;
  contractProducts: ContractProduct[];
}
