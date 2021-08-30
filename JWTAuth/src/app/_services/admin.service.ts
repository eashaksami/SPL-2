import { Images } from './../_models/images';
import { Question } from './../_models/question';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '@app/_models/user';
import { Course } from '@app/_models/Course';


@Injectable()
export class AdminService{
    baseUrl = 'http://localhost:4000/';
    user: User;
    onSelectUploadQuestion: boolean = false;
    onSelectViewQuestion: boolean = true;
    selectedChapterId: number = null;
    
    constructor(private http: HttpClient) {}

    getAllStudents(): Observable<User[]>{
        return this.http.get(this.baseUrl+ 'admin').pipe(map((response => <User[]>response)));
    }

    getQuestions(id: number): Observable<Question[]>{
        // console.log(this.http.get(this.baseUrl+'test/chapters'+ id).pipe(map((response => <Question[]>response))));
        return this.http.get(this.baseUrl+'admin/question/view/'+ id).pipe(map((response => <Question[]>response)));
    }

    updateResult(question: string, option1: string, option2: string, option3: string, option4: string, correctAnswer: string, answerDetails: string, questionId: number, chapterId: number)
    {
        return this.http.put<Question>(this.baseUrl + 'Admin',{ question, option1, option2, option3, option4, correctAnswer, answerDetails, questionId, chapterId })
        .pipe(map((response =>{<Question>response})));       
    }

    uploadQuestion(question: string, option1: string, option2: string, option3: string, option4: string, correctAnswer: string, answerDetails: string, chapterId: number)
    {
        return this.http.post<Question>(this.baseUrl + 'Admin/question/upload',{ question, option1, option2, option3, option4, correctAnswer, answerDetails, chapterId })
        .pipe(map((response =>{<Question>response})));       
    }

    addNewCourse(courseName: string, imageId: number): Observable<Course>{
        const params = new HttpParams()
                        .set('courseName', courseName)
                        .set('imageId', imageId.toString())
        return this.http.get(this.baseUrl+ 'admin/course', {params}).pipe(map((response => <Course>response)));
    }

    postFile(fileToUpload: File): Observable<Images> {
        const formData: FormData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        return this.http.post(this.baseUrl + 'Admin/upload', formData).pipe(map((response => <Images>response)));
    }

    deleteQuestion(questionId: number)
    {
        return this.http.delete(this.baseUrl + 'Admin/' + questionId);       
    }

}