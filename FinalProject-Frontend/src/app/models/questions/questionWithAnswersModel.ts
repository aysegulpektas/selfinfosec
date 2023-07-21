import { AnswerWithTrue } from "./answerWithTrue";

export interface QuestionWithAnswerModel {
    questionId:number,
    questionText:string,
    answers:AnswerWithTrue[]
}