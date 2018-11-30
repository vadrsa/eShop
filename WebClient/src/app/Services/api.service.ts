import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Config } from '../Helpers/config';
import { AlertService } from './alert.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(
    private http: HttpClient,
    private alertService: AlertService
  ) {
  }

  private formatErrors(error: any) {
    // if(error.status === 0)
    //   this.alertService.error("Cannot connect to the server.");
    // else this.alertService.error(error.error.message);

    return  throwError(error.error);
  }
  
  encodeQueryData(data) : string {
    let ret = [];
    for (let d in data){
      if(data[d] != undefined)
        ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));

    }
    return ret.join('&');
  }

  get<T>(path: string, queryParams = {}, params: HttpParams = new HttpParams()): Observable<T> {
    let qparams = this.encodeQueryData(queryParams);
    qparams = (qparams && qparams != '')? '?'+qparams: '';
    return this.http.get<T>(Config.apiUrl+path + qparams, { params });
  }

  put(path: string, body: Object = {}): Observable<any> {
    return this.http.put(
      `${Config.apiUrl}${path}`,
      JSON.stringify(body)
    ).pipe(catchError(this.formatErrors));
  }

  post(path: string, body: Object = {}): Observable<any> {
    return this.http.post(
      `${Config.apiUrl}${path}`,
      JSON.stringify(body)
    ).pipe(catchError(this.formatErrors));
  }

  delete(path): Observable<any> {
    return this.http.delete(
      `${Config.apiUrl}${path}`
    ).pipe(catchError(this.formatErrors));
  }
}