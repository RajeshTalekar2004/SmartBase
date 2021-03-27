import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CgstModel } from '../_models/cgstmodel';
import { map } from 'rxjs/operators';
import { CgstParams } from '../_models/cgstParams';
import { PaginatedResult } from '../_models/pagination ';
import { ServiceResponseModel } from '../_models/serviceresponseModel';


@Injectable({
  providedIn: 'root'
})

export class CgstService {

  baseUrl = environment.apiUrl;
  cgstModels: CgstModel[] = [];

  constructor(private http: HttpClient) { }

  getAllCgst(cgstParams: CgstParams) {
    //debugger;
    let params = this.getPaginationHeaders(cgstParams.pageNumber, cgstParams.pageSize);
    params = params.append('orderBy', cgstParams.orderBy);
    return this.getPaginatedResult<CgstModel[]>(this.baseUrl + 'CgstMaster/GetAllByPage', params);
  }

  editCgst(cgstParams: CgstParams) {
    //debugger;

  }

  deleteCgst(cgstParams: CgstParams) {
    //debugger;
  }

  getById(cgstId: string) {
    //debugger;
    return this.http.get<ServiceResponseModel>(this.baseUrl + 'CgstMaster/' + cgstId);
  }


  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    //debugger;
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return params;
  }

  private getPaginatedResult<T>(url, params) {
    //debugger;
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        //debugger;
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

}
