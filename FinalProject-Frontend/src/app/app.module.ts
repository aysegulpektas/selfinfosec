import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { NaviComponent } from './components/navi/navi.component';
import { RegisterComponent } from './components/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { CategoryComponent } from './components/category/category.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AddCategoryComponent } from './components/admin/add-category/add-category.component';
import { MainComponent } from './components/main/main.component';
import { IntroductionComponent } from './components/introduction/introduction.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ArticleListItemComponent } from './components/articles/article-list-item/article-list-item.component';
import { SettingsNaviComponent } from './components/settings-navi/settings-navi.component';
import { SubcategoriesComponent } from './components/subcategories/subcategories.component';
import { AddArticleComponent } from './components/admin/add-article/add-article.component';
import { ArticleViewerComponent } from './components/articles/article-viewer/article-viewer.component';
import { FooterComponent } from './components/footer/footer.component';
import { PreferencesComponent } from './components/preferences/preferences.component';
import { LanguageSelectorComponent } from './components/language-selector/language-selector.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { ProfileNaviComponent } from './components/profile-page/profile-navi/profile-navi.component';
import { ProfileProgressComponent } from './components/profile-page/profile-progress/profile-progress.component';
import { AddSubcategoryComponent } from './components/admin/add-subcategory/add-subcategory.component';
import { AddSequencedImageComponent } from './components/admin/add-sequenced-image/add-sequenced-image.component';
import { AddQuestionComponent } from './components/admin/add-question/add-question.component';
import { QuizAnswersViewerComponent } from './components/quiz/quiz-answers-viewer/quiz-answers-viewer.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ArticlesHelperComponent } from './components/helperComponents/articles-helper/articles-helper.component';
import { CategoryNamePipe } from './pipes/category-name.pipe';

export function HttpLoaderFactory(httpClient:HttpClient){
  return new TranslateHttpLoader(httpClient,"/assets/i18n/","-language.json");
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NaviComponent,
    RegisterComponent,
    CategoryComponent,
    ArticlesComponent,
    AddCategoryComponent,
    MainComponent,
    IntroductionComponent,
    ForgotPasswordComponent,
    SettingsComponent,
    ChangePasswordComponent,
    ArticleListItemComponent,
    SettingsNaviComponent,
    SubcategoriesComponent,
    AddArticleComponent,
    ArticleViewerComponent,
    FooterComponent,
    PreferencesComponent,
    LanguageSelectorComponent,
    QuizComponent,
    ProfilePageComponent,
    ProfileNaviComponent,
    ProfileProgressComponent,
    AddSubcategoryComponent,
    AddSequencedImageComponent,
    AddQuestionComponent,
    QuizAnswersViewerComponent,
    ResetPasswordComponent,
    ArticlesHelperComponent,
    CategoryNamePipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    TranslateModule.forRoot({
      loader:{
        provide:TranslateLoader,
        useFactory:HttpLoaderFactory,
        deps:[HttpClient]
      },
      defaultLanguage:"tr"
    })


  ],
  providers: [{provide:HTTP_INTERCEPTORS,useClass:AuthInterceptor,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
