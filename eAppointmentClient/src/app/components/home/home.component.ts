import { Component, OnInit } from '@angular/core';
import { departmentList } from '../../constants';
import { DoctorModel } from '../../models/doctor.model';
import { HttpService } from '../../services/http.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DxSchedulerModule } from 'devextreme-angular';
import { SwalService } from '../../services/swal.service';
import { AppointmentModel } from '../../models/appointment.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule, DxSchedulerModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private _httpService: HttpService,
    private _swalService: SwalService
  ) {
  }

  ngOnInit(): void {
  }

  departmentList = departmentList
  doctorList: DoctorModel[] = []
  appointmentList: AppointmentModel[] = []
  selectedDepartmentValue: number = 0;
  selectedDoctorId: string = ""

  getDoctorByDepartment() {
    if (this.selectedDepartmentValue > 0) {
      let tempDoctorId = this.selectedDoctorId;
      this.selectedDoctorId = "";
      this._httpService.post<DoctorModel[]>("Appointments/GetAllDoctorsByDepartment", { departmentId: this.selectedDepartmentValue }, res => {
        this.doctorList = res.data;
        if (this.doctorList.length == 0) {
          this._swalService.callToast("There is no doctor with the selected department", 'warning');
        }

      })
    }
  }

  getAllAppointmentsByDoctorId() {
    if (this.selectedDoctorId) {
      this._httpService.post<AppointmentModel[]>("Appointments/GetAllAppointmentsByDoctorId", { doctorId: this.selectedDoctorId }, res => {
        this.appointmentList = res.data;
      })
    }
  }
}
