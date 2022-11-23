import { BaseComponent } from '../base/components';
import { ProductEnum } from './enums/product.enum';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { Product } from './models/product.model';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent extends BaseComponent<ProductService, Product> implements OnInit {
  units: any[] = [
    {
      name: 'Cái',
      code: ProductEnum.CAI
    },
    {
      name: 'Chiếc',
      code: ProductEnum.CHIEC
    },
    {
      name: 'Bộ',
      code: ProductEnum.BO
    }
  ];

  constructor(productService: ProductService, messageService: MessageService, confirmationService: ConfirmationService) {
    super(productService, messageService, confirmationService);
  }

  async ngOnInit() {
    await this.loadData();
  }
}
