import { AnswerModel } from "./answerModel";

export interface QuestionModel {
    questionId:number,
    questionText:string,
    answers:AnswerModel[]
}