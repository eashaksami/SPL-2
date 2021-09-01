import { CourseService } from './../_services/course.service';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { AuthenticationService } from './../_services/authentication.service';
import { SubscriptionsService } from './../_services/subscriptions.service';
import { SubscribedCourses } from './../_models/subscribedCourses';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-subscriptions',
  templateUrl: './my-subscriptions.component.html',
  styleUrls: ['./my-subscriptions.component.less'],
  providers:[DatePipe]
})
export class MySubscriptionsComponent implements OnInit {

  mySubscriptions: SubscribedCourses[];
  isLoading: boolean = false;
  length: number = 0;

  constructor(private subscriptionsService: SubscriptionsService,
              private authenticationService: AuthenticationService,
              public courseService: CourseService,
              private router: Router) { }

  ngOnInit(): void {
    this.getMySubscriptions();
    this.formatDate();
  }

  getMySubscriptions(){
    this.isLoading = true;
    this.subscriptionsService.getMySubscriptions(+this.authenticationService.getUsername().userId)
    .subscribe((course: SubscribedCourses[]) => {
      this.isLoading = false;
      this.mySubscriptions = course;
      this.length = this.mySubscriptions.length;
      console.log(this.length)
      console.log(this.mySubscriptions);
    });
  }

  formatDate(){
    // for(var i=0;i<this.mySubscriptions.length;i++){
    //   this.mySubscriptions[i].length = this.datePipe.transform(this.mySubscriptions[i].length,"yyyy-MM-dd");
    // }
  }

  Enroll(){
    // this.router.navigate(['/test/courseCode']);
  }
}
