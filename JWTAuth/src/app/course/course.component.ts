import { SubscriptionsService } from './../_services/subscriptions.service';
import { Component, OnInit } from '@angular/core';
import { Course } from '@app/_models/Course';
import { CourseService } from '@app/_services/course.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  courses: Course[];
  isLoading: boolean = false;

  constructor(public courseService: CourseService, private router: Router,
              private othersService: SubscriptionsService,
              private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    var user = this.authenticationService.getUsername();
    if(user!=null)
    {
      this.getCourses();
    }
    else
      this.getAllCourses();
  }

  getCourses(){
    this.isLoading = true;
    this.courseService.getCourses(+this.authenticationService.getUsername().userId)
    .subscribe((course: Course[]) => {
      this.isLoading = false;
      this.courses = course;
      console.log(this.courses);
    });
    console.log(this.courses);
  }

  getAllCourses(){
    this.isLoading = true;
    this.courseService.getAllCourses()
    .subscribe((course: Course[]) => {
      this.isLoading = false;
      this.courses = course;
      console.log(this.courses);
    });
    console.log(this.courses);
  }

  Subscribe(courseName: string, courseCode: number){
    this.othersService.selectedCourse = courseName;
    this.othersService.courseCode = courseCode;
    this.router.navigate(['/subscription']);
  }

}
