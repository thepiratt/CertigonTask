import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form!: FormGroup;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username:['', [Validators.required, Validators.minLength(4),Validators.maxLength(10)]],
      email:['', [Validators.required, Validators.email]],
      password:['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]]
    })
  }

  get username() { return this.form.get('username'); }
  get email() { return this.form.get('email'); }
  get password() { return this.form.get('password'); }

  submit(): void {
    if (this.form.invalid) {
      porukaError("Registration not successful!");
      return;
    }
    const val = this.form.value;
    this.httpClient.post('http://localhost:5000/Autentifikacija/Register', val)
      .subscribe((res: any) => {
            this.router.navigate(['/login']);
      });
  }
}
