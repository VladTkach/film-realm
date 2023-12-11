import {Pagination} from "./pagination";

export interface QueryParams{
  sorts: string;
  page: number;
  pageSize: number;
  filters?: string;

}
