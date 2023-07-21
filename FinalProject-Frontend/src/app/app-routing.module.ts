import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { RegisterComponent } from './components/register/register.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { IntroductionComponent } from './components/introduction/introduction.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { AddArticleComponent } from './components/admin/add-article/add-article.component';
import { ArticleViewerComponent } from './components/articles/article-viewer/article-viewer.component';
import { PreferencesComponent } from './components/preferences/preferences.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { ProfileProgressComponent } from './components/profile-page/profile-progress/profile-progress.component';
import { AddSequencedImageComponent } from './components/admin/add-sequenced-image/add-sequenced-image.component';
import { AddCategoryComponent } from './components/admin/add-category/add-category.component';
import { AddSubcategoryComponent } from './components/admin/add-subcategory/add-subcategory.component';
import { AddQuestionComponent } from './components/admin/add-question/add-question.component';
import { QuizAnswersViewerComponent } from './components/quiz/quiz-answers-viewer/quiz-answers-viewer.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

const routes: Routes = [
  {path:"", component:IntroductionComponent},
  {path:"login", component:LoginComponent},
  {path:"register", component:RegisterComponent},
  {path:"main",component:MainComponent},
  {path:"articles",component:ArticlesComponent},
  {path:"forgot-password",component:ForgotPasswordComponent},
  {path:"settings",component:SettingsComponent},
  {path:"change-password",component:ChangePasswordComponent},
  {path:"add-article",component:AddArticleComponent},
  {path:"article/view/:articleId",component:ArticleViewerComponent},
  {path:"preferences",component:PreferencesComponent},
  {path:"quiz/:id",component:QuizComponent},
  {path:"profile",component:ProfilePageComponent},
  {path:"progress",component:ProfileProgressComponent},
  {path:"add-sequenced-image",component:AddSequencedImageComponent},
  {path:"add-category",component:AddCategoryComponent},
  {path:"add-subcategory",component:AddSubcategoryComponent},
  {path:"add-question",component:AddQuestionComponent},
  {path:"quiz-answers/:id",component:QuizAnswersViewerComponent},
  {path:"reset-password",component:ResetPasswordComponent},
  {path:"introduction", component:IntroductionComponent},
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
