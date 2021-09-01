import { PerformanceService } from './_services/performance.service';
import { SubscriptionsService } from './_services/subscriptions.service';
import { AnswerComponent } from './question/answer/answer.component';
import { CourseService } from './_services/course.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CountdownModule } from 'ngx-countdown';
import { ChartsModule } from 'ng2-charts';


import { AppComponent } from './app.component';
import { appRoutingModule } from './app.routing';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ChapterComponent } from './chapter/chapter.component';
import { CourseComponent } from './course/course.component';
import { QuestionComponent } from './question/question.component'
import { CommonModule } from '@angular/common';
import { SubscriptionComponent } from './course/subscription/subscription.component';;
import { MySubscriptionsComponent } from './my-subscriptions/my-subscriptions.component';
import { DemoExamComponent } from './demo-exam/demo-exam.component';
import { AnswersComponent } from './demo-exam/answers/answers.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';
import { PracticeExamComponent } from './practice-exam/practice-exam.component';
import { DemoQuestionComponent } from './demo-exam/demo-question/demo-question.component';
import { ChartJsComponent } from './chart-js/chart-js.component';
import { ProgressGraphComponent } from './progress-graph/progress-graph.component';
import { CourseCompletionComponent } from './course-completion/course-completion.component';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { HomeComponentComponent } from './home-component/home-component.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { UploadQuestionComponent } from './upload-question/upload-question.component';
import { ViewQuestionComponent } from './view-question/view-question.component';
import { ViewStudentComponent } from './view-student/view-student.component';
import { AdminHomePageComponent } from './admin-home-page/admin-home-page.component';
import { AddCourseComponent } from './add-course/add-course.component';
import { AdminService } from './_services/admin.service';
import { UpdateCourseComponent } from './update-course/update-course.component';
import { AddQuestionComponent } from './update-course/add-question/add-question.component';
import { ViewQuestionsComponent } from './update-course/view-questions/view-questions.component';
@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        appRoutingModule,
        CountdownModule,
        FormsModule,
        CommonModule,
        ChartsModule,
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        SignUpComponent ,
        ChapterComponent ,
        CourseComponent ,
        QuestionComponent,
        AnswerComponent ,
        SubscriptionComponent,
        MySubscriptionsComponent,
        DemoExamComponent ,
        AnswersComponent ,
        ImageUploadComponent ,
        PracticeExamComponent ,
        DemoQuestionComponent,
        ChartJsComponent ,
        ProgressGraphComponent ,
        CourseCompletionComponent ,
        LoadingSpinnerComponent ,
        HomeComponentComponent,
        ForbiddenComponent ,
        UploadQuestionComponent ,
        ViewQuestionComponent ,
        ViewStudentComponent ,
        AdminHomePageComponent,
        AddCourseComponent
,
        UpdateCourseComponent ,
        AddQuestionComponent ,
        ViewQuestionsComponent  ],
        
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        CourseService,
        SubscriptionsService,
        PerformanceService,
        AdminService

        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
