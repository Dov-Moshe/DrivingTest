[System.Serializable]
public class RuleQuestionResults
{
    private int numOfQuestions;
    private int countQuestionsPassed;
    private int numOfCorrectAnswers;
    private int numOfQuestionsLeft;


    public RuleQuestionResults(int numOfQuestions)
    {
        this.numOfQuestions = numOfQuestions;
        this.numOfCorrectAnswers = 0;
        this.countQuestionsPassed = 0;
        this.numOfQuestionsLeft = numOfQuestions;

    }

    public void IncCount(bool isCorrect)
    {
        countQuestionsPassed++;
        if(isCorrect)
        {
            numOfCorrectAnswers++;
        }
    }

    public int GetNumOfQuestions()
    {
        return this.numOfQuestions;
    }

    public int GetNumCorrectAnswers()
    {
        return this.numOfCorrectAnswers;
    }

    public void UpdateQuestionsLeft()
    {
        if (this.numOfQuestionsLeft > 0)
            this.numOfQuestionsLeft--;
    }

    public int GetNumQuestionsLeft()
    {
        return this.numOfQuestionsLeft;
    }

}
