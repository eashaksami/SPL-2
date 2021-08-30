import { Course } from './../_models/Course';
import { AdminService } from '@app/_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { Images } from '@app/_models/images';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.less']
})
export class AddCourseComponent implements OnInit {

  courseName: string;
  course: Course;
  receivedImage: Images;
  image: File = null;
  imageUrl: string = "/assets/Images/du.png";

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
  }

  saveChanges(imageId: number){
    this.adminService.addNewCourse(this.courseName, +imageId)
    .subscribe((course: Course) =>{
      alert("Course Added Successfully!!!");
      this.course = course;
      this.courseName = null;
    });
  }

  handleFileInput(file: FileList){
    this.image = file.item(0);

    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.image);
  }

  OnSubmit(Image){
    this.adminService.postFile(this.image).subscribe(
      data =>{
        console.log('done');
        this.receivedImage = data;
        this.saveChanges(this.receivedImage.imageId);
        // Caption.value = null;
        Image = null;
        this.imageUrl = "/assets/Images/du.png";
      }
    );
   }

}
