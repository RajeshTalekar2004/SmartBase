import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CgstListComponent } from './cgst/cgst-list/cgst-list.component'
import { CgstEditComponent } from './cgst/cgst-edit/cgst-edit.component'
import { CgstAddComponent } from './cgst/cgst-add/cgst-add.component'
import { HomeComponent } from './home/home.component';
import { IgstComponent } from './igst/igst.component';
import { SgstComponent } from './sgst/sgst.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'cgstlist', component: CgstListComponent, canActivate: [AuthGuard] },
      { path: 'cgst/:cgstid', component: CgstEditComponent, canDeactivate: [AuthGuard] },
      { path: 'cgstadd', component: CgstAddComponent, canActivate: [AuthGuard] },
      { path: 'sgst', component: SgstComponent },
      { path: 'igst', component: IgstComponent }
    ]
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
