import { Component, OnInit } from '@angular/core';
import { Chapter } from '@app/_models/Chapter';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '@app/_services/course.service';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrls: ['./chapter.component.less']
})
export class ChapterComponent implements OnInit {

  chapters: Chapter[];
  examStarted: boolean =false;
  values: number[] = [25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100];
  examType: string[] = ['Practice exam', 'Test exam'];
  options: string[] = ['Yes', 'no'];
  isDisabled: boolean = true;
  isValid: boolean = false;
  isLoading: boolean = false;

  constructor(private courseService: CourseService,
              private route: ActivatedRoute,
              private router: Router) { }

  courseCode: number = +this.route.snapshot.params['id'];

  ngOnInit(): void {
    this.isLoading = false;
    this.courseService.chapterIds = new Array();
    this.getChapters();
    console.log(this.courseService.examStarted);
  }

  getChapters(){
    this.isLoading = true;
    this.courseService.grtChapters(+this.route.snapshot.params['id'])
    .subscribe((chapter: Chapter[]) =>{
      this.isLoading = false;
      this.chapters = chapter;
      console.log(this.chapters);
    });
    
  }

  ExamType(examType: string)
  {
    if(examType === 'Practice exam') this.courseService.examType = 'practiceExam';
    else this.courseService.examType = 'testExam';
    // console.log(this.courseService.examType);
    
    // if(examType==='practiceExam')
    //   this.isPracticeExam = true;
    
    // else
    //   this.isPracticeExam = false;
    // console.log(this.isPracticeExam, this.examType);
  }

  onClickSeenOrUnseen(type: string)
  {
    if(type === 'Yes')
    {
      this.courseService.SeenOrUnseen = 1;
      this.isDisabled = false;
    }
    else
    {
      this.courseService.SeenOrUnseen = 0;
      this.isDisabled = true;
    } 
    console.log(this.courseService.SeenOrUnseen);
    this.calculateCorrectOrWrong(this.courseService.isCorrect,this.courseService.isWrong);
  }

  onClickIsCorrect(input:string)
  {
    if(input === 'Yes') this.courseService.isCorrect = 'yes';
    else this.courseService.isCorrect = 'no';
    // this.courseService.isCorrect = input;
    console.log(this.courseService.isCorrect);
    this.calculateCorrectOrWrong(this.courseService.isCorrect,this.courseService.isWrong);
  }

  onClickIsWrong(input: string)
  {
    if(input === 'Yes')this.courseService.isWrong = 'yes';
    else this.courseService.isWrong = 'no';
    // this.courseService.isWrong = input;
    console.log(this.courseService.isWrong);
    this.calculateCorrectOrWrong(this.courseService.isCorrect,this.courseService.isWrong);
  }

  calculateCorrectOrWrong(isCorrect: string, isWrong: string )
  {
    if(isCorrect==='yes' && isWrong==='yes')
      this.courseService.CorrectOrWrong = 2;
    if(isCorrect==='no' && isWrong==='no')
      this.courseService.CorrectOrWrong = 3;
    if(isCorrect==='yes' && isWrong==='no')
      this.courseService.CorrectOrWrong = 1;
    if(isCorrect==='no' && isWrong==='yes')
      this.courseService.CorrectOrWrong = 0; 
      
    console.log(this.courseService.CorrectOrWrong);   
  }

  selectedChapter(isChecked: boolean, chapterId: number){
    console.log(isChecked);
    if(isChecked){
      this.courseService.chapterIds.push(chapterId);
    }
    else{
      var index: number = this.courseService.chapterIds.indexOf(chapterId, 0);
      this.courseService.chapterIds.splice(index, 1);
    }
    console.log(this.courseService.chapterIds.length);
    this.isValid = this.courseService.chapterIds.length > 0;
  }

  onStartExam(){
    this.examStarted = true;
  }

  noOfQuestion(questions: number){
    this.courseService.howManyQuestions = questions;
    console.log(this.courseService.howManyQuestions);
  }

  onViewProgressGraph(){
    
  }

}
