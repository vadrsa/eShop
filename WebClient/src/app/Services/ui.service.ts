import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../Helpers/config';
import { from } from 'rxjs';
import { map, publishReplay, refCount } from 'rxjs/operators';
import { ApiService } from './api.service';
import { SlideItem } from '../Entities/slideItem';
@Injectable({
  providedIn: 'root'
})
export class UIService {

  constructor(
    private apiService: ApiService
  ) {
  }

  getHomeSlider(): Observable<SlideItem[]>{
    return this.apiService.get('UI/HomeSlider');
  }
}
