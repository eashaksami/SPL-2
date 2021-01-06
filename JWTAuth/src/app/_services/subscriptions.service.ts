import { SubscribedCourses } from './../_models/subscribedCourses';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class SubscriptionsService{
    baseUrl = 'http://localhost:4000/';
    constructor(private http: HttpClient){}
    selectedCourse: string;
    courseCode: number;
    // course: Course;

    subscribeToCourse(price: number, studentId: number, courseCode: number, length: number){
        console.log(this.http.post<any>(this.baseUrl + 'subscription', { price, studentId, courseCode, length }));
        return this.http.post<any>(this.baseUrl + 'subscription', { price, studentId, courseCode, length });
    }

    getMySubscriptions(studentId: number): Observable<SubscribedCourses[]>{

        const params = new HttpParams().set('studentId', studentId.toString());

        return this.http.get(this.baseUrl+'subscription/myCourses', {params})
            .pipe(map((response => <SubscribedCourses[]>response)));
    }

    postFile(fileToUpload: File) {
        const formData: FormData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        return this.http.post(this.baseUrl + 'subscription/upload', formData);
    }
}