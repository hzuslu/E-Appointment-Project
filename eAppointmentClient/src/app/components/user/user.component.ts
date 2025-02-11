import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { UserModel } from '../../models/user.model';
import { UserPipe } from '../../pipes/user.pipe';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    FormsModule,
    UserPipe,
    FormValidateDirective,
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
})
export class UserComponent implements OnInit {
  createModel: UserModel = new UserModel();
  updateModel: UserModel = new UserModel();
  users: UserModel[] = [];
  search: string = '';

  @ViewChild('addModalCloseBtn') addModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  @ViewChild('updateModalCloseBtn') updateModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  constructor(
    private _httpService: HttpService,
    private _swalService: SwalService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this._httpService.post<UserModel[]>('Users/GetAllUsers', {}, (res) => {
      this.users = res.data;
    });
  }

  add(form: NgForm) {
    if (form.valid) {
      this._httpService.post('Users/CreateUser', this.createModel, (res) => {
        this.getAll();
        this.addModalCloseBtn.nativeElement.click();
        this._swalService.callToast('User created successfully');
        this.createModel = new UserModel();
      });
    }
  }

  get(data: UserModel) {
    this.updateModel = { ...data };
  }

  update(form: NgForm) {
    if (form.valid) {
      this._httpService.post('Users/UpdateUser', this.updateModel, (res) => {
        this.getAll();
        this.updateModalCloseBtn.nativeElement.click();
        this._swalService.callToast('User updated successfully');
        this.updateModel = new UserModel();
      });
    }
  }

  delete(id: string, fullName: string) {
    this._swalService.callSwal(
      'Delete User',
      `Are you sure you want to delete the User? ${fullName}`,
      () => {
        this._httpService.post<string>(
          'Users/DeleteUserById',
          { UserId: id },
          (res) => {
            this.getAll();
            this._swalService.callToast(res.data, 'info');
          },
          (err) => {
            this._swalService.callToast(err.error.errorMessages, 'error');
          }
        );
      }
    );
  }
}
