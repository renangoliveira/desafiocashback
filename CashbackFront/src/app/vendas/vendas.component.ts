import { Component, OnInit, TemplateRef } from '@angular/core';
import { VendaService } from '../_services/venda.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Venda } from '../_models/Venda';
import { Cashback } from '../_models/Cashback';
import { HttpHeaders } from '@angular/common/http';
import { NavComponent } from '../nav/nav.component';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-vendas',
  templateUrl: './vendas.component.html',
  styleUrls: ['./vendas.component.css']
})
export class VendasComponent implements OnInit {

  titulo = 'Vendas';
  dataVenda: string;
  vendas: Venda[];
  venda: Venda;
  cashback: Cashback;
  oldId: number;

  modoSalvar = 'post';

  registerForm: FormGroup;
  bodyDeletarVenda = '';

  file: File;
  fileNameToUpdate: string;

  dataAtual: string;

  _filtroLista = '';

  constructor(
    private vendaService: VendaService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  editarVenda(venda: Venda, template: any) {
    if (venda.status === 'Aprovado') {
      this.toastr.error('Erro ao tentar Editar - Vendas com Status Aprovado, NÃO podem ser editadas');
      template.hide();
      this.getVendas();
    } else {
      this.oldId = venda.codigoId;
      this.modoSalvar = 'put';
      this.openModal(template);
      this.venda = Object.assign({}, venda);
      this.registerForm.patchValue(this.venda);
    }
  }

  novaVenda(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirVenda(venda: Venda, template: any) {
   if (venda.status === 'Aprovado') {
      this.toastr.error('Erro ao tentar Deletar - Vendas com Status Aprovado, NÃO podem ser deletadas');
      template.hide();
      this.getVendas();
    } else {
      this.openModal(template);
      this.venda = venda;
      this.bodyDeletarVenda = `Tem certeza que deseja excluir a venda com valor: ${venda.valor}, Código: ${venda.codigoId}`;
    }
  }

  confirmeDelete(template: any) {
    this.vendaService.deleteVenda(this.venda).subscribe(
      () => {
        template.hide();
        this.getVendas();
        this.toastr.success('Deletado com Sucesso');
      }, error => {
        this.toastr.error('Erro ao tentar Deletar');
        console.log(error);
      }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getVendas();
    this.getCashBack();
  }

  validation() {
    this.registerForm = this.fb.group({
      codigoId: ['', Validators.required],
      valor: ['', Validators.required],
      data: ['', Validators.required]
    });
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        var retornoUsername = sessionStorage.getItem('username');

        this.venda = Object.assign({}, this.registerForm.value);
        this.venda.username = retornoUsername.toString();
        this.vendaService.postVenda(this.venda).subscribe(
          (novaVenda: Venda) => {
            template.hide();
            this.getVendas();
            this.toastr.success('Venda inserida com Sucesso!');
          }, error => {
            this.toastr.error(`Erro ao Inserir`);
          }
        );
      } else {
        this.venda = Object.assign({ id: this.venda.codigoId }, this.registerForm.value);
        var retornoUsername = sessionStorage.getItem('username');
        this.venda.username = retornoUsername.toString();
        this.vendaService.putVenda(this.oldId, this.venda).subscribe(
          () => {
            template.hide();
            this.getVendas();
            this.toastr.success('Editado com Sucesso!');
          }, error => {
            this.toastr.error(`Erro ao Editar`);
          }
        );
      }
    }
  }

  getVendas() {
    console.log('GetVendas()');
    this.vendaService.getAllVendas(sessionStorage.getItem('username')).subscribe(
      (_vendas: Venda[]) => {
        this.vendas = _vendas;
        console.log(this.vendas);
      }, error => {
        this.toastr.error(`Erro ao tentar Carregar vendas: ${error}`);
      });
  }

  getCashBack() {
    console.log('GetCashBack()');
    this.vendaService.getCashBack(sessionStorage.getItem('username')).subscribe(
      (_cashback: Cashback) => {
        this.cashback = _cashback;
        console.log(this.cashback);
      }, error => {
        this.toastr.error(`Erro ao tentar Carregar cashback`);
      });
  }

  userName() {
    return sessionStorage.getItem('username');
  }
}
