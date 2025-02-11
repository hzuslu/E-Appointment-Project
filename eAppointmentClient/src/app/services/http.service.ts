import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResultModel } from '../models/result.model';
import { api } from '../constants';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  token: string = localStorage.getItem('token') ?? '';

  constructor(private http: HttpClient, private _errorService: ErrorService) {}

  post<T>(
    api_url: string,
    body: any,
    callback: (res: ResultModel<T>) => void,
    errCallBack?: (err: HttpErrorResponse) => void
  ) {
    this.http
      .post<ResultModel<T>>(`${api}/${api_url}`, body, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .subscribe({
        next: (res) => {
          if (res.data !== null && res.data !== undefined) callback(res);
        },
        error: (err: HttpErrorResponse) => {
          if (errCallBack !== undefined) errCallBack(err);
          this._errorService.errorHandler(err);
        },
      });
  }
}
