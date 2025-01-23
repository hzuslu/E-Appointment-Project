import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LoginRequestDto } from '../../models/login_request_model';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/http.service';
import { LoginResponseDto } from '../../models/login_response_model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, FormValidateDirective],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @ViewChild("password") password: ElementRef<HTMLInputElement> | null = null;
  public model: LoginRequestDto = new LoginRequestDto();


  constructor(
    private _httpService: HttpService,
    private _routerService: Router
  ) { }

  showOrHidePassword() {
    if (this.password == undefined) return;

    if (this.password.nativeElement.type === "password") {
      this.password.nativeElement.type = "text";
    }
    else {
      this.password.nativeElement.type = "password";
    }
  }

  signIn(form: NgForm) {
    if (form.valid) {
      this._httpService.post<LoginResponseDto>("Auth/Login", this.model, (res) => {
        localStorage.setItem("token", res.data!.token!)
        this._routerService.navigateByUrl("/")
      }, (res) => {
        console.log(res.error);
      })
    }
  }
}
