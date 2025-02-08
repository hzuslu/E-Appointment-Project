import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { departmentList } from '../../constants';
import { DoctorModel } from '../../models/doctor.model';
import { HttpService } from '../../services/http.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { DxSchedulerModule } from 'devextreme-angular';
import { SwalService } from '../../services/swal.service';
import { AppointmentModel } from '../../models/appointment.model';
import { CreateAppointmentModel } from '../../models/appointment.create.model';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientModel } from '../../models/patient.model';
import { ResultModel } from '../../models/result.model';

declare const $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    DxSchedulerModule,
    FormValidateDirective,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [DatePipe],
})
export class HomeComponent implements OnInit {
  createModel: CreateAppointmentModel = new CreateAppointmentModel();
  @ViewChild('addModalCloseBtn') addModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  constructor(
    private _httpService: HttpService,
    private _swalService: SwalService,
    private date: DatePipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit() {
    const modalElement = document.getElementById('addModal');

    modalElement?.addEventListener('hidden.bs.modal', () => {
      document.body.style.paddingRight = '';
      document.body.style.overflow = '';
    });
  }

  departmentList = departmentList;
  doctorList: DoctorModel[] = [];
  appointmentList: AppointmentModel[] = [];
  selectedDepartmentValue: number = 0;
  selectedDoctorId: string = '';

  getDoctorByDepartment() {
    if (this.selectedDepartmentValue > 0) {
      this.selectedDoctorId = '';
      this._httpService.post<DoctorModel[]>(
        'Appointments/GetAllDoctorsByDepartment',
        { departmentId: this.selectedDepartmentValue },
        (res) => {
          this.doctorList = res.data;
          if (this.doctorList.length == 0) {
            this._swalService.callToast(
              'There is no doctor with the selected department',
              'warning'
            );
          }
        }
      );
    }
  }

  getAllAppointmentsByDoctorId() {
    if (this.selectedDoctorId) {
      this._httpService.post<AppointmentModel[]>(
        'Appointments/GetAllAppointmentsByDoctorId',
        { doctorId: this.selectedDoctorId },
        (res) => {
          this.appointmentList = res.data;
        }
      );
    }
  }

  onAppointmentFormOpening(e: any) {
    e.cancel = true;
    this.createModel.startDate =
      this.date.transform(e.appointmentData.startDate, 'dd.MM.yyyy HH:mm') ??
      '';

    this.createModel.endDate =
      this.date.transform(e.appointmentData.endDate, 'dd.MM.yyyy HH:mm') ?? '';

    this.createModel.doctorId = this.selectedDoctorId;

    $('#addModal').modal('show');
  }

  getPatient() {
    this._httpService.post<PatientModel>(
      'Appointments/GetPatientById',
      { identityNumber: this.createModel.identityNumber },
      (res) => {
        if (res && res.data) {
          const {
            identityNumber,
            firstName,
            lastName,
            city,
            town,
            fullAddress,
            id,
          } = res.data;

          Object.assign(this.createModel, {
            identityNumber,
            firstName,
            lastName,
            city,
            town,
            fullAddress,
            patientId: id,
          });
        } else {
          Object.assign(this.createModel, {
            patientId: null,
            firstName: '',
            lastName: '',
            city: '',
            town: '',
            fullAdress: '',
            id: '',
          });
        }
      }
    );
  }

  create(form: NgForm) {
    if (form.valid) {
      this._httpService.post(
        'Appointments/CreateAppointment',
        this.createModel,
        (res) => {
          this.addModalCloseBtn.nativeElement.click();
          this._swalService.callToast(`${res.data}`, 'info');
          this.getAllAppointmentsByDoctorId();
        }
      );
    }
  }

  onAppointmentDeleted(e: any) {
    e.cancel = true;
  }

  onAppointmentDeleting(e: any) {
    e.cancel = true;
    console.log(e);
    this._swalService.callSwal(
      'Delete Appointment',
      'Dou you want to delete this appointment?',
      () => {
        this._httpService.post(
          'Appointments/DeleteAppointment',
          {
            id: e.appointmentData.id,
          },
          (res: ResultModel<string>) => {
            this._swalService.callToast(res.data, 'info');
            this.getAllAppointmentsByDoctorId();
          }
        );
      }
    );
  }

  onAppointmentUpdating(e: any) {
    e.cancel = true;
    this._httpService.post(
      'Appointments/UpdateAppointment',
      {
        id: e.newData.id,
        startDate: e.newData.startDate,
        endDate: e.newData.endDate,
      },
      (res: ResultModel<string>) => {
        this._swalService.callToast(res.data, 'info');
        this.getAllAppointmentsByDoctorId();
      }
    );
  }
}
