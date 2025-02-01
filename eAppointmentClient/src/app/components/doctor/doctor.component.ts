import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { DepartmentModel, DoctorModel } from '../../models/doctor.model';
import { CommonModule } from '@angular/common';
import { departmentList } from '../../constants';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { SwalService } from '../../services/swal.service';
import { DoctorPipe } from '../../pipes/doctor.pipe';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    FormsModule,
    DoctorPipe,
    FormValidateDirective],
  templateUrl: './doctor.component.html',
  styleUrl: './doctor.component.css'
})
export class DoctorComponent implements OnInit {

  createModel: DoctorModel;
  updateModel: DoctorModel;
  doctors: DoctorModel[] = [];
  departments: DepartmentModel[];
  search: string = "";
  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined

  constructor(
    private _httpService: HttpService,
    private _swalService: SwalService
  ) {
  }
  ngOnInit(): void {
    this.getAll()
    this.departments = departmentList;
    this.createModel = new DoctorModel();
    this.updateModel = new DoctorModel();
  }

  getAll() {
    this._httpService.post<DoctorModel[]>("Doctors/GetAllDoctors", {}, res => {
      this.doctors = res.data;
    })
  }

  add(form: NgForm) {
    if (form.valid) {
      this._httpService.post("Doctors/CreateDoctor", this.createModel, res => {
        this.getAll();
        this.addModalCloseBtn.nativeElement.click();
        this._swalService.callToast("Doctor created successfully");
        this.createModel = new DoctorModel();
      })
    }
  }

  get(data: DoctorModel) {
    this.updateModel = { ...data }
    this.updateModel.departmentValue = data.department.value
  }

  update(form: NgForm) {
    if (form.valid) {
      this._httpService.post("Doctors/UpdateDoctor", this.updateModel, res => {
        this.getAll();
        this.updateModalCloseBtn.nativeElement.click();
        this._swalService.callToast("Doctor updated successfully");
        this.updateModel = new DoctorModel();
      })
    }
  }

  delete(id: string, fullName: string) {
    this._swalService.callSwal(
      "Delete doctor",
      `Are you sure you want to delete the doctor? ${fullName}`,
      () => {
        this._httpService.post<string>(
          "Doctors/DeleteDoctorById",
          { doctorId: id },
          res => {
            this.getAll();
            this._swalService.callToast(res.data, "info");
          },
          err => {
            this._swalService.callToast(err.error.errorMessages, "error");
          }
        );
      }
    );
  }

}
