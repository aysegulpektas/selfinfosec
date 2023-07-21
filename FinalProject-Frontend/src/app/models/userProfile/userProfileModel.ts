import { SuccessRateDto } from "./successRateDto";
import { UserProfileInfoModel } from "./userProfileInfoModel";

export interface UserProfileModel {
    userInfo:UserProfileInfoModel;
    completedArticles:SuccessRateDto[];
}