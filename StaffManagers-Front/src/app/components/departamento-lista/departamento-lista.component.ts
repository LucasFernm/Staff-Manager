import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DepartamentoService } from '../../services/departamento.service';
import { DepartamentoModel } from '../../models/departamento.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-departamentos-lista',
  templateUrl: './departamento-lista.component.html',
  styleUrls: ['./departamento-lista.component.css'],
})
export class DepartamentosListaComponent implements OnInit {
  departamentos: DepartamentoModel[] = [];
  departamentoEditando: DepartamentoModel | null = null;
  exibirFormularioModal = false;
  @Output() onFormClose = new EventEmitter<void>();

  constructor(
    private departamentoService: DepartamentoService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarDepartamentos();
  }

  carregarDepartamentos(): void {
    this.departamentoService.getDepartamentos().subscribe({
      next: (data) => this.departamentos = data,
      error: (e) => console.error(e)
    });
  }

  navegarParaFuncionarios(departamentoId: number): void {
    this.router.navigate(['/departamentos', departamentoId, 'funcionarios']);
  }

  adicionarDepartamento(): void {
    this.departamentoEditando = null;
    this.exibirFormularioModal = true;
  }

  editarDepartamento(departamento: DepartamentoModel): void {
    this.departamentoEditando = departamento;
    this.exibirFormularioModal = true;
  }

  excluirDepartamento(departamentoId: number): void {
    if (confirm('Tem certeza que deseja excluir este departamento?')) {
      this.departamentoService.excluirDepartamento(departamentoId).subscribe({
        next: () => {
          alert('Departamento excluído com sucesso.');
          this.carregarDepartamentos();
        },
        error: (e) => console.error(e)
      });
    }
  }

  fecharModal(): void {
    this.exibirFormularioModal = false;
    this.carregarDepartamentos();
  }
}
