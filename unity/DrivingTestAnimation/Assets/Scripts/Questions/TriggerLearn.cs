using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TriggerLearn : MonoBehaviour
{

    [Serializable]
    public struct SpriteRule
    {
        public string rule;
        public Sprite sprite;
    }

    [SerializeField]
    List<SpriteRule> spriteRuleList;


    [SerializeField]
    List<KeyValuePair<string, Sprite>> ssss;

    [SerializeField]
    private GameObject windowLearn;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text explanation;

    public static TriggerLearn Instance;

    void Awake() {
        Instance = this;
    }

    public void Trigger(string rule)
    {
        Debug.Log("learn trigger: " + rule);
        //rule="stop_sign";
        
        if(spriteRuleList.Exists(SpriteRule => SpriteRule.rule == rule))
        {
            SpriteRule spriteRuleStruct = spriteRuleList.Find(SpriteRule => SpriteRule.rule == rule);
            Sprite newSprite = spriteRuleStruct.sprite;
            windowLearn.GetComponent<Image>().sprite = newSprite;
            ExpObj exp;
            if (!GameManager.Instance.explanationMap.TryGetValue(rule, out exp))
            {
                Debug.Log("Error: this rule is not existing in this current test!");
                return;
            }
            title.text = exp.title;
            explanation.text = exp.explanation;
            ContrallerManager.Instance.OnPause();
            windowLearn.SetActive(true);
        } else
        {
            Debug.Log("Error: this rule is not existing!");
        }
    }
}
