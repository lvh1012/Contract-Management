import { Page } from '../models/page';
import { HttpClient } from '@angular/common/http';
import { DataPage } from '../models';

export class BaseService<TModel> {
  resource!: string;
  url!: string;

  constructor(protected http: HttpClient) { }

  getData(page: Page = {} as any, query: string = '') {
    const queryString = query ? `?query=${query}` : '';
    return this.http
      .post(`${this.url}/GetData${queryString}`, page)
      .toPromise() as Promise<DataPage<TModel>>;
  }

  deleteMultiple(listId: Array<string>) {
    const data = { ids: listId }
    return this.http
      .post(`${this.url}/DeleteMultiple`, data)
      .toPromise() as Promise<boolean>;
  }

  delete(id: string) {
    return this.http
      .delete(`${this.url}/${id}`)
      .toPromise() as Promise<boolean>;
  }

  getById(id: string) {
    return this.http
      .get(`${this.url}/${id}`)
      .toPromise() as Promise<TModel>;
  }

  insert(data: TModel) {
    return this.http
      .post(`${this.url}`, data)
      .toPromise() as Promise<TModel>;
  }

  update(id: string, data: TModel) {
    return this.http
      .put(`${this.url}/${id}`, data)
      .toPromise() as Promise<TModel>;
  }
}
