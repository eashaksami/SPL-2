import { Answers } from './../../_models/answers';
import { Question } from './../../_models/question';
import { CourseService } from '@app/_services/course.service';
import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { AuthenticationService } from '@app/_services';
import { TestQuestion } from '@app/_models/TestQuestion';
import { ChartOptions, ChartType } from 'chart.js';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css']
})
export class AnswerComponent implements OnInit {

  questions: TestQuestion[];
  answers: Answers[];
  // dtatbaseUpdate:any = [];
  questionIds: number[] = [];
  isCorrects: number[] = [];
  // selectedAnswers: string[] = new Array(10);
  // isTrue: boolean[] = new Array(10);
  // selected: boolean[] = new Array(10);
  // isCorrect: boolean[] = new Array(10);
  selectedAnswers: any;
  isTrue: any;
  selected: any;
  isCorrect: any;
  length: number = 0;
  totalCorrect: number = 0;
  totalWrong: number = 0;
  noAttempt: number = 0;

  constructor(private courseService: CourseService,
              private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.questions = this.courseService.testQuestions;

    var keys = Object.keys(this.questions);
    length = keys.length;

    this.selectedAnswers = this.fillArray(length);
    this.isTrue = this.fillArray(length);
    this.selected = this.fillArray(length);
    this.isCorrect = this.fillArray(length);
    this.courseService.correctOrWrong = this.fillArray(length);

    this.isTrue.fill(false);
    this.selected.fill(false);
    this.isCorrect.fill(false);
    this.courseService.correctOrWrong.fill(0);

    for(var i=0;i<length;i++)
    {
      this.selectedAnswers[i] = this.courseService.selectedAnswer[i];
      this.questionIds[i] = this.questions[i].questionId;
    }

    this.courseService.getAnswers(this.questionIds).subscribe((answer: Answers[]) => {
      this.answers = answer;
      console.log(this.answers);
      this.something();
    });
    //console.log(this.questions);
    // console.log(this.selectedAnswers);
    // this.something();
  }

  fillArray(i: number)
  {
    return new Array(i);
  }

  something(){
    for(var i=0;i<length;i++)
    {
      console.log(this.answers[i].correctAnswer +" "+ this.selectedAnswers[i])
      if(this.answers[i].correctAnswer === this.selectedAnswers[i])
      {
        this.isCorrect[i] = true;
        console.log("isCorrect[i] = true "+this.isCorrect[i]);
        this.courseService.correctOrWrong[i] = 1;
      }
      else
      {
        this.isCorrect[i] = false;
        console.log("isCorrect[i] = false "+this.isCorrect[i]);
        this.courseService.correctOrWrong[i] = 0;
      }

      if(this.selectedAnswers[i]!=='')
      {
        this.selected[i] = true;
        console.log("selected[i] = true "+this.selected[i]);
      }
      else
      {
        this.selected[i] = false;
        console.log("selected[i] = false "+this.selected[i]);
      }
    }

    for(var i=0;i<length;i++)
    {
      if(this.selected[i])
      {
        if(this.isCorrect[i])
        {
          this.totalCorrect++;
          // console.log("Total Correct "+this.totalCorrect);
        }
        else
        {
          this.totalWrong++;
          // console.log("Total wrong "+this.totalWrong);
        }
      }
      else
      {
        this.noAttempt++;
        // console.log("No Attempt "+this.noAttempt);
      }
    }
    this.UpdateChartData(this.totalCorrect, this.totalWrong, this.noAttempt);
    console.log(this.isCorrect);
    console.log(this.selected);
    console.log(this.totalCorrect);
    console.log(this.totalWrong);
    console.log(this.noAttempt);
    // console.log(this.courseService.correctOrWrong);
    this.updateDatabase();
  }

  chartData: number[] = [];

  UpdateChartData(totalCorrect: number, totalWrong: number, noAttempt: number){
    this.chartData[0] = totalCorrect;
    this.chartData[1] = totalWrong;
    this.chartData[2] = noAttempt;
  }
  
  
  public pieChartOptions: ChartOptions = {
    responsive: true,
  };
  
  public pieChartLabels = ['Correct', 'Wrong', 'No Attempt'];
  public pieChartData = [this.chartData];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public pieChartColors: Array < any > = [{
    backgroundColor: ['green', 'red', 'yellow']
 }];

  updateDatabase()
  {
    console.log('database update');
     for(var i=0;i<length;i++)
     {
        // this.questionIds[i] = this.questions[i].questionId;
        this.isCorrects[i] = this.courseService.correctOrWrong[i];
     } 
     console.log(this.courseService.correctOrWrong);
     console.log(this.isCorrects);

      this.courseService.updateDatabase(this.questionIds,this.isCorrects,this.courseService.examType,
        +this.authenticationService.getUsername().userId).subscribe(response =>{
        console.log(response);
      });
  }
}
 
