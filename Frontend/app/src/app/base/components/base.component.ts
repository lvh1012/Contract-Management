import { BaseModel } from '../models';
import { BaseService } from 'src/app/base/services';
import { FormState } from '../enums/base-enum';
import { ConfirmationService, MessageService } from "primeng/api";
import { DataPage } from "../models";
import { Page } from "../models";
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

export class BaseComponent<TService extends BaseService<TModel>, TModel extends BaseModel>{
  dialogVisible: boolean = false;
  loading: boolean = false;
  formState: FormState = FormState.ADD;
  data: TModel[] = [];
  page: Page = {
    totalPage: 0,
    totalRow: 0,
    pageIndex: 1,
    pageSize: 15
  };
  currentRow: TModel = {} as any;
  selectedRows: TModel[] = [];
  submitted: boolean = false;
  pageOption: number[] = [15, 30, 50];

  search = new Subject<string>();
  delay: number = 400;

  constructor(protected service: TService, protected messageService: MessageService, protected confirmationService: ConfirmationService) {
    this.search.pipe(
      debounceTime(this.delay),
      distinctUntilChanged())
      .subscribe(async (value) => {
        this.loadData(value);
      });
  }

  setLoading(value: boolean) {
    this.loading = value;
  }

  async loadData(query: string = '') {
    this.setLoading(true);
    const dataPage = await this.service.getData(this.page, query);
    const { data, page } = dataPage as DataPage<TModel>;
    this.data = data;
    this.page = page;
    this.setLoading(false);
  }

  delete(rowData: TModel) {
    this.confirmationService.confirm({
      message: 'Bạn có chắn chắn muốn xóa bản ghi này không?',
      header: 'Xác nhận',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Có',
      rejectLabel: 'Không',
      accept: async () => {
        await this.deleteOne(rowData.id as string);
        await this.loadData();
        this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đã xóa', life: 3000 });
      }
    });
  }

  deleteSelected() {
    this.confirmationService.confirm({
      message: 'Bạn có chắn chắn muốn xóa những bản ghi này không?',
      header: 'Xác nhận',
      acceptLabel: 'Có',
      rejectLabel: 'Không',
      icon: 'pi pi-exclamation-triangle',
      accept: async () => {
        const listId = this.selectedRows.map(i => i.id);
        await this.deleteMultiple(listId as Array<string>);
        this.selectedRows = [];
        await this.loadData();
        this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đã xóa', life: 3000 });
      }
    });
  }
  async deleteMultiple(listId: Array<string>) {
    await this.service.deleteMultiple(listId);
  }

  async deleteOne(id: string) {
    await this.service.delete(id);
  }

  async edit(rowData: TModel) {
    this.openDialog();
    this.formState = FormState.EDIT;
    const data = await this.service.getById(rowData.id as string) as TModel;
    this.currentRow = { ...data };
  }

  openDialog() {
    this.submitted = false;
    this.dialogVisible = true;
  }

  add() {
    this.openDialog();
    this.formState = FormState.ADD;
  }

  hideDialog() {
    this.dialogVisible = false;
    this.submitted = false;
    this.currentRow = {} as any;
  }

  async beforeSave() {
    return;
  }

  async save() {
    this.submitted = true;
    await this.beforeSave();
    if (this.formState === FormState.ADD) {
      await this.insert();
      this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đã thêm', life: 3000 });
    }
    else if (this.formState === FormState.EDIT) {
      await this.update();
      this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đã cập nhật', life: 3000 });
    }
    this.hideDialog();
    await this.loadData();
  }
  async update() {
    await this.service.update(this.currentRow.id as string, this.currentRow);
  }
  async insert() {
    await this.service.insert(this.currentRow);
  }

  setPage($event: any) {
    this.page.pageIndex = $event.page + 1;
    this.page.pageSize = $event.rows;
  }

  paginate($event: any) {
    this.setPage($event);
    this.loadData();
  }
}
