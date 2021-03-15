import { Component, OnInit } from '@angular/core';
import { Question } from '@app/_models/question';
import { CourseService } from '@app/_services/course.service';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.less']
})
export class AnswersComponent implements OnInit {

  questions: Question[];
  questionIds: number[] = [];
  // isCorrects: number[] = [];
  selectedAnswers: any;
  isTrue: any;
  selected: any;
  isCorrect: any;
  // length: number = 0;

  constructor(private courseService: CourseService,
    private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.questions = this.courseService.questions;

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
    this.selectedAnswers[i] = this.courseService.selectedAnswer[i];
    //console.log(this.questions);
    // console.log(this.selectedAnswers);
    this.something();
  }

  fillArray(i: number)
  {
    return new Array(i);
  }

  something(){
    for(var i=0;i<length;i++)
    {
      if(this.courseService.questions[i].correctAnswer === this.selectedAnswers[i])
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
  }

}
