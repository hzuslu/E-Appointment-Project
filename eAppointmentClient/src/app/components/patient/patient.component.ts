import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientPipe } from '../../pipes/patient.pipe';
import { PatientModel } from '../../models/patient.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    FormsModule,
    PatientPipe,
    FormValidateDirective],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.css'
})
export class PatientComponent implements OnInit {

  createModel: PatientModel;
  updateModel: PatientModel;
  patients: PatientModel[] = [];
  search: string = "";
  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined

  constructor(
    private _httpService: HttpService,
    private _swalService: SwalService
  ) {
  }

  ngOnInit(): void {
    this.getAll();
    this.createModel = new PatientModel();
    this.updateModel = new PatientModel();
  }

  getAll() {
    this._httpService.post<PatientModel[]>("Patients/GetAllPatients", {}, res => {
      this.patients = res.data;
    })
  }

  add(form: NgForm) {
    if (form.valid) {
      this._httpService.post("Patients/CreatePatient", this.createModel, res => {
        this.getAll();
        this.addModalCloseBtn.nativeElement.click();
        this._swalService.callToast("Patient created successfully");
        this.createModel = new PatientModel();
      })
    }
  }

  get(data: PatientModel) {
    this.updateModel = { ...data }
  }

  update(form: NgForm) {
    if (form.valid) {
      this._httpService.post("Patients/UpdatePatient", this.updateModel, res => {
        this.getAll();
        this.updateModalCloseBtn.nativeElement.click();
        this._swalService.callToast("Patient updated successfully");
        this.updateModel = new PatientModel();
      })
    }
  }

  delete(id: string, fullName: string) {
    this._swalService.callSwal(
      "Delete Patient",
      `Are you sure you want to delete the Patient? ${fullName}`,
      () => {
        this._httpService.post<string>(
          "Patients/DeletePatientById",
          { PatientId: id },
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
