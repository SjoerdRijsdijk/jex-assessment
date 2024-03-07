import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VacancyService } from './vacancy.service';
import { CompanyService } from '../company/company.service';
import { Company } from '../models/company';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-vacancy',
  templateUrl: './vacancy.component.html',
  styleUrl: './vacancy.component.scss',
})
export class VacancyComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  companies: Company[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private vacancyService: VacancyService,
    private companyService: CompanyService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: [''],
      company: [null, Validators.required],
    });

    this.companyService.getCompanies().subscribe((data) => {
      this.companies = data.items;
    });
  }

  onSubmit() {
    if (!this.form.valid) {
      return;
    }

    this.vacancyService
      .createVacancy({
        title: this.form.get('title')?.value,
        description: this.form.get('description')?.value,
        companyId: this.form.get('company')!.value,
      })
      .subscribe((data) => {
        this.snackBar.open('Vacancy created successfully!', 'Close', {
          duration: 3000,
        });
      });
  }
}
