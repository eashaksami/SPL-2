import { Course } from './../_models/Course';
import { Component, OnInit } from '@angular/core';
import { CourseService } from '@app/_services/course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-home-page',
  templateUrl: './admin-home-page.component.html',
  styleUrls: ['./admin-home-page.component.less']
})
export class AdminHomePageComponent implements OnInit {

  courses: Course[];
  isLoading: boolean = false;

  constructor(public courseService: CourseService,
              private router: Router) { }

  ngOnInit(): void {
    this.getAllCourses();
  }

  getAllCourses(){
    this.isLoading = true;
    this.courseService.getAllCourses()
    .subscribe((courses: Course[]) => {
      this.isLoading = false;
      this.courses = courses;
      console.log(this.courses);
    });
  }

  // Enroll(){
  //   this.router.navigate(['/admin/updateCourse']);
  // }

}
