import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from '../models/company';
import { ListResponse } from '../models/global';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  constructor(private http: HttpClient) {}

  getCompanies(
    hasVacancies: boolean | null = null
  ): Observable<ListResponse<Company>> {
    let params = {};
    if (hasVacancies) {
      params = { hasVacancies };
    }
    return this.http.get<ListResponse<Company>>(
      `${environment.apiUrl}/companies`,
      {
        params: params,
      }
    );
  }
}
