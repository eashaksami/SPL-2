import { Router } from '@angular/router';
import { AuthenticationService } from '@app/_services';
import { SubscriptionsService } from './../../_services/subscriptions.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.less']
})
export class SubscriptionComponent implements OnInit {

  constructor(private othersService: SubscriptionsService,
              private authenticationService: AuthenticationService,
              private router: Router) { }
    
  courseSelected: string;
  isLoading: boolean = false;

  ngOnInit(): void {
    this.courseSelected = this.othersService.selectedCourse;
  }

  Subscribed(month: number, price: number){
    this.isLoading = true;
    console.log(this.othersService.courseCode);
    this.othersService.subscribeToCourse(price,
                                         +this.authenticationService.getUsername().userId,
                                          this.othersService.courseCode, month)
                                          .subscribe(data => {
                                            this.isLoading = false;
                                            console.log(data);
                                            alert("subscription added successfully!!!");
                                            this.router.navigate(['/subscriptions']);
                                          });
  }

}
