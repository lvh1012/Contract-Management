import { CustomerService } from './../customer/services/customer.service';
import { DataPage } from './../base/models/data-page';
import { Customer } from './../customer/models';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BaseComponent } from '../base/components';
import { Contract } from './models';
import { ContractService } from './services';
import { FormState } from '../base/enums';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent extends BaseComponent<ContractService, Contract> implements OnInit {
  customerResults: Customer[] = []
  searchCustomer = new Subject<any>();
  override delay: number = 400;

  constructor(contractService: ContractService, messageService: MessageService, confirmationService: ConfirmationService, private customerService: CustomerService) {
    super(contractService, messageService, confirmationService);
  }

  async ngOnInit() {
    await this.loadData();

    this.searchCustomer.pipe(debounceTime(this.delay), distinctUntilChanged())
      .subscribe(async ($event: any) => {
        let query = $event.filter;
        query = query?.trim();
        if (!query) return;

        const res = await this.customerService.getData(undefined, query);
        this.customerResults = res.data;
      });
  }

  changeCustomer($event: any) {
    const { value } = $event;
    this.customerResults = this.customerResults.filter(c => c.id == value);
  }

  override async edit(rowData: Contract) {
    this.openDialog();
    this.formState = FormState.EDIT;
    const data = await this.service.getById(rowData.id as string);
    this.currentRow = { ...data };

    const customer = await this.customerService.getById(data.customerId as string);
    this.customerResults = [customer];
  }
}
