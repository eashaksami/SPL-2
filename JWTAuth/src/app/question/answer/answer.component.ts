import { Answers } from './../../_models/answers';
import { Question } from './../../_models/question';
import { CourseService } from '@app/_services/course.service';
import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { AuthenticationService } from '@app/_services';
import { TestQuestion } from '@app/_models/TestQuestion';

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
      // if(this.courseService.testQuestions[i].correctAnswer === this.selectedAnswers[i])
      // {
      //   this.isCorrect[i] = true;
      //   this.courseService.correctOrWrong[i] = 1;
      // }
      // else
      // {
      //   this.isCorrect[i] = false;
      //   this.courseService.correctOrWrong[i] = 0;
      // }

      if(this.answers[i].correctAnswer === this.selectedAnswers[i])
      {
        this.isCorrect[i] = true;
        this.courseService.correctOrWrong[i] = 1;
      }
      else
      {
        this.isCorrect[i] = false;
        this.courseService.correctOrWrong[i] = 0;
      }

      if(this.selectedAnswers[i]!=='')
      {
        this.selected[i] = true;
      }
      else
        this.selected[i] = false;
    }
    // console.log(this.courseService.correctOrWrong);
    this.updateDatabase();
  }

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
     /*this.dtatbaseUpdate
        .push({questionId: this.questionIds,isCorrect:this.isCorrects, examType: this.courseService.examType,
              studentId: +this.authenticationService.getUsername().studentId});      
     console.log(this.dtatbaseUpdate);
     var jsonData = JSON.stringify(this.dtatbaseUpdate);
    //  console.log(jsonData);
     var data = jsonData.toString(); 
     data = data.slice(1);
     data = data.slice(0,data.length-1);
    //  console.log(data);
     console.log(this.courseService.examType);*/

      // this.courseService.updateDatabase(this.dtatbaseUpdate).subscribe(response =>{
      //   console.log(response);
      // });

      this.courseService.updateDatabase(this.questionIds,this.isCorrects,this.courseService.examType,
        +this.authenticationService.getUsername().studentId).subscribe(response =>{
        console.log(response);
      });
  }
}
 