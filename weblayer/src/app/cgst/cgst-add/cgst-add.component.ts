import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CgstModel } from 'src/app/_models/cgstmodel';
import { CgstService } from 'src/app/_services/cgst.service';
import { TextInputComponent } from '../../_forms/text-input/text-input.component'


@Component({
  selector: 'app-cgst-add',
  templateUrl: './cgst-add.component.html',
  styleUrls: ['./cgst-add.component.css']
})
export class CgstAddComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  cgstModel: CgstModel;
  addForm: FormGroup;

  constructor(private cgstService: CgstService, private toastr: ToastrService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.initilizeForm();
  }

  initilizeForm() {
    this.addForm = new FormGroup({
      cgstId: new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$")]),
      cgstDetail: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      cgstRate: new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$")])
    })
  }

  numberOnly(inValue: string): ValidatorFn {
    return (control: AbstractControl) => {
      debugger;
      return control?.value === control?.parent?.controls[inValue].value
        ? null : { isNumber: true }
    }
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : { isMatching: true }
    }
  }



  register() {
    console.log(this.addForm.value);
  }

}
