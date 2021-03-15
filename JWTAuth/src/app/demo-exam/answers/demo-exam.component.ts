import { Question } from './../_models/question';
import { Component, OnInit } from '@angular/core';
import { Course } from '@app/_models/Course';
import { CourseService } from '@app/_services/course.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/_services';
import { Images } from '@app/_models/images';

@Component({
  selector: 'app-demo-exam',
  templateUrl: './demo-exam.component.html',
  styleUrls: ['./demo-exam.component.less']
})
export class DemoExamComponent implements OnInit {

  courses: Course[];
  questions: Question[];
  images: Images[];
  questionGet: boolean = false;
  selectedCourseCode: number;
  isLoading: boolean = false;

  constructor(private courseService: CourseService,
              private router: Router) { }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(){
    this.isLoading = true;
    this.courseService.getCoursesDemo()
    .subscribe((course: Course[]) => {
      this.isLoading = false;
      this.courses = course;
      console.log(this.courses);
    });
  }

  TakeDemo(courseCode: number){
    this.selectedCourseCode = courseCode;
    console.log(this.selectedCourseCode);
  }

}
