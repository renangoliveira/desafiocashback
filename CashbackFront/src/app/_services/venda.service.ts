import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Venda } from '../_models/Venda';
import { Cashback } from '../_models/Cashback';

@Injectable({
  providedIn: 'root'
})
export class VendaService {
  // baseURL = 'https://localhost:44338/api/vendas';
  baseURL = 'https://localhost:44338/api/vendas';


constructor(private http: HttpClient) { }

  getVenda() {
    return this.http.get(this.baseURL);
  }

  getVendaByID(id: number) {
    return this.http.get<Venda>(`${this.baseURL}/${id}`);
  }

  getAllVendas(username: string): Observable<Venda[]> {
    return this.http.get<Venda[]>(`${this.baseURL}/getVendasByUser/${username}`);
  }

  getCashBack(username: string): Observable<Cashback> {
    return this.http.get<Cashback>(`${this.baseURL}/getCashbackAcumulado/${username}`);
  }

  postVenda(venda: Venda) {
    return this.http.post(this.baseURL, venda);
  }

  putVenda(oldCodigo: number, venda: Venda) {
    return this.http.put(`${this.baseURL}/${oldCodigo}`, venda);
  }

  deleteVenda(venda: Venda) {
    return this.http.delete(`${this.baseURL}/${venda.codigoId}/${venda.username}/`);
  }
}
