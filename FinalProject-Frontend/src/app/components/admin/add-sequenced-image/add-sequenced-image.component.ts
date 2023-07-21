import { HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-add-sequenced-image',
  templateUrl: './add-sequenced-image.component.html',
  styleUrls: ['./add-sequenced-image.component.css']
})
export class AddSequencedImageComponent {
  selectedArticle:number = -1;
  constructor(private formBuilder:FormBuilder,private toastrService:ToastrService,private quizService:QuizService){

  }
  ngOnInit(){

  }

  createFormData():FormData{
    var formData = new FormData();
    if(this.selectedArticle == -1){
      this.toastrService.error("Lütfen makale seçin");
    }
    let sequence = document.getElementById('sequence') as HTMLInputElement;
    let imageFile = document.getElementById('imageFile') as HTMLInputElement;
    let description = document.getElementById('description') as HTMLInputElement;
    let imagePath = document.getElementById('imagePath') as HTMLInputElement;
    let showType = document.getElementById('showType') as HTMLInputElement;
    formData.append('articleId',this.selectedArticle.toString());
    formData.append('sequence',sequence.value);
    formData.append('imageFile',imageFile.files[0]);
    formData.append('description',description.value);
    formData.append('imagePath',imagePath.value);
    formData.append('showType',showType.value);
    return formData;
  }
  sendImage(){
    this.quizService.addImage(this.createFormData()).subscribe({
      next:(response)=>{
        if(response.type == HttpEventType.Response){
          let data = response.body as any;
          if(data.success){
            this.toastrService.success("Resim başarıyla yüklendi");
          }else{
            this.toastrService.error("Resim yüklenirken bir hata oluştu");
          }
        }

      },
      error:(err)=>{
        this.toastrService.error("Resim yüklenirken bir hata oluştu");
      }
    })
  }
  getArticleId(selArticleId:number){
    this.selectedArticle = selArticleId;
  }
}


