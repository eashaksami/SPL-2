import { CourseService } from '@app/_services/course.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import * as jwt_decode from "jwt-decode";

import { AuthenticationService } from '@app/_services';

@Component({ templateUrl: 'login.component.html',
            styleUrls: ['login.component.css'] })
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    isLoading = false;
    submitted = false;
    returnUrl: string;
    error = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private courseService: CourseService
    ) { 
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }

        this.isLoading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe(
                data => {
                    if(this.authenticationService.getUsername().role === 'Admin'){
                        this.router.navigate(['/admin']).then(() => {
                            window.location.reload();
                          });
                        // this.reload();
                    }else if(this.authenticationService.getUsername().role === 'Student'){
                        this.router.navigate(['']).then(() => {
                            window.location.reload();
                          });
                    }else{
                        this.router.navigate(['/login']);
                    }
                    // this.router.navigate([this.returnUrl]);
                    // window.location.reload();
                    this.courseService.user=data;
                    // this.router.navigate(['/test']);
                    // this.router.navigate(['/']);
                    console.log(jwt_decode(this.courseService.user.token));
                    console.log(data);

                    // console.log(this.authenticationService.getUsername().studentId);
                },
                error => {
                    this.error = error;
                    this.isLoading = false;
                });
    }

    reload(){
        window.location.reload();
    }

    SignUp(){
        this.router.navigate(['/register']);
    }
}
