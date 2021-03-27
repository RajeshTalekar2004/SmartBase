import { Component, OnInit } from '@angular/core';
import { CgstService } from '../../_services/cgst.service'
import { CgstModel } from '../../_models/cgstmodel'
import { Pagination } from '../../_models/pagination '
import { CgstParams } from '../../_models/cgstParams'
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cgst-list',
  templateUrl: './cgst-list.component.html',
  styleUrls: ['./cgst-list.component.css']
})
export class CgstListComponent implements OnInit {

  cgstModels: CgstModel[];
  pagination: Pagination;
  cgstParams: CgstParams;
  sortByList = [{ value: 'cgstId', display: 'Id' }, { value: 'cgstDetail', display: 'Detail' }, { value: 'cgstRate', display: 'Rate' }];
  pageSizes = [{ value: '10', display: '10' }, { value: '20', display: '20' }, { value: '50', display: '50' }];

  constructor(private cgstService: CgstService, private router: Router, private toastr: ToastrService) {
    this.cgstParams = new CgstParams();
  }

  ngOnInit(): void {
    this.loadCgstList();
  }

  loadCgstList() {
    //debugger;
    this.cgstService.getAllCgst(this.cgstParams).subscribe(response => {
      //debugger;
      this.cgstModels = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters() {
    this.cgstParams = new CgstParams();
    this.loadCgstList();
  }

  addNewRecord() {
    console.log('adding new record');
    this.router.navigateByUrl('cgstadd');
  }

  editRecord() {
    console.log('Edit record');
  }

  deleteRecord() {
    console.log('deleting record');
  }

  pageChanged(event: any) {
    //debugger;
    this.cgstParams.pageNumber = event.page;
    this.loadCgstList();
  }

}
