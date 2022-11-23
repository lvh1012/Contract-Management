import { Page } from "./page";

export interface DataPage<T> {
  data: T[];
  page: Page;
}
