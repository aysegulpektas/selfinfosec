import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { CategoryModel } from 'src/app/models/category/categoryModel';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})

export class CategoryComponent implements OnInit{
  categories:CategoryModel[] = [];
  constructor(private translateService:TranslateService, private categoryService: CategoryService,private toastrService:ToastrService) { }
  ngOnInit(): void {
    this.getCategories();
  }
  getCategories(){
    this.categoryService.getCategories().subscribe({
      next:(response)=>{
        this.categories = response.data;
      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant("errorToastMessage"));
        console.log("Kategoriler alınırken bir hata oluştu");
      }
    })
  }
}
