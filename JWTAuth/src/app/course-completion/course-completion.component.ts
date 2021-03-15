import { CourseCompletion } from './../_models/courseCompletion';
import { Component, OnInit } from '@angular/core';
import { PerformanceService } from '@app/_services/performance.service';
import { AuthenticationService } from '@app/_services';
import { ActivatedRoute } from '@angular/router';
import { ChartOptions, ChartType } from 'chart.js';
// import { JwtHelperService } from "@auth0/angular-jwt";

@Component({
  selector: 'app-course-completion',
  templateUrl: './course-completion.component.html',
  styleUrls: ['./course-completion.component.less']
})
export class CourseCompletionComponent implements OnInit {

  courseCompletionData: CourseCompletion[] = [];
  totalQuestion: number[] = [];
  totalSolved: number[] = [];
  chapterNames: string[] = [];
  length: number = 0;
  countTotalQsn: number = 0;
  countTotalSeen: number = 0;
  chartData: number[] = [];
  isLoading: boolean = false;

  constructor(private performanceService: PerformanceService,
              private authenticationService: AuthenticationService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.getProgress();

    // const helper = new JwtHelperService();
 
    // const decodedToken = helper.decodeToken(this.authenticationService.getUsername().token);
    // const expirationDate = helper.getTokenExpirationDate(this.authenticationService.getUsername().token);
    // const isExpired = helper.isTokenExpired(this.authenticationService.getUsername().token);

    // console.log("Decoded Token: " + decodedToken);
    // console.log("Expire Date" + expirationDate);
    // console.log("is Expired" + isExpired);
  }

  getProgress(){
    this.isLoading = true;
    this.performanceService.getCourseCompletionData(+this.authenticationService.getUsername().userId,
                                            +this.route.snapshot.params['courseCode'])
    .subscribe((data: CourseCompletion[]) => {
      this.isLoading = false;
      console.log(data);
      this.courseCompletionData = data;
      console.log(this.courseCompletionData);
      this.updateData();
      
    });
    // console.log("sami");
    // console.log(this.graphData);
  }

  updateData(){
    console.log(this.courseCompletionData);
    var keys = Object.keys(this.courseCompletionData);
      length = keys.length;
      // this.graphData = new Array(length);
    for(var i = 0; i < length; i++){
      this.chapterNames[i] = this.courseCompletionData[i].name;
      this.totalQuestion[i] = this.courseCompletionData[i].totalQuestion;
      this.totalSolved[i] = this.courseCompletionData[i].totalSeen; 

      this.countTotalQsn += this.totalQuestion[i];
      this.countTotalSeen += this.totalSolved[i];
    }
    console.log(this.totalQuestion);
    console.log(this.totalSolved);
    console.log(this.chapterNames);

    this.chartData[0] = this.countTotalQsn;
    this.chartData[1] = this.countTotalSeen;
  }
  

  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  
  public barChartLabels = this.chapterNames;
  public barChartType = 'bar';
  public barChartLegend = true;

  public barChartData = [
    {data: this.totalQuestion, label: 'Total Question'},
    {data: this.totalSolved, label: 'Total Solved'}
  ];

  public pieChartOptions: ChartOptions = {
    responsive: true,
  };
  
  public pieChartLabels = ['Solved', 'Total Question'];
  public pieChartData = [this.chartData];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public pieChartColors: Array < any > = [{
    backgroundColor: ['red', 'yellow']
 }];

}


// use user define color:
// public pieChartColors: Array < any > = [{
//   backgroundColor: ['red', 'yellow', 'rgba(148,159,177,0.2)'],
//   borderColor: ['rgba(135,206,250,1)', 'rgba(106,90,205,1)', 'rgba(148,159,177,1)']
// }];

// <canvas baseChart
//           [data]="pieChartData"
//           [labels]="pieChartLabels"
//           [chartType]="pieChartType"
//           [colors]="pieChartColors"></canvas>
