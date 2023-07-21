import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ArticleFilterDto } from 'src/app/models/articleFilterDto';
import { ArticleInfoModel } from 'src/app/models/articleInfoModel';
import { CategoryModel } from 'src/app/models/category/categoryModel';
import { SubCategoryModel } from 'src/app/models/subcategory/subCategoryModel';
import { ArticleService } from 'src/app/services/article.service';
import { CategoryService } from 'src/app/services/category.service';
import { SubCategoryService } from 'src/app/services/sub-category.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})

export class ArticlesComponent implements OnInit {
  categories:CategoryModel[] = [];
  subcategories:SubCategoryModel[] = [];
  selectedSubCategories:number[] = [];
  articles:ArticleInfoModel[];
  selectedSubcategories:number[] = [];
  searchTitle:string = "";
  loading:boolean = false;
  loadingSubcategories = true;
  isAdmin:boolean = false;
  constructor(private articleService:ArticleService,private translateService:TranslateService,private categoryService: CategoryService,private toastrService:ToastrService,private subcategoryService:SubCategoryService) { }
  ngOnInit(): void {
    this.isAdmin = localStorage.getItem("role") != undefined && localStorage.getItem("role") == "ADMIN" ? true: false;
    this.getCategories();
    this.getSubCategories();
    this.getArticles();
  }
  getCategories(){
    this.categoryService.getCategories().subscribe({
      next:(response)=>{
        this.categories = response.data;
      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant("errorToastMessage"));
        console.log("Kategoriler alınırken bir hata oluştu")
      }
    })
  }
  getSubCategories(){
    this.loadingSubcategories = true;
    this.subcategoryService.getSubCategories().subscribe({
      next:(response)=>{
        this.subcategories = response.data;
        this.loadingSubcategories = false;
      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant("errorToastMessage"));
        console.log("Alt kategoriler alınırken bir hata oluştu")

      }
    })
  }
  getArticles(){
    let filter:ArticleFilterDto = {subcategories:[],title:""};
    filter.subcategories = this.selectedSubCategories;
    let searchInput = document.getElementById('searchTitleInput') as HTMLInputElement;
    filter.title = searchInput.value ?? "";
    this.loading = true;
    this.articleService.getAllArticles(filter).subscribe({
      next:(response)=>{
        this.articles = response.data;
        console.log(response);
        this.loading = false;
      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant("errorToastMessage"));
        this.loading = false;
      }
    })
  }
  isActiveSubcategories(id:number){
    let find = this.selectedSubCategories.find(x=>x == id);
    if(find == null){
      return false;
    }else{
      return true;
    }
  }
  toggleActive(id:number){
    let isActive = this.isActiveSubcategories(id);
    if(isActive){
      this.removeItemAll(id);
    }else{
      this.selectedSubCategories.push(id);
    }
    this.getArticles();
  }
  removeItemAll(value:number) {
    let arr = this.selectedSubCategories;
    var i = 0;
    while (i < arr.length) {
      if (arr[i] === value) {
        arr.splice(i, 1);
      } else {
        ++i;
      }
    }
    this.selectedSubCategories = arr;
  }
  clearSelectedSubcategories(){
    this.selectedSubCategories = [];
    this.getArticles();
  }
}
