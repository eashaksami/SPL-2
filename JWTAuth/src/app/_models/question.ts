export interface Question{
    questionId: number;
    chapterId: number;
    imageId: number;
    imageUrl: string;
    imageIdForAnswer: number;
    imageUrlForAnswer: string;
    question: string;
    option1: string;
    option2: string;
    option3: string;
    option4: string;
    correctAnswer: string;
    answerDetails: string;
}
