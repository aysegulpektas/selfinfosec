import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AddCategoryModel } from 'src/app/models/category/addCategoryModel';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  addCategoryForm:FormGroup;
  constructor(private categoryService:CategoryService,private formBuilder:FormBuilder,private toastrService:ToastrService) {

  }
  ngOnInit(){
    this.createAddCategoryForm();
  }
  createAddCategoryForm(){
    this.addCategoryForm = this.formBuilder.group({
      categoryName: ["",Validators.required],
      categoryDescription: [""]
    })
  }
  addCategory(){
    //Backend tarafında kategori açıklaması boş bir şekilde kategori eklenmesine destek için güncelleme yapılacak...
    let categoryModel:AddCategoryModel = Object.assign({},this.addCategoryForm.value);
    this.categoryService.addCategory(categoryModel).subscribe({
      next:(response)=>{
        if(response.success){
          this.toastrService.success(response.message && response.message != "" ? response.message :  "Kategori başarıyla eklendi")
        }else{
          this.toastrService.error(response.message && response.message != "" ? response.message :  "Kategori eklenemedi")
        }
      },error:(err)=>{
        this.toastrService.error("Kategori eklenemedi");
      }
    })
  }
}
