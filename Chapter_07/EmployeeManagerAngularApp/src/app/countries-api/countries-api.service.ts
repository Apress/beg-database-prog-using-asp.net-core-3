import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class CountriesApiService {

  //baseUrl: string = 'http://localhost:5000/api/countries';

  baseUrl: string = '/api/countries';

  constructor(private _http: HttpClient) {

  }

  selectAll() {
    return this._http.get(this.baseUrl, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }

}
