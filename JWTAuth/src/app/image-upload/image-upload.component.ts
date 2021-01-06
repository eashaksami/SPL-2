import { SubscriptionsService } from './../_services/subscriptions.service';
import { CourseService } from '@app/_services/course.service';
import { Images } from './../_models/images';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.less']
})
export class ImageUploadComponent implements OnInit {

  images: Images[];
  image: File = null;
  imageUrl: string = "/assets/Images/du.png";

  constructor(private courseService: CourseService,
              private subscriptionService: SubscriptionsService) { }

  ngOnInit(): void {
    this.getImages();
  }

  getImages(){
    this.courseService.getImages().subscribe((image: Images[]) =>{
      this.images = image;
      console.log(this.images);
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

  OnSubmit(Caption,Image){
    this.subscriptionService.postFile(this.image).subscribe(
      data =>{
        console.log('done');
        // Caption.value = null;
        Image = null;
        this.imageUrl = "/assets/Images/du.png";
      }
    );
   }

}
