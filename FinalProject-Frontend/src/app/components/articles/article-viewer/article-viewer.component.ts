import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { map, switchMap, tap } from 'rxjs';
import { ArticleDataModel } from 'src/app/models/articleDataModel';
import { ListResponseModel } from 'src/app/models/listResponseModel';
import { QuestionGroupModel } from 'src/app/models/questions/questionGroupModel';
import { SequencedImageModel } from 'src/app/models/sequencedImageModel';
import { ArticleService } from 'src/app/services/article.service';
import { AuthService } from 'src/app/services/auth.service';
import { DarkLightSwitchListenerService } from 'src/app/services/dark-light-switch-listener.service';
import { QuestionService } from 'src/app/services/question.service';
import { SequencedImageService } from 'src/app/services/sequenced-image.service';
import { environment } from 'src/environments/environment';
declare var $:any;
@Component({
  selector: 'app-article-viewer',
  templateUrl: './article-viewer.component.html',
  styleUrls: ['./article-viewer.component.css']
})

export class ArticleViewerComponent {
  articleData:ArticleDataModel;
  articleId:number;
  sequencedImages:ListResponseModel<SequencedImageModel>
  currentImage:{url:string,sequence:number};
  currentSequence = 1;
  questionGroups:QuestionGroupModel[];
  isLogged:boolean;
  isAdmin:boolean = false;
  constructor(private authService:AuthService,private questionService:QuestionService ,private sequencedImageService:SequencedImageService, private articleService:ArticleService,private activatedRoute:ActivatedRoute,private translateService:TranslateService, private toastrService:ToastrService,private darkModeListener:DarkLightSwitchListenerService){}
  ngOnInit(){
    this.initializeArticleViewer();
    this.darkModeListenerFnc();
    this.isLogged = this.authService.isAuthenticated();
    this.isAdmin = localStorage.getItem("role") != undefined && localStorage.getItem("role") == "ADMIN" ? true: false;
  }
  prepareInjectStyle(){
    let styleEl = document.createElement('style');
    console.log(localStorage.getItem('darkMode'));
    if(localStorage.getItem('darkMode') == "true"){
      styleEl.innerHTML = "* { color:white !important; }";
    }else{
      styleEl.innerHTML = "* { color:black !important; }";
    }
    styleEl.innerHTML += "body { width:95%; overflow:hidden; word-break:break-word; box-sizing:border-box; padding:10px; margin:0px;}";
    return styleEl;
  }
  changeArticleViewerMode(){
    let articleFrameHead = $("#article-viewer").contents().find('head');
    articleFrameHead.find("style").remove();
    articleFrameHead.append(this.prepareInjectStyle());
  }
  darkModeListenerFnc(){
    this.darkModeListener.isDarkMode$.subscribe(data=>{
      this.changeArticleViewerMode();
      console.log("calisti");
    })
  }
  initializeArticleViewer(){
    this.activatedRoute.params.subscribe(param=>{
      this.articleId = param['articleId'];
      this.getQuestionGroups();
      this.getArticle(this.articleId).then(x=>{
        let articleFrameHead = $("#article-viewer").contents().find('head');
        let articleFrameBody = $("#article-viewer").contents().find('body');
        console.log($("#article-viewer").contents())
        if(x == true){
          articleFrameHead.append(this.prepareInjectStyle());
          articleFrameBody.html(this.articleData.articleContent);
        }else{
          this.toastrService.error(this.translateService.instant("errorToastMessage"));
        }
        $("#article-viewer").css("height",articleFrameBody.get(0).scrollHeight+"px");
      });
    })
  }
  getArticle(articleId:number):Promise<boolean>{
    return new Promise<boolean>((resolve,reject)=>{
      this.articleService.getArticleContent(articleId).subscribe({
        next:(response)=>{
          this.articleData = response.data
          this.getSequencedImages(articleId);
          console.log(this.articleData);
          resolve(true);
        },
        error:(err)=>{
          resolve(false);
        }
      })
    })

  }
  getSequencedImages(articleId:number){
    var webrootUrl = environment.webrootPath;
    var path = "sequencedImages/";
    //webrootUrl+path+x.imagePath
    var images = this.sequencedImageService.getImages(articleId).subscribe((result)=>{
      let temp:ListResponseModel<SequencedImageModel>;
      temp = result;
      temp.data.forEach((sequencedImage)=>{
        sequencedImage.imagePath = webrootUrl+path+sequencedImage.imagePath;
      })
      this.sequencedImages = temp;
      console.log(temp);
    })
    
  }
  nextImg(){
    if(this.currentSequence < this.sequencedImages.data.length){
      this.currentSequence += 1;
    }else{
      this.currentSequence = 1;
    }

  }
  getQuestionGroups(){
    this.questionService.getQuestionGroupsWithArticleId(this.articleId).subscribe({
      next:(response)=>{
        this.questionGroups = response.data;
      },
      error:(err)=>{

      }
    })
  }
  deleteArticle(){
    this.articleService.deleteArticle(this.articleId).subscribe({
      next:(response)=>{
        if(response.success){
          this.toastrService.success(response.message ?? "Makale silindi");
        }else{
          this.toastrService.error(response.message ?? this.translateService.instant("errorToastMessage"));
        }

      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant("errorToastMessage"));
      }
    })
  }

}
