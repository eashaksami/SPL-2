import { UpdateCourseComponent } from './update-course/update-course.component';
import { AddCourseComponent } from './add-course/add-course.component';
import { ViewStudentComponent } from './view-student/view-student.component';
import { AdminHomePageComponent } from './admin-home-page/admin-home-page.component';
import { HomeComponentComponent } from './home-component/home-component.component';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { CourseCompletionComponent } from './course-completion/course-completion.component';
import { ProgressGraphComponent } from './progress-graph/progress-graph.component';
import { ChartJsComponent } from './chart-js/chart-js.component';
import { DemoQuestionComponent } from './demo-exam/demo-question/demo-question.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';
import { AnswersComponent } from './demo-exam/answers/answers.component';
import { DemoExamComponent } from './demo-exam/demo-exam.component';
import { MySubscriptionsComponent } from './my-subscriptions/my-subscriptions.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { CourseComponent } from './course/course.component';
import { ChapterComponent } from './chapter/chapter.component';
import { QuestionComponent } from './question/question.component';
import { AnswerComponent } from './question/answer/answer.component';
import { SubscriptionComponent } from './course/subscription/subscription.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const routes: Routes = [
    { path: '', component: HomeComponentComponent },
    { path: 'admin', component: AdminHomePageComponent, canActivate: [AuthGuard], data: {permittedRoles: ['Admin']} },
    { path: 'admin/viewStudent', component: ViewStudentComponent, canActivate: [AuthGuard], data: {permittedRoles: ['Admin']} },
    { path: 'admin/addCourse', component: AddCourseComponent, canActivate: [AuthGuard], data: {permittedRoles: ['Admin']} },
    { path: 'admin/updateCourse/:courseCode', component: UpdateCourseComponent, canActivate: [AuthGuard], data: {permittedRoles: ['Admin']} },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: SignUpComponent },
    { path: 'forbidden', component: ForbiddenComponent },
    { path: 'image', component: ImageUploadComponent, canActivate: [AuthGuard]},
    // { path: 'demo', component: DemoExamComponent },
    // , canActivate: [AuthGuard], data: {permittedRoles: ['Admin']}
    { path: 'demo', component: DemoExamComponent },
    { path: 'demo/answer', component: AnswersComponent },
    { path: 'subscription', component: SubscriptionComponent , canActivate: [AuthGuard]},
    { path: 'subscriptions', component: MySubscriptionsComponent , canActivate: [AuthGuard]},
    { path: 'test', component: CourseComponent },
    // { path: 'chart', component: ChartJsComponent , canActivate: [AuthGuard]},
    { path: 'progress/:courseCode', component: ProgressGraphComponent , canActivate: [AuthGuard]},
    { path: 'coursecompletion/:courseCode', component: CourseCompletionComponent , canActivate: [AuthGuard]},
    { path: 'demoExam/question/:courseCode', component: DemoQuestionComponent },
    { path: 'test/:id', component: ChapterComponent , canActivate: [AuthGuard]},
    { path: 'test/questions/:chapterId', component: QuestionComponent , canActivate: [AuthGuard]},
    { path: 'test/questions/answer/details', component: AnswerComponent , canActivate: [AuthGuard]},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
