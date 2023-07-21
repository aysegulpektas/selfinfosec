import { Component, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ArticleInfoModel } from 'src/app/models/articleInfoModel';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-articles-helper',
  templateUrl: './articles-helper.component.html',
  styleUrls: ['./articles-helper.component.css']
})
export class ArticlesHelperComponent {
  @Output() selectedArticleId = new EventEmitter<number>();
  articles:ArticleInfoModel[] = null;
  loading:boolean = false;
  constructor(private articleService:ArticleService,private toastrService:ToastrService) {}
  ngOnInit(){
    this.getArticles();
  }
  getArticles(){
    this.loading = true;
    this.articleService.getAllArticles().subscribe({
      next:(response)=>{
        this.articles = response.data;
        console.log(response);
        this.loading = false;
      },
      error:(err)=>{
        this.toastrService.error("Bir hata oluştu");
        this.loading = false;
      }
    })
  }
  emitArticleId(articleId:number){
    this.selectedArticleId.emit(articleId);
  }
  changeSelectedArticle($event:Event){

    let selectedArticleId = parseInt(($event.target as HTMLInputElement).value);
    if(selectedArticleId != -1){
      this.emitArticleId(selectedArticleId);
    }else{
      
    }
  }
}
