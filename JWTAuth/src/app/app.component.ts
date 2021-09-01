import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';
import { User } from './_models';

@Component({ selector: 'app',
             templateUrl: 'app.component.html',
             styleUrls: ['app.component.css']})
export class AppComponent implements OnInit{
    currentUser: User;
    isLoggedIn: boolean;
    isAdmin: boolean = false;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }
    ngOnInit(): void {
        var user = this.authenticationService.getUsername();
        if(user==null)
        {
            this.isLoggedIn = false;
        }
        else
        {
            this.isLoggedIn = true;
            
            //check is admin
            if(user.role === 'Admin'){
                this.isAdmin = true;
            }else{
                this.isAdmin = false;
            }
        }
    }

    logout() {
        this.authenticationService.logout();
        this.isLoggedIn = false;
        this.router.navigate(['']);
    }
}
