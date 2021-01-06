import { Component, OnInit } from '@angular/core';
import { Question } from '@app/_models/question';
import { CourseService } from '@app/_services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '@app/_services';
import { TestQuestion } from '@app/_models/TestQuestion';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {

  // question: Question[];
  testQuestion: TestQuestion[];
  // selectedAnswer: number[] = new Array(10);
  examStarted : boolean = false;
  leftTimes : number = 30;
  timeUp : boolean = false;
  currentIndex : number = 0;
  isActive: boolean = false;
  isPracticeExam: boolean = false;
  next: boolean = false;

  constructor(private courseService: CourseService,
              private authenticationService: AuthenticationService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    //this.visited.fill(false);
    console.log(this.authenticationService.getUsername().studentId);
    this.getQuestions();
    this.examStarted=true;
  }

  selected(option: string, index: number){
    this.courseService.selectedAnswer[index]=option;
    console.log(this.courseService.selectedAnswer[index], index);
    // this.status = option;
    // this.questionNo = index;
    // this.visited[index] = true;
    // for(var i =0;i<10;i++)
    // console.log(this.visited[i]);
  }
  submit(){
    this.timeUp=true;
    this.router.navigate(['/test/questions/answer/details']);
    console.log(this.courseService.selectedAnswer);
    // console.log(this.courseService.selectedAnswer[7]);
  }

  // onStartExam(){
  //   this.getQuestions();
  //   this.examStarted=true;
  // }

  getQuestions(){
    // +this.route.snapshot.params['chapterId']
      this.courseService.getQuestions(this.courseService.chapterIds,
        +this.authenticationService.getUsername().studentId,
        this.courseService.examType, this.courseService.CorrectOrWrong,
        this.courseService.SeenOrUnseen, +this.courseService.howManyQuestions)
        .subscribe((question: TestQuestion[]) => {
        // this.questions = question;
        this.courseService.testQuestions = question;
        this.testQuestion = question;
        console.log(this.testQuestion);
        var keys = Object.keys(this.testQuestion);
        var length = keys.length;
        this.courseService.selectedAnswer = new Array(length);
        this.courseService.selectedAnswer.fill('');
        console.log(length);

        // this.questionIds = new Array(length);
  
        // for(var i = 0;i < length; i++)
        // this.questionIds[i] = this.question[i].questionId;
        // console.log(this.questionIds);
      });
    //console.log(this.questions);
  }

  onTimerFinished(e:Event){
    if (e["action"] == "done"){
       //your code here
        this.timeUp = true;
        this.router.navigate(['/test/questions/answer/details']);
       console.log("Time Up");
     }
   }
  
}
