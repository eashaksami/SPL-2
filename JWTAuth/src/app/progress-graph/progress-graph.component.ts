import { AuthenticationService } from './../_services/authentication.service';
import { Progress } from './../_models/progress';
import { PerformanceService } from './../_services/performance.service';
import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-progress-graph',
  templateUrl: './progress-graph.component.html',
  styleUrls: ['./progress-graph.component.less']
})
export class ProgressGraphComponent implements OnInit {
  progressData: Progress[] = [];
  graphData: number[] = [];
  public lineChartLabels = [];
  length: number = 0;
  isLoading: boolean = false;

  constructor(private performanceService: PerformanceService,
              private authenticationService: AuthenticationService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.getProgress();
  }

  getProgress(){
    this.isLoading = true;
    this.performanceService.getProgressData(+this.authenticationService.getUsername().userId,
                                            +this.route.snapshot.params['courseCode'])
    .subscribe((data: Progress[]) => {
      this.isLoading = false;
      console.log(data);
      this.progressData = data;
      console.log(this.progressData);

      var keys = Object.keys(this.progressData);
      length = keys.length;
      // this.graphData = new Array(length);
      for(var i = 0; i < length; i++){
      this.graphData[i] = ((this.progressData[i].totalCorrectAnswer)*100)/(this.progressData[i].quantity);
      this.lineChartLabels[i] = "Exam " + (i+1).toString();
    }
    console.log(this.graphData);
    });
    // console.log("sami");
    // console.log(this.graphData);
  }
  //line chart
  // public datas = [1, 3, 5];
  // public lineChartData: ChartDataSets[] = [
  //   { data: this.datas, label: 'Series A' },
  // ];

  // [65, 59, 80, 81, 56, 55, 40]
  public lineChartData: ChartDataSets[] = [
    { data: this.graphData, label: 'Percantage' },
  ];
  // public lineChartLabels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public lineChartOptions = {
    responsive: true,
  };
  public lineChartColors = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(255,0,0,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartPlugins = [];

}
