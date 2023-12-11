import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';
import { DepartamentosListaComponent } from './components/departamento-lista/departamento-lista.component';
import { FuncionarioListComponent } from './components/funcionario-lista/funcionario-lista.component';


export const routes: Routes = [
  { path: 'departamentos', component: DepartamentosListaComponent },
  { path: 'departamentos/:id/funcionarios', component: FuncionarioListComponent },
  { path: '', redirectTo: '/departamentos', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
