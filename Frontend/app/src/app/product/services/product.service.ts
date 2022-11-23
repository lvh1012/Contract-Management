import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Product } from '../models';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/base/services';

@Injectable()
export class ProductService extends BaseService<Product> {
  constructor(http: HttpClient) {
    super(http);

    this.resource = 'Product';
    this.url = `${environment.baseUrl}/${environment.prefix}/${this.resource}`
  }
}
