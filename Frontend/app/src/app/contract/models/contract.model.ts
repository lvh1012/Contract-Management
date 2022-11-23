import { Customer } from './../../customer/models';
import { BaseModel } from "src/app/base/models";
import { ContractProduct } from '.';

export interface Contract extends BaseModel {
  total: number;
  customerId: string;
  customer: Customer;
  contractProducts: ContractProduct[];
}
