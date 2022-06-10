[System.Serializable]
public class QusetionObj
{
    public int id;
    public string question;
    public string answer_a;
    public string answer_b;
    public string answer_c;
    public string answer_d;
    public string correct_answer;

    public string Answer_a
    {
        get { return answer_a; }
        set { answer_a = value; }
    }

    public string Answer_b
    {
        get { return answer_b; }
        set { answer_b = value; }
    }
    
    public string Answer_c
    {
        get { return answer_c; }
        set { answer_c = value; }
    }

    public string Answer_d
    {
        get { return answer_d; }
        set { answer_d = value; }
    }
}
