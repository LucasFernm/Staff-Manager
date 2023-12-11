import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FuncionarioModel } from '../models/funcionario.model';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioService {
  private apiUrl = 'https://localhost:7034/api/Funcionario';

  constructor(private http: HttpClient) { }

  // Obter todos os funcionários
  getFuncionarios(): Observable<FuncionarioModel[]> {
    return this.http.get<FuncionarioModel[]>(this.apiUrl);
  }

  // Obter um funcionário pelo ID
  getFuncionarioById(id: number): Observable<FuncionarioModel> {
    return this.http.get<FuncionarioModel>(`${this.apiUrl}/${id}`);
  }

  getFuncionariosDoDepartamento(departamentoId: number): Observable<FuncionarioModel[]> {
    return this.http.get<FuncionarioModel[]>(`${this.apiUrl}/${departamentoId}`);
  }

  // Criar um novo funcionário
  criarFuncionario(funcionario: FuncionarioModel): Observable<FuncionarioModel> {
    return this.http.post<FuncionarioModel>(this.apiUrl, funcionario);
  }

  // Atualizar um funcionário existente
  atualizarFuncionario(funcionario: FuncionarioModel): Observable<FuncionarioModel> {
    return this.http.put<FuncionarioModel>(`${this.apiUrl}/${funcionario.id}`, funcionario);
  }

  // Excluir um funcionário
  excluirFuncionario(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
