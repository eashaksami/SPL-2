import { Answers } from './../_models/answers';
import { Images } from './../_models/images';
import { User } from './../_models/user';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Chapter } from '@app/_models/Chapter';
import { Course } from '@app/_models/Course';
import { Question } from '@app/_models/question';
import { TestQuestion } from '@app/_models/TestQuestion';


@Injectable()
export class CourseService{
    baseUrl = 'http://localhost:4000/';
    user: User;
    questions: Question[];
    testQuestions: TestQuestion[];
    selectedAnswer: any;
    correctOrWrong: any;

    examType: string = 'testExam';
    SeenOrUnseen: number = 0;
    isCorrect: string = 'no';
    isWrong: string = 'no';
    CorrectOrWrong: number = 0;
    chapterIds: number[];
    howManyQuestions: number = 25;
    examStarted: boolean = false;
    
    constructor(private http: HttpClient) {}

    getCourses(studentId: number): Observable<Course[]>{
        const params = new HttpParams()
                        .set('studentId', studentId.toString())
        return this.http.get(this.baseUrl+ 'test', {params}).pipe(map((response => <Course[]>response)));
    }

    getAllCourses(): Observable<Course[]>{
        return this.http.get(this.baseUrl+ 'test/allcourses').pipe(map((response => <Course[]>response)));
    }

    getCoursesDemo(): Observable<Course[]>{
        return this.http.get(this.baseUrl+ 'test/demo/questions/exam/sami')
        .pipe(map((response => <Course[]>response)));
    }

    grtChapters(id: number): Observable<Chapter[]>{
        console.log(this.http.get(this.baseUrl+'test/chapters'+ id).pipe(map((response => <Chapter[]>response))));
        return this.http.get(this.baseUrl+'test/chapters/'+ id).pipe(map((response => <Chapter[]>response)));
    }

    getQuestions(chapterIds: number[], studentId: number,
        examType: string,CorrectOrWrong: number,SeenOrUnseen: number, TotalQuestion: number)
    {
            return this.http.post(this.baseUrl+'test/exam/questions', {studentId, examType, CorrectOrWrong,
                                                                        SeenOrUnseen, TotalQuestion, chapterIds})
                .pipe(map((response => <Question[]>response)));

    }

    getDemoQuestions(courseCode: number): Observable<Question[]>{
        console.log(courseCode);
        const params = new HttpParams().set('courseCode', courseCode.toString());
        return this.http.get(this.baseUrl+ 'test/demo/questions/exam',{params})
        .pipe(map((response => <Question[]>response)));
    }

    getAnswers(questionId: number[]){
        return this.http.post(this.baseUrl + 'test/answers',{ questionId })
        .pipe(map((response =><Answers[]>response)));
    }

    updateDatabase(questionId: number[], isCorrect: number[], examType: string, studentId: number)
    {
        // const headers = new HttpHeaders()
        //  .set('Content-Type', 'application/json');
        // let options = { headers: headers };
        // var b = JSON.stringify(jsonData);
        return this.http.post<any>(this.baseUrl + 'test',{ questionId, isCorrect, examType,studentId })
        .pipe(map((response =>{console.log(response)})));       
    }

    getImages(): Observable<Images[]>{
         return this.http.get<any>(this.baseUrl + 'subscription') 
         .pipe(map((response =><Images[]>response)));      
    }

}
