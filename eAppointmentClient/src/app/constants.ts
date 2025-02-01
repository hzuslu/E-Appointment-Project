import { DepartmentModel } from './models/doctor.model';

export const api: string = "https://localhost:7142/api";

export const departmentList: DepartmentModel[] = [
	{ value: 1, name: 'Cardiology' },
	{ value: 2, name: 'Dermatology' },
	{ value: 3, name: 'Gastroenterology' },
	{ value: 4, name: 'General Surgery' },
	{ value: 5, name: 'Gynecology' },
	{ value: 6, name: 'Neurology' },
	{ value: 7, name: 'Ophthalmology' },
	{ value: 8, name: 'Orthopedics' },
	{ value: 9, name: 'Pediatrics' },
	{ value: 10, name: 'Urology' }
];

