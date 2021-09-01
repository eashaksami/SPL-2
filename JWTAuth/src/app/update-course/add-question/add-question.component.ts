import { AdminService } from './../../_services/admin.service';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.less']
})
export class AddQuestionComponent implements OnInit, OnChanges {

  @Input() chapterId: number;

  question: string;
  option1: string;
  option2: string;
  option3: string;
  option4: string;
  correctAnswer: string;
  answerDetails: string;
  saveChange: boolean = false;

  constructor(public adminService: AdminService) { }
  ngOnChanges(changes: SimpleChanges): void {
    console.log("Sami"+this.chapterId + this.adminService.onSelectUploadQuestion);
  }

  ngOnInit(): void {
  }

  saveChanges() {
    this.adminService
    .uploadQuestion(this.question, this.option1, this.option2, this.option3, this.option4, this.correctAnswer, this.answerDetails, +this.chapterId)
      .subscribe(() =>{
        alert("Record Added Successfully!!!!");
        this.question = null;
        this.option1 = null;
        this.option2 = null;
        this.option3 = null;
        this.option4 = null;
        this.correctAnswer = null;
        this.answerDetails = null;
        this.saveChange = true;
      }, error => {
        console.log('error');
      });
  }

  viewQuestion(){
    this.adminService.onSelectViewQuestion = true;
    this.adminService.onSelectUploadQuestion = false;
  }

}
