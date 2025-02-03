import { PatientModel } from './patient.model'

export class AppointmentModel {
	id: string
	startDate: Date
	endDate: Date
	title: string
	patient: PatientModel
}
