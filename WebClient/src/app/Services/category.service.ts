import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../Helpers/config';
import { from } from 'rxjs';
import { map, publishReplay, refCount} from 'rxjs/operators';
import { Category, Product } from '../Entities';
import { ApiService } from './api.service';
@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  _tree: Observable<Category[]>;

  constructor(
    private http: HttpClient,
    private apiService: ApiService
  ) {
  }

  getCategoryTree(): Observable<Category[]>{
    if(!this._tree)
      this._tree = this.apiService.get<Category[]>('categories/tree').pipe(publishReplay(1), refCount());
    return this._tree;
  }

  getTopLevel(): Observable<Category[]>{
    return this.apiService.get('categories');
  }

  getByID(id:number): Observable<Category>{
    return this.apiService.get('categories/'+ id);
  }

  getProductsByID(id:number, limit: number, start: number, orderBy: string = undefined, ascending = true): Observable<Product[]>{
    return this.apiService.get('categories/'+ id + '/products/', {limit, start, orderBy, ascending});
  }

  getProductsCountByID(id:number): Observable<number>{
    return this.apiService.get('categories/'+ id + '/productsCount');
  }

  clearCache(){
    this._tree = null;
  }
}
