import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../Helpers/config';
import { from } from 'rxjs';
import { map, publishReplay, refCount } from 'rxjs/operators';
import { Product, ProductOrderType } from '../Entities';
import { ApiService } from './api.service';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  _orderCriterias: Observable<ProductOrderType[]>;

  constructor(
    private http: HttpClient,
    private apiService: ApiService
  ) {
  }

  getAllProducts(): Observable<Product[]>{
    return this.apiService.get('products');
  }

  getByID(id: number): Observable<Product>{
    return this.apiService.get<Product>('products/' + id);
  }

  getOrderCriterias(): Observable<ProductOrderType[]>{
    if(!this._orderCriterias)
      this._orderCriterias = this.apiService.get<ProductOrderType[]>('products/orderCriterias').pipe(publishReplay(), refCount());
    return this._orderCriterias;
  }

  serach(query: string, categoryID: number, limit: number, start: number, orderBy: string = undefined, ascending = true): Observable<Product[]>{
    return this.apiService.get<Product[]>('products/search', {query, categoryID, limit, start, orderBy, ascending});
  }

  searchCount(query: string, categoryID: number){
    return this.apiService.get<number>('products/searchCount', {query, categoryID});
  }

}
