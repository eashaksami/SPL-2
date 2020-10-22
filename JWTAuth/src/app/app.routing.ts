import { ChartJsComponent } from './chart-js/chart-js.component';
import { DemoQuestionComponent } from './demo-exam/demo-question/demo-question.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';
import { AnswersComponent } from './demo-exam/answers/answers.component';
import { DemoExamComponent } from './demo-exam/demo-exam.component';
import { MySubscriptionsComponent } from './my-subscriptions/my-subscriptions.component';
// import { SubscriptionComponent } from './subscription/subscription.component';
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

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: SignUpComponent},
    { path: 'image', component: ImageUploadComponent},
    { path: 'demo', component: DemoExamComponent},
    { path: 'demo/answer', component: AnswersComponent},
    { path: 'subscription', component: SubscriptionComponent },
    { path: 'subscriptions', component: MySubscriptionsComponent },
    { path: 'test', component: CourseComponent },
    { path: 'chart', component: ChartJsComponent },
    { path: 'demoExam/question/:courseCode', component: DemoQuestionComponent },
    { path: 'test/:id', component: ChapterComponent },
    { path: 'test/questions/:chapterId', component: QuestionComponent },
    { path: 'test/questions/answer/details', component: AnswerComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);