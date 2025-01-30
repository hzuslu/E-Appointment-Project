import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from "../layouts/navbar/navbar.component";
import { HttpService } from '../../services/http.service';
import { DoctorModel } from '../../models/doctor.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule],
  templateUrl: './doctor.component.html',
  styleUrl: './doctor.component.css'
})
export class DoctorComponent implements OnInit {

  doctors: DoctorModel[] = [];

  constructor(private _httpService: HttpService) {
  }
  ngOnInit(): void {
    this.getAll()
  }

  getAll() {
    this._httpService.post<DoctorModel[]>("Doctors/GetAllDoctors", {}, res => {
      this.doctors = res.data;
    })
  }
}
