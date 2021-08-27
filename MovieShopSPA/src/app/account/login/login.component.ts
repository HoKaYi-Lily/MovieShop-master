import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login = {
    email: '',
    password: ''
  };

  invalidLogin!: boolean;


  constructor(private authService: AuthService, private router:
     Router, private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    console.log('inside ng OnInit Login Component');
    console.log(this.login);
  }

    // Router => we use to programaticallr route to some URL
  // ActivatedRoute => to read the current route => localhost:4200/movies/details/22 =>
  loginSubmit() {
    // console.log(f);
    // console.log('submit button clicked');
    //console.log(this.login);
    // send this object to a Service

    this.authService.login(this.login)
      .subscribe(
        (response: any) => {
          if (response) {
            // go to home page
            this.router.navigate(['/']);
          }
          else {
            this.invalidLogin = true;
          }
        }, (err: any) => {
          this.invalidLogin = true,
            console.log(err);
        }
      )
  }

}
