import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FuncionarioService } from '../../services/funcionario.service';
import { FuncionarioModel } from '../../models/funcionario.model';

@Component({
  selector: 'app-funcionario-form',
  templateUrl: './funcionario-form.component.html',
  styleUrls: ['./funcionario-form.component.css'],
})
export class FuncionarioFormComponent implements OnInit {
  @Input() funcionario: FuncionarioModel | null = null;
  @Input() departamentoId?: number;
  @Output() fecharFormulario = new EventEmitter<void>();
  funcionarioForm: FormGroup;
  funcionarioId: number | undefined;

  constructor(
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService
  ) {
    this.funcionarioForm = this.fb.group({
      nome: [''],
      foto: [''],
      rg: [''],
      cargo: [''],
      dataDeNascimento: [''],
      departamentoId: [this.departamentoId]
    });
  }

  ngOnInit(): void {
    if (this.funcionario) {
      this.funcionarioId = this.funcionario.id;
      this.funcionarioForm.patchValue(this.funcionario);
    } else {
      this.funcionarioForm.get('departamentoId')?.setValue(this.departamentoId);
    }
  }

  salvar(): void {
    if (this.funcionarioForm.valid) {
      const funcionarioData = { ...this.funcionarioForm.value, id: this.funcionarioId };
      if (this.funcionarioId) {
        this.funcionarioService.atualizarFuncionario(funcionarioData).subscribe({
          next: () => {
            alert('Funcionário atualizado com sucesso.');
            this.fecharFormulario.emit();
          },
          error: (e) => console.error(e)
        });
      } else {
        this.funcionarioService.criarFuncionario(funcionarioData).subscribe({
          next: () => {
            alert('Funcionário criado com sucesso.');
            this.fecharFormulario.emit();
          },
          error: (e) => console.error(e)
        });
      }
    }
  }
}
