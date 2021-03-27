import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CgstModel } from 'src/app/_models/cgstmodel';
import { CgstService } from 'src/app/_services/cgst.service';

@Component({
  selector: 'app-cgst-edit',
  templateUrl: './cgst-edit.component.html',
  styleUrls: ['./cgst-edit.component.css']
})
export class CgstEditComponent implements OnInit {

  cgstModel: CgstModel;
  editForm: FormGroup;
  constructor(private cgstService: CgstService, private toastr: ToastrService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadCgstDetail();
  }

  initilizeForm(){
    this.editForm = new FormGroup({
      cgstId: new FormControl(),
      cgstDetail: new FormControl(),
      cgstRate: new FormControl()
    })
  }

  loadCgstDetail() {
    this.cgstService.getById(this.route.snapshot.paramMap.get('cgstid')).subscribe(cgstObject => {
      //debugger;
      this.cgstModel = cgstObject.data;
      //debugger;
    }, error => {
      console.log(error);
      this.toastr.error(error);      
    })
  }

}
