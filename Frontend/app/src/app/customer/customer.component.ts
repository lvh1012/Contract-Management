import { CustomerService } from './services';
import { Component, OnInit } from '@angular/core';
import { Customer } from './models';
import { BaseComponent } from '../base/components';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent extends BaseComponent<CustomerService, Customer> implements OnInit {

  constructor(customerService: CustomerService, messageService: MessageService, confirmationService: ConfirmationService) {
    super(customerService, messageService, confirmationService);
  }

  async ngOnInit() {
    await this.loadData();
  }
}
