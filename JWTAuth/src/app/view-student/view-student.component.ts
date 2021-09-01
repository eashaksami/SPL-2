import { Component, OnInit } from '@angular/core';
import { User } from '@app/_models';
import { AdminService } from '@app/_services/admin.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.less']
})
export class ViewStudentComponent implements OnInit {

  students: User[];
  isLoading: boolean = false;

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getAllStudents();
  }

  getAllStudents(){
    this.isLoading = true;
    this.adminService.getAllStudents()
    .subscribe((student: User[]) => {
      this.isLoading = false;
      console.log(student);
      this.students = student;
    });
  }

}
