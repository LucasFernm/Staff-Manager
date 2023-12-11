import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartamentoService } from '../../services/departamento.service';
import { DepartamentoModel } from '../../models/departamento.model';

@Component({
  selector: 'app-departamento-form',
  templateUrl: './departamento-form.component.html',
  styleUrls: ['./departamento-form.component.css'],
})
export class DepartamentoFormComponent implements OnInit {
  @Input() departamento: DepartamentoModel | null = null;
  @Output() fecharFormulario = new EventEmitter<void>();
  departamentoForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private departamentoService: DepartamentoService
  ) {
    this.departamentoForm = this.fb.group({
      nome: ['', Validators.required],
      sigla: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    if (this.departamento) {
      this.departamentoForm.patchValue(this.departamento);
    }
  }

  salvar(): void {
    if (this.departamentoForm.valid) {
      const departamentoData = this.departamentoForm.value;
      let operacao;
      if (this.departamento && this.departamento.departamentoId) {
        operacao = this.departamentoService.atualizarDepartamento({ ...departamentoData, departamentoId: this.departamento.departamentoId });
      } else {
        operacao = this.departamentoService.criarDepartamento(departamentoData);
      }
      operacao.subscribe({
        next: () => {
          alert('Operação realizada com sucesso.');
          this.fecharFormulario.emit();
        },
        error: (e) => console.error(e)
      });
    } else {
      console.error('Formulário inválido');
    }
  }
}
