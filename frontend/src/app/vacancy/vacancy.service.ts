import { Injectable } from '@angular/core';
import { SaveVacancy, Vacancy } from '../models/vacancy';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponse } from '../models/global';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class VacancyService {
  private apiUrl = 'https://localhost:7146/api';
  constructor(private http: HttpClient) {}

  getVacancies(companyId: number): Observable<ListResponse<Vacancy>> {
    return this.http.get<ListResponse<Vacancy>>(
      `${environment.apiUrl}/vacancies`,
      {
        params: {
          companyId: companyId,
        },
      }
    );
  }

  createVacancy(vacancy: SaveVacancy) {
    return this.http.post(`${this.apiUrl}/vacancies`, vacancy);
  }
}
