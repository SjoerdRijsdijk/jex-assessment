import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../company/company.service';
import { Company } from '../models/company';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { VacancyService } from '../vacancy/vacancy.service';
import { Vacancy } from '../models/vacancy';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss',
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class CompanyListComponent implements OnInit {
  dataSource: Company[] = [];
  columnsToDisplay: string[] = ['name', 'address'];
  columnsToDisplayWithExpand: string[] = [...this.columnsToDisplay, 'expand'];
  expandedElement: Company | null = null;

  vacancies: Vacancy[] = [];

  constructor(
    private companyService: CompanyService,
    private vacancyService: VacancyService
  ) {}
  ngOnInit(): void {
    this.companyService.getCompanies(true).subscribe((data) => {
      this.dataSource = data.items;
    });
  }

  onExpand(element: Company): void {
    this.vacancies = [];
    this.expandedElement = this.expandedElement === element ? null : element;
    if (this.expandedElement) {
      this.vacancyService
        .getVacancies(this.expandedElement.id)
        .subscribe((data) => {
          this.vacancies = data.items;
        });
    }
  }
}
