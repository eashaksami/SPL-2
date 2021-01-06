import { Component, OnInit } from '@angular/core';

import { Course } from '@app/_models/Course';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    loading = false;
    //users: User[];
    courses: Course[];

    constructor() { }

    ngOnInit() {
    }
}