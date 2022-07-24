using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutputGenerator : MonoBehaviour
{
    private Dictionary <string, string> descriptions = new Dictionary<string, string>();
    private Dictionary <string, int> maxMistakes = new Dictionary <string, int>();
    private Dictionary <string, string> templates = new Dictionary<string, string>();
    private int score = 0;
    private string summaryReact = "";
    private string summarydisplay = "";
    private int correctAnswers = 0;
    private int bonusPerQuestion = 1;




    private void DescriptionsAddingValues(){
        descriptions.Add("traffic_light", "מעבר רמזור לפי הוראות החוק");
        descriptions.Add("stop_sign", "קיום הוראת תמרור עצור");
        descriptions.Add("yield_sign", "תמרור האט ותן זכות קדימה");
        descriptions.Add("no_enter_sign", "אי כניסה לרחוב המוצב בו תמרור אין כניסה");
        descriptions.Add("crosswalk_sign", "תמרור מעבר חציה");
        descriptions.Add("bump_sign", "תמרור פסי האטה");
        descriptions.Add("square_sign", "מתן זכות לרכב הנמצא קודם לכן בכיכר תנועה");
        descriptions.Add("speed_limit_sign", "הגבלת מהירות הנסיעה");
        descriptions.Add("no_turn_back_sign", "ציות לתמרור האוסר פניית פרסה");
        descriptions.Add("blinker", "הדלקת פנס איתות בעת פנייה");
        descriptions.Add("continuous_line", "שמירה על אי חציית קו הפרדה רצוף");
    }

    private void MaxMistakesAddingValues(){
        maxMistakes.Add("traffic_light", 5);
        maxMistakes.Add("stop_sign", 5);
        maxMistakes.Add("yield_sign", 5);
        maxMistakes.Add("no_enter_sign", 3);
        maxMistakes.Add("crosswalk_sign", 5);
        maxMistakes.Add("bump_sign", 5);
        maxMistakes.Add("square_sign", 3);
        maxMistakes.Add("speed_limit_sign", 5);
        maxMistakes.Add("no_turn_back_sign", 3);
        maxMistakes.Add("blinker", 10);
        maxMistakes.Add("continuous_line", 5);
    }

    
    private void TemplatesAddingValues()
    {
        templates.Add("subTitie1", "פירוט הערות וניקוד על החוקים:");
        templates.Add("subTitie2", "שיפורים אפשריים:");
        templates.Add("numMistakesRule", "כמות הפעמים בהם עברת על חוק זה הינו {0}.");
        templates.Add("scoreReduce1", "עבור כל מעבר על חוק זה הירידה בציון היא {0}, ובסך הכול {1}.");
        templates.Add("scoreReduce2", "עבור חוק זה הפירוט הוא אחת משלוש רמות, והרמה עבור מבחן זה היא {0}, והירידה בציון היא {1}.");
        templates.Add("subTitieBonus", "בונוס שאלות:");
        templates.Add("answersBonus", "מספר השאלות שנענו נכון הן {0}, עבור כל שאלה יש בונוס של {1} נקודות, ובסך הכל נוספו {2} נקודות לציון.");
    }


    public void MakeOutput()
    {
        DescriptionsAddingValues();
        MaxMistakesAddingValues();
        TemplatesAddingValues();
        CalculateQuestionBonus();

        (this.summaryReact, this.score) = OutputSummaryReact();
        this.summarydisplay = OutputSummaryDisplay();
        
    }

    private (string, int) OutputSummaryReact()
    {
        Dictionary<string, Logger.RuleObj> rules = Logger.Instance.rulesMistakes;
        int numOfRules = rules.Count;
        int tempScore = 100;
        int pointsPerRule = 100 / numOfRules;

        string strOut = "";
        foreach(KeyValuePair<string, Logger.RuleObj> entry in rules)
        {
            string ruleStr;
            if (!descriptions.TryGetValue(entry.Key, out ruleStr))
            {
                Debug.Log("Error: this rule is not existing in this current test!");
                break;
            }

            int mistakes = entry.Value.mistakes;
            int pointsPerMistakes = pointsPerRule / maxMistakes[entry.Key];
            bool isMaxMistakes = maxMistakes[entry.Key] <= mistakes ? true : false;
            int reduce = isMaxMistakes ? pointsPerRule : (mistakes * pointsPerMistakes);
            tempScore -= reduce;

            strOut += ruleStr + ": " + String.Format(templates["numMistakesRule"], mistakes) 
                    + " " + String.Format(templates["scoreReduce1"], pointsPerMistakes, reduce);
            strOut += "\n";
        }

        // bonus questions
        strOut += "\n";
        strOut += templates["subTitieBonus"] + " " 
                + String.Format(templates["answersBonus"], correctAnswers, bonusPerQuestion, correctAnswers * bonusPerQuestion);
        tempScore += (this.correctAnswers * this.bonusPerQuestion);


        return (strOut, tempScore);
    }

    private string OutputSummaryDisplay()
    {
        Dictionary<string, Logger.RuleObj> rules = Logger.Instance.rulesMistakes;
        int numOfRules = rules.Count;
        int tempScore = 100;
        int pointsPerRule = 100 / numOfRules;

        string strOut = "";
        strOut += "<size=120%><b>" + templates["subTitie1"] + "</b></size>\n";
        foreach(KeyValuePair<string, Logger.RuleObj> entry in rules)
        {
            string ruleStr;
            if (!descriptions.TryGetValue(entry.Key, out ruleStr))
            {
                Debug.Log("Error: this rule is not existing in this current test!");
                break;
            }

            int mistakes = entry.Value.mistakes;
            int pointsPerMistakes = pointsPerRule / maxMistakes[entry.Key];
            bool isMaxMistakes = maxMistakes[entry.Key] <= mistakes ? true : false;
            int reduce = isMaxMistakes ? pointsPerRule : (mistakes * pointsPerMistakes);
            tempScore -= reduce;

            strOut += "<b>" + ruleStr + ":</b> " + String.Format(templates["numMistakesRule"], IntReverseConvertStr(mistakes)) 
                    + " " + String.Format(templates["scoreReduce1"], IntReverseConvertStr(pointsPerMistakes), IntReverseConvertStr(reduce));
            strOut += "\n";
        }

        // bonus questions
        strOut += "\n";
        strOut += "<b>" + templates["subTitieBonus"] + "</b>\n" 
                + String.Format(templates["answersBonus"], correctAnswers, bonusPerQuestion, correctAnswers * bonusPerQuestion);

        return strOut;
    }

    public (int, string) GetOutputReact()
    {
        return (this.score, this.summaryReact);
    }

    public (int, string) GetOutputDisplay()
    {
        return (this.score, this.summarydisplay);
    }

    private string IntReverseConvertStr(int num)
    {
        char[] arr = num.ToString().ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    private void CalculateQuestionBonus()
    {        
        Dictionary<string, RuleQuestionResults> questionResultsMap =Logger.Instance.questionResultsMap;
        foreach(KeyValuePair<string, RuleQuestionResults> entry in questionResultsMap)
        {
            Debug.Log("=============\nQuestios rule " + entry.Key);
            Debug.Log("Questions Left: " + entry.Value.GetNumQuestionsLeft().ToString());
            Debug.Log("Correct answer: " + entry.Value.GetNumCorrectAnswers().ToString() + "\n=============");

            this.correctAnswers += entry.Value.GetNumCorrectAnswers();
        }
    }
}