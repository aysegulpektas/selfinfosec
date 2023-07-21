import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SubCategoryModel } from 'src/app/models/subcategory/subCategoryModel';
import { ArticleService } from 'src/app/services/article.service';
import { DynamicFormValidatorService } from 'src/app/services/dynamic-form-validator.service';
import { SubCategoryService } from 'src/app/services/sub-category.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css']
})
export class AddArticleComponent implements OnInit {
  subcategories: SubCategoryModel[];
  addArticleForm: FormGroup;
  constructor(private articleService:ArticleService,private dynamicValidator: DynamicFormValidatorService, private subcategoryService: SubCategoryService, private toastrService: ToastrService, private formBuilder: FormBuilder) { }
  ngOnInit(): void {
    this.getSubcategories();
    this.createAddArticleForm();

  }
  createAddArticleForm() {
    this.addArticleForm = this.formBuilder.group({
      articleTitle: ["", Validators.required],
      articleDescription: ["", Validators.required],
      subcategoryId: [],
      articleContent: ["", Validators.required],
      contentType: ["", Validators.required],
      languageCode:["",Validators.required]
    })
  }
  addArticle() {
    var isValid = this.dynamicValidator.validate(this.addArticleForm);
    if (isValid) {
      let formValues = Object.assign({}, this.addArticleForm.value)
      formValues.subcategoryId = parseInt(formValues.subcategoryId);
      console.log(formValues);

      this.articleService.addArticle(formValues).subscribe({
        next:(response)=>{
          if(response.success){
            this.toastrService.success("Makale başarıyla eklendi");
          }else{
            console.log(response);
            this.toastrService.error("Makale eklenirken bir sorun oluştu");
          }
        },
        error:(err)=>{
          console.log(err);
          this.toastrService.error("Makale eklenirken bir sorun oluştu");
        }
      })
    }
  }
  getSubcategories() {
    this.subcategoryService.getSubCategories().subscribe({
      next: (response) => {
        this.subcategories = response.data;
      },
      error: () => {
        this.toastrService.error("Bir hata oluştu")
      }
    })
  }

}
