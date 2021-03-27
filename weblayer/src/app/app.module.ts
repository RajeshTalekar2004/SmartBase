import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';

import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { SgstComponent } from './sgst/sgst.component';
import { IgstComponent } from './igst/igst.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { CgstListComponent } from './cgst/cgst-list/cgst-list.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { CgstEditComponent } from './cgst/cgst-edit/cgst-edit.component';
import { CgstAddComponent } from './cgst/cgst-add/cgst-add.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    SgstComponent,
    IgstComponent,
    TestErrorsComponent,
    CgstListComponent,
    CgstEditComponent,
    CgstAddComponent,
    TextInputComponent,
    DateInputComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }