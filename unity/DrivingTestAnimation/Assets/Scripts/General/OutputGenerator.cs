using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutputGenerator : MonoBehaviour
{
    private Dictionary <string, string> descriptions = new Dictionary<string, string>();
    private Dictionary <string, string> conjunctions = new Dictionary<string, string>();
    private Dictionary <string, string> templates = new Dictionary<string, string>();
    private int score = 0;
    private string summary = "";

    private void DescriptionsAddingValues(){
        descriptions.Add("traffic_light_str", "רמזור");
        descriptions.Add("stop_sign", "תמרור עצור");
        descriptions.Add("yield_sign", "תמרור האט ותן זכות קדימה");
        descriptions.Add("no_enter_sign", "תמרור אין כניסה");
        descriptions.Add("crosswalk_sign", "תמרור מעבר חציה");
        descriptions.Add("bump_sign", "תמרור פסי האטה");
        descriptions.Add("square_sign", "כיכר תנועה");
        descriptions.Add("speed_limit_sign", "הגבלת מהירות");
        descriptions.Add("no_turn_back_sign", "תמרור האוסר פניית פרסה");
        descriptions.Add("blinker", "פנס איתות");
        descriptions.Add("continuous_line", "קו הפרדה רצוף");
    }

    private void ConjunctionsAddingValues()
    {
        conjunctions.Add("in", "ב");
        conjunctions.Add("level", "כמות");
        conjunctions.Add("level1", "נמוכה");
        conjunctions.Add("level2", "בינונית");
        conjunctions.Add("level3", "גבוהה");
        // conjunctions.Add();
        // conjunctions.Add();
        // conjunctions.Add();
        // conjunctions.Add();
        // conjunctions.Add();
        // conjunctions.Add();
        // conjunctions.Add();
    }

    private void TemplatesAddingValues()
    {
        templates.Add("subTitie1", "פירוט הערות וניקוד על החוקים עליהם נבחת:");
        templates.Add("subTitie2", "שיפורים אפשריים:");
        templates.Add("numMistakesRule", "כמות הפעמים בהם עברת על חוק זה הינו {0}.");
        templates.Add("scoreReduce1", "עבור כל מעבר על חוק זה הירידה בציון היא {0}, ובסך הכול {1}.");
        templates.Add("scoreReduce2", "עבור חוק זה הפירוט הוא אחת משלוש רמות, והרמה עבור מבחן זה היא {0}, והירידה בציון היא {1}.");
    }

    

    /*public struct RulesStr {
        static string traffic_light_str = "רמזור";  /// 1
        static string stop_sign = "תמרור עצור";  /// 2 - v
        static string yield_sign = "תמרור האט ותן זכות קדימה";  /// 3
        static string no_enter_sign = "תמרור אין כניסה";  /// 4
        //static string one_way_sign = "תמרור רחוב חד-סיטרי";  /// 5
        static string crosswalk_sign = "תמרור מעבר חציה";  /// 6
        static string bump_sign = "תמרור פסי האטה";  /// 7
        static string square_sign = "כיכר תנועה";  /// 8
        //static string red_white_sidewalk = "מדרכה צבועה אדום-לבן";  /// 9
        //static string red_yellow_sidewalk = "מדרכה צבועה אדום-צהוב";  /// 10
        static string speed_limit_sign = "הגבלת מהירות";  /// 11 - v
        static string no_turn_back_sign = "תמרור האוסר פניית פרסה";  /// 12 - v
        //string two_ways_sign = "תמרור רחוב דו-סיטרי";
        //string no_parking_sign = "תמרור האוסר על חניה";
        static string blinker = "פנס איתות"; /// v
        static string continuous_line = "קו הפרדה רצוף"; /// v


    }*/

    public void MakeOutput()
    {
        DescriptionsAddingValues();
        ConjunctionsAddingValues();
        TemplatesAddingValues();

        Dictionary<string, Logger.RuleObj> rules = Logger.Instance.rulesMistakes;
        string strOut = "";
        foreach(KeyValuePair<string, Logger.RuleObj> entry in rules)
        {
            string ruleStr;
            if (!descriptions.TryGetValue(entry.Key, out ruleStr))
            {
                Debug.Log("Error: this rule is not existing in this current test!");
                break;
            }
            strOut += ruleStr + ": " + String.Format(templates["numMistakesRule"], entry.Value.mistakes) + "\n";
        }
        Debug.Log(strOut);
        Debug.Log("פירוט");

        this.score = 100;
        this.summary = strOut;
    }

    public (int, string) GetOutput()
    {
        return (this.score, this.summary);
    }
}