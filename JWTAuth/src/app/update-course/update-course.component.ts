import { AdminService } from '@app/_services/admin.service';
import { Question } from '@app/_models/question';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Chapter } from '@app/_models/Chapter';
import { CourseService } from '@app/_services/course.service';

@Component({
  selector: 'app-update-course',
  templateUrl: './update-course.component.html',
  styleUrls: ['./update-course.component.less']
})
export class UpdateCourseComponent implements OnInit {

  chapters: Chapter[];
  questions: Question[];
  // isValid: boolean = false;
  isLoading: boolean = false;
  // isLoadingQuestion: boolean = false;
  // getQuestions: boolean = false;
  onClickChange: boolean = false;
  index: number = null;
  chapterId: number = 0;
  selectedCourse: string = '';
  isSelected: boolean = false;

  constructor(public courseService: CourseService,
              private route: ActivatedRoute,
              private adminService: AdminService,
              private router: Router) { }

  courseCode: number = +this.route.snapshot.params['courseCode'];

  ngOnInit(): void {
    this.isLoading = false;
    this.getChapters();
  }

  getChapters(){
    this.isLoading = true;
    this.courseService.grtChapters(+this.route.snapshot.params['courseCode'])
    .subscribe((chapter: Chapter[]) =>{
      this.isLoading = false;
      this.chapters = chapter;
      console.log(this.chapters);
    });
    
  }

  selectedChapter(isChecked: boolean, chapterId: number, chapterName: string){
    console.log(isChecked);
    if(isChecked){
      // this.adminService.selsctedChapter = chapterId;
      this.chapterId = chapterId;
      this.selectedCourse = chapterName;
      this.isSelected = true;
    }
    else{
      this.chapterId = 0;
    }
    // console.log("From Update Course: "+this.adminService.selsctedChapter);
  }

  // viewQuestions(chapterId: number){
  //   this.isLoadingQuestion = true;
  //   this.adminService.getQuestions(chapterId)
  //   .subscribe((questions: Question[]) =>{
  //     this.getQuestions = true;
  //     this.isLoadingQuestion = false;
  //     this.questions = questions;
  //     console.log(this.questions);
  //   });
  // }

  changeInfo(i: number){
    this.onClickChange = true;
    this.index = i;
  }

}
