import { Customer } from './../models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/base/services';

@Injectable()
export class CustomerService extends BaseService<Customer> {
  constructor(http: HttpClient) {
    super(http);

    this.resource = 'Customer';
    this.url = `${environment.baseUrl}/${environment.prefix}/${this.resource}`
  }
}
