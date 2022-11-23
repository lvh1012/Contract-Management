import { Product } from './../../product/models';
import { Contract } from './contract.model';
import { BaseModel } from "src/app/base/models";

export interface ContractProduct extends BaseModel {
  quantity: number;
  contractId: string;
  contract: Contract;
  productId: string;
  product: Product;
}
