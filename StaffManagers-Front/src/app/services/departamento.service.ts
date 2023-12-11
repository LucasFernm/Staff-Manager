import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DepartamentoModel } from '../models/departamento.model';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {
  private apiUrl = 'https://localhost:7034/api/Departamento';

  constructor(private http: HttpClient) { }

  // Obter todos os departamentos
  getDepartamentos(): Observable<DepartamentoModel[]> {
    return this.http.get<DepartamentoModel[]>(this.apiUrl);
  }

  // Obter um departamento pelo ID
  getDepartamentoById(id: number): Observable<DepartamentoModel> {
    return this.http.get<DepartamentoModel>(`${this.apiUrl}/${id}`);
  }


  // Criar um novo departamento
  criarDepartamento(departamento: DepartamentoModel): Observable<DepartamentoModel> {
    return this.http.post<DepartamentoModel>(this.apiUrl, departamento);
  }

  // Atualizar um departamento existente
  atualizarDepartamento(departamento: DepartamentoModel): Observable<DepartamentoModel> {
    return this.http.put<DepartamentoModel>(`${this.apiUrl}/${departamento.departamentoId}`, departamento);
  }

  // Excluir um departamento
  excluirDepartamento(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
