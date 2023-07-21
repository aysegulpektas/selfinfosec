import { Component, Input } from '@angular/core';
import {  Router } from '@angular/router';
import { ArticleInfoModel } from 'src/app/models/articleInfoModel';
import { SubCategoryModel } from 'src/app/models/subcategory/subCategoryModel';

@Component({
  selector: 'app-article-list-item',
  templateUrl: './article-list-item.component.html',
  styleUrls: ['./article-list-item.component.css']
})
export class ArticleListItemComponent {
  @Input() article:ArticleInfoModel;
  @Input() categories:SubCategoryModel[];
  currentCategory:SubCategoryModel;
  constructor(private router:Router) {

  }
  ngOnInit(){
    this.currentCategory = this.categories.filter(x=>x.subcategoryId ==this.article.subcategoryId)[0];
  }
  goToArticle(article:ArticleInfoModel){
    this.router.navigate(["/article/view/"+article.articleId]);
  }
}
