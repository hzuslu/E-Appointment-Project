import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() { }

  callToast(title: string, icon: SweetAlertIcon = 'success') {
    Swal.fire({
      title: title,
      timer: 2000,
      icon: icon,
      showCloseButton: false,
      toast: true,
      position: 'bottom',
      showConfirmButton: false,
    });
  }

  callSwal(title: string, text: string, callback: () => void) {
    Swal.fire({
      title: title,
      text: text,
      icon: 'question',
      showConfirmButton: true,
      confirmButtonText: "Delete",
      showCancelButton: true,
      cancelButtonText: "Cancel"
    }).then(res => {
      if (res.isConfirmed) {
        callback()
      }
    })
  }
}
