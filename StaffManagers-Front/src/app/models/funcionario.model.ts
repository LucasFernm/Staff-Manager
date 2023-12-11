export interface FuncionarioModel {
  id?: number;
  nome: string;
  foto?: string;
  rg: string;
  cargo: string;
  dataDeNascimento: Date;
  departamentoId: number;
}
