import { Contract } from './../../contract/models/contract.model';
import { BaseModel } from "src/app/base/models";

export interface Customer extends BaseModel {
  contracts?: Contract[];
}
