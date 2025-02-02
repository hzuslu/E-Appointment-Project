import { Component, OnInit } from '@angular/core';
import { departmentList } from '../../constants';
import { DoctorModel } from '../../models/doctor.model';
import { HttpService } from '../../services/http.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DxSchedulerModule } from 'devextreme-angular';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule, DxSchedulerModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private _httpService: HttpService
  ) {
  }
  ngOnInit(): void {
    this.getDoctorList()
  }

  departmentList = departmentList
  doctorList: DoctorModel[] = []
  appointments: any = [
    {
      startDate: new Date("2025-02-02 09:00"),
      endDate: new Date("2025-02-02 09:30"),
      title: "Hasan Uslu"
    },
    {
      startDate: new Date("2025-02-02 10:00"),
      endDate: new Date("2025-02-02 10:30"),
      title: "Ahmet Demir"
    },
    {
      startDate: new Date("2025-02-02 11:00"),
      endDate: new Date("2025-02-02 11:30"),
      title: "Mehmet Yalçın"
    },

  ]

  selectedDepartmentValue: number = 0;
  selectedDoctorId: string = ""

  getDoctorList() {
    this._httpService.post<DoctorModel[]>("Doctors/GetAllDoctors", {}, res => {
      this.doctorList = res.data;
    })
  }
}
