import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../Helpers/config';
import { from } from 'rxjs';
import { map } from 'rxjs/operators';
import { Brand, Product } from '../Entities';
import { ApiService } from './api.service';
@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(
    private http: HttpClient,
    private apiService:ApiService
  ) {
  }

  getBar(): Observable<Brand[]>{
    return this.apiService.get('brands/baritems');
  }

  getByID(id:number): Observable<Brand>{
    return this.apiService.get('brands/'+ id);
  }

  getProductsByID(id:number, limit: number, start: number, orderBy: string = undefined, ascending = true): Observable<Product[]>{
    return this.apiService.get('brands/'+ id + '/products/', {limit, start, orderBy, ascending});    
  }

  getProductsCountByID(id:number): Observable<number>{
    return this.apiService.get('brands/'+ id + '/productsCount');
  }

}
