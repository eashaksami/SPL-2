import { CourseCompletion } from './../_models/courseCompletion';
import { Progress } from './../_models/progress';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class PerformanceService{
    baseUrl = 'http://localhost:4000/';

    constructor(private http: HttpClient){}
    
    getProgressData(studentId: number, courseCode: number): Observable<Progress[]>{
        const params = new HttpParams()
                        .set('studentId', studentId.toString())
                        .set('courseCode', courseCode.toPrecision());
        return this.http.get(this.baseUrl+ 'performance', {params}).pipe(map((response => <Progress[]>response)));
    }

    getCourseCompletionData(studentId: number, courseCode: number): Observable<CourseCompletion[]>{
        const params = new HttpParams()
                        .set('studentId', studentId.toString())
                        .set('courseCode', courseCode.toPrecision());
        return this.http.get(this.baseUrl+ 'performance/courseCompletion', {params}).pipe(map((response => <CourseCompletion[]>response)));
    }
}