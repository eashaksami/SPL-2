import { AdminService } from '@app/_services/admin.service';
import { Question } from '@app/_models/question';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-view-questions',
  templateUrl: './view-questions.component.html',
  styleUrls: ['./view-questions.component.less']
})
export class ViewQuestionsComponent implements OnInit, OnChanges {

  isLoading: boolean = false;
  getQuestions: boolean = false;
  length: number = 0;
  questions: Question[];
  onClickChange: boolean = false;
  index: number = null;
  question: string;
  option1: string;
  option2: string;
  option3: string;
  option4: string;
  correctAnswer: string;
  answerDetails: string;
  @Input() chapterId: number;
  @Input() selectedCourse: string;
  @Input() isSelected: boolean;

  constructor(public adminService: AdminService) { }
  ngOnChanges(changes: SimpleChanges): void {
    this.viewQuestions(changes.chapterId.currentValue);
  }

  ngOnInit(): void {
    if(this.adminService.onSelectViewQuestion){
      this.viewQuestions(this.chapterId);
    }
    // this.viewQuestions();
  }

  viewQuestions(chapterId: number){
    this.isLoading = true;
    this.chapterId = chapterId;
    this.adminService.getQuestions(this.chapterId)
    .subscribe((questions: Question[]) =>{
      this.getQuestions = true;
      this.isLoading = false;
      this.questions = questions;
      this.length = this.questions.length;
      console.log(this.questions);
      console.log("From View Question:"+ this.chapterId);
    });
  }

  changeInfo(i: number){
    this.onClickChange = true;
    this.index = i;
  }

  editResult(question: string, option1: string, option2: string, option3: string, option4: string, correctAnswer: string, answerDetails: string, questionId: number, chapterId: number){
    if(this.question == null) this.question = question;
    if(this.option1 == null) this.option1 = option1;
    if(this.option2 == null) this.option2 = option2;
    if(this.option3 == null) this.option3 = option3;
    if(this.option4 == null) this.option4 = option4;
    if(this.correctAnswer == null) this.correctAnswer = correctAnswer;
    if(this.answerDetails == null) this.answerDetails = answerDetails;

    this.adminService.updateResult(this.question, this.option1, this.option2, this.option3, this.option4, this.correctAnswer, this.answerDetails, +questionId, +chapterId)
    .subscribe(() =>{
      this.viewQuestions(this.chapterId);
      alert("Changes Saved Successfully!!!!");
      this.onClickChange = false;
      this.index = null;
    });
  }

  uploadQuestion(){
    this.adminService.onSelectUploadQuestion = true;
    this.adminService.onSelectViewQuestion = false;
  }

  deleteQuestion(questionId: number){
    this.adminService.deleteQuestion(+questionId)
    .subscribe((data) => {
      console.log(data);
      this.viewQuestions(this.chapterId);
      alert("Record Deleted Successfully");
    });
  }
}
