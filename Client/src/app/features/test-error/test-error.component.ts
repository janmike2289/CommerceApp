import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  imports: [
    MatButton,
  ],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})

export class TestErrorComponent {
  baseUrl='http://localhost:5241/api/'
  private http = inject(HttpClient);
  validationErrors?: string[];

  getNotFoundError() {
    this.http.get(this.baseUrl + 'BugTester/notfound').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

    getBadRequestError() {
    this.http.get(this.baseUrl + 'BugTester/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

    getUnauthorizedError() {
    this.http.get(this.baseUrl + 'BugTester/unauthorized').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

    getInternalServerError() {
    this.http.get(this.baseUrl + 'BugTester/internalerror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

    getValidationError() {
    this.http.post(this.baseUrl + 'BugTester/validationerror', {"name": ""}).subscribe({
      next: response => console.log(response),
      error: error => this.validationErrors = error
    })
  }
}
