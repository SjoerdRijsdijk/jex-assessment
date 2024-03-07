export interface Vacancy {
  id: number;
  title: string;
  description: string;
}

export interface SaveVacancy {
  companyId: number;
  title: string;
  description: string;
}
