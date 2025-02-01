import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(
    private _swalService: SwalService
  ) { }

  errorHandler(error: HttpErrorResponse) {
    let message = "";

    if (error.error?.errorMessages && Array.isArray(error.error.errorMessages)) message = error.error.errorMessages.join(" ");
    if (error.status === 0) message = "API is not available";
    else if (error.status === 400) message = "Bad Request: The server could not understand the request.";
    else if (error.status === 401) message = "Unauthorized: Please check your authentication.";
    else if (error.status === 403) message = "Forbidden: You do not have permission to access this resource.";
    else if (error.status === 404) message = "Not Found: The requested resource could not be found.";
    else if (error.status === 408) message = "Request Timeout: The server took too long to respond.";
    else if (error.status === 409) message = "Conflict: The request could not be completed due to a conflict.";
    else if (error.status === 500) message = "Internal Server Error: Please try again later.";
    else if (error.status === 502) message = "Bad Gateway: Received an invalid response from the upstream server.";
    else if (error.status === 503) message = "Service Unavailable: The server is temporarily unavailable.";
    else if (error.status === 504) message = "Gateway Timeout: The server did not receive a timely response.";
    else message = `Unexpected Error: ${error.message}`;

    this._swalService.callToast(message, 'error');
  }


}
