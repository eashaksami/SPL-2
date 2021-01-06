import { Component, OnInit } from '@angular/core';
import { Question } from '@app/_models/question';
import { TestQuestion } from '@app/_models/TestQuestion';
import { CourseService } from '@app/_services/course.service';
import { AuthenticationService } from '@app/_services';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-practice-exam',
  templateUrl: './practice-exam.component.html',
  styleUrls: ['./practice-exam.component.less']
})
export class PracticeExamComponent implements OnInit {

  question: Question[];
  currentIndex : number = 0;
  isPracticeExam: boolean = true;
  viewAnswer: boolean = false;

  questionIds: number[] = [];
  isCorrects: number[] = [];
  selectedAnswers: any;
  length: number = 0;
  // dtatbaseUpdate:any = [];

  constructor(private courseService: CourseService,
              private authenticationService: AuthenticationService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    console.log(this.authenticationService.getUsername().studentId);
    this.getQuestions();

    this.selectedAnswers = this.fillArray(length);
    for(var i=0;i<length;i++)
    this.selectedAnswers[i] = this.courseService.selectedAnswer[i];

    this.courseService.correctOrWrong = this.fillArray(length);
    this.courseService.correctOrWrong.fill(0);
  }

  fillArray(i: number)
  {
    return new Array(i);
  }

  selected(option: string, index: number){
    this.courseService.selectedAnswer[index]=option;
    console.log(this.courseService.selectedAnswer[index], index);
  }
  
  getQuestions(){
      this.courseService.getQuestions(this.courseService.chapterIds,
      +this.authenticationService.getUsername().studentId,
      this.courseService.examType, this.courseService.CorrectOrWrong,
      this.courseService.SeenOrUnseen, +this.courseService.howManyQuestions)
        .subscribe((question: Question[]) => {
        // this.questions = question;
        this.courseService.questions = question;
        this.question = question;
        console.log(this.question);
        var keys = Object.keys(this.question);
        this.length = keys.length;
        this.courseService.selectedAnswer = new Array(length);
        this.courseService.selectedAnswer.fill('');
        console.log(length);
  
        // this.questionIds = new Array(length);
  
        // for(var i = 0;i < length; i++)
        // this.questionIds[i] = this.question[i].questionId;
        // console.log(this.questionIds);
      });
  }

  onClickViewAnswer()
  {
    this.viewAnswer = true;
  }

  onClickNext()
  {
    this.currentIndex++;
    this.viewAnswer = false;
    console.log(this.currentIndex);
    console.log(this.length);
    if(this.currentIndex == this.length)
    {
      console.log('database update');
      this.something();
    }
  }

  something(){
      for(var i=0;i<this.length;i++)
      {
        if(this.courseService.questions[i].correctAnswer === this.courseService.selectedAnswer[i])
        {
          this.courseService.correctOrWrong[i] = 1;
          console.log(this.courseService.correctOrWrong[i]);
        }
        else
        {
          this.courseService.correctOrWrong[i] = 0;
          console.log(this.courseService.correctOrWrong[i]);
        }
    }
    this.updateDatabase();
  }

  updateDatabase()
  {
    console.log('database update');
    // console.log(this.courseService.correctOrWrong);
     for(var i=0;i<this.length;i++)
     {
        this.questionIds[i] = this.question[i].questionId;
        this.isCorrects[i] = this.courseService.correctOrWrong[i];
     } 

      this.courseService.updateDatabase(this.questionIds,this.isCorrects,this.courseService.examType,
        +this.authenticationService.getUsername().studentId).subscribe(response =>{
        console.log(response);
      });
  }
}
