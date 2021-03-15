import { Component, OnInit, Input } from '@angular/core';
import { CourseService } from '@app/_services/course.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Course } from '@app/_models/Course';
import { Question } from '@app/_models/question';

@Component({
  selector: 'app-demo-question',
  templateUrl: './demo-question.component.html',
  styleUrls: ['./demo-question.component.less']
})
export class DemoQuestionComponent implements OnInit {

  courseCode: number;
  courses: Course[];
  questions: Question[];
  isLoading: boolean = false;
  
  constructor(private courseService: CourseService,
    private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.TakeDemo();
  }

  TakeDemo(){
    this.isLoading = true;
    this.courseService.getDemoQuestions(+this.route.snapshot.params['courseCode']).subscribe((question: Question[]) => {
      this.isLoading = false;
      this.questions = question;
      console.log(this.questions);
      this.courseService.questions = question;
    });
    this.courseService.selectedAnswer = new Array(10);
  }

  selected(option: string, index: number){
    this.courseService.selectedAnswer[index]=option;
    console.log(this.courseService.selectedAnswer[index], index);
  }

  submit(){
    this.router.navigate(['/demo/answer']);
    console.log(this.courseService.selectedAnswer);
    // console.log(this.courseService.selectedAnswer[7]);
  }

}
