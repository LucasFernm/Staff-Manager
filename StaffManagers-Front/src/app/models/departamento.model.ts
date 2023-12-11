import { FuncionarioModel } from "./funcionario.model";

export interface DepartamentoModel {
  departamentoId: number;
  nome: string;
  sigla: string;
  funcionarios: FuncionarioModel[];
}
