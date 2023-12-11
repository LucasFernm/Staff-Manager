import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { DepartamentosListaComponent } from './components/departamento-lista/departamento-lista.component';
import { DepartamentoFormComponent } from './components/departamento-form/departamento-form.component';
import { FuncionarioListComponent } from './components/funcionario-lista/funcionario-lista.component';
import { FuncionarioFormComponent } from './components/funcionario-form/funcionario-form.component';



@NgModule({
  declarations: [
    AppComponent,
    DepartamentosListaComponent,
    DepartamentoFormComponent,
    FuncionarioListComponent,
    FuncionarioFormComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
