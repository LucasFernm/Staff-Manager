import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartamentoService } from '../../services/departamento.service';
import { FuncionarioService } from '../../services/funcionario.service';
import { FuncionarioModel } from '../../models/funcionario.model';

@Component({
  selector: 'app-funcionario-list',
  templateUrl: './funcionario-lista.component.html',
  styleUrls: ['./funcionario-lista.component.css'],
})
export class FuncionarioListComponent implements OnInit {
  funcionarios: FuncionarioModel[] = [];
  departamentoId!: number;
  departamentoNome: string = '';
  exibirFormularioModal = false;
  funcionarioEditando: FuncionarioModel | null = null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private departamentoService: DepartamentoService,
    private funcionarioService: FuncionarioService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.departamentoId = +params['id'];
      this.carregarInformacoesDoDepartamento();
    });
  }

  carregarInformacoesDoDepartamento(): void {
    this.departamentoService.getDepartamentoById(this.departamentoId).subscribe(departamento => {
      this.departamentoNome = departamento.nome;
      this.funcionarios = departamento.funcionarios || [];
    }, error => {
      console.error('Erro ao carregar informações do departamento', error);
    });
  }

  adicionarFuncionario(): void {
    this.funcionarioEditando = null;
    this.exibirFormularioModal = true;
  }

  editarFuncionario(funcionario: FuncionarioModel): void {
    this.funcionarioEditando = funcionario;
    this.exibirFormularioModal = true;
  }

  excluirFuncionario(funcionarioId: number | undefined): void {
    if (funcionarioId === undefined) return;
    if (confirm('Tem certeza que deseja excluir este funcionário?')) {
      this.funcionarioService.excluirFuncionario(funcionarioId).subscribe({
        next: () => {
          alert('Funcionário excluído com sucesso.');
          this.carregarInformacoesDoDepartamento();
        },
        error: (e) => console.error(e),
      });
    }
  }

  fecharModal(): void {
    this.exibirFormularioModal = false;
    this.carregarInformacoesDoDepartamento();
  }
}
