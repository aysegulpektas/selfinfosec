import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserProfileInfoModel } from 'src/app/models/userProfile/userProfileInfoModel';
import { UserProfileModel } from 'src/app/models/userProfile/userProfileModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile-progress',
  templateUrl: './profile-progress.component.html',
  styleUrls: ['./profile-progress.component.css']
})
export class ProfileProgressComponent {
  userProfileInfo:UserProfileModel;
  loading:boolean;
  constructor(private userServie:UserService,private toastrService:ToastrService){}
  ngOnInit(){
    this.loading = true;
    this.userServie.getProgress().subscribe({

      next:(response)=>{
        this.loading = false;
        this.userProfileInfo = response.data;
      },error:(err)=>{
        this.loading = false;
        this.toastrService.error("Profil ilerlemeniz getirilirken bir sorun oluştu");
      }
    })
  }

}
