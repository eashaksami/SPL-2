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

  constructor(private subscriptionsService: SubscriptionsService,
              private authenticationService: AuthenticationService,
              private router: Router) { }

  ngOnInit(): void {
    this.getMySubscriptions();
    this.formatDate();
  }

  getMySubscriptions(){
    this.subscriptionsService.getMySubscriptions(+this.authenticationService.getUsername().studentId)
    .subscribe((course: SubscribedCourses[]) => {
      this.mySubscriptions = course;
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
