import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AddSubCategoryModel } from 'src/app/models/subcategory/addSubCategoryModel';
import { SubCategoryService } from 'src/app/services/sub-category.service';

@Component({
  selector: 'app-add-subcategory',
  templateUrl: './add-subcategory.component.html',
  styleUrls: ['./add-subcategory.component.css']
})
export class AddSubcategoryComponent {
  addSubCategoryForm:FormGroup;
  constructor(private subcategoryService:SubCategoryService,private formBuilder:FormBuilder,private toastrService:ToastrService){}
  ngOnInit(){
    this.createSubCategoryForm();
  }
  createSubCategoryForm(){
    this.addSubCategoryForm = this.formBuilder.group({
      subcategoryName: ["",Validators.required],
      categoryId: ["",Validators.required],
    })
  }
  addSubCategory(){
    let subcategoryModel:AddSubCategoryModel = Object.assign({},this.addSubCategoryForm.value);
    console.log(subcategoryModel);
    this.subcategoryService.addSubCategory(subcategoryModel).subscribe({
      next:(response)=>{
        if(response.success){
          this.toastrService.success(response.message && response.message != "" ? response.message :  "Alt Kategori başarıyla eklendi")
        }else{
          this.toastrService.error(response.message && response.message != "" ? response.message :  "Alt Kategori eklenemedi")
        }
      },error:(err)=>{
        this.toastrService.error("Alt Kategori eklenemedi");
      }
    })
  }
}
