using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionCharacters : MonoBehaviour
{
    // public Sprite[] alanExpressions;
    // public Sprite[] cindyExpressions;
    // public Sprite[] alanMomExpressions;
    // public Sprite[] alanDadExpressions;
    // public Sprite[] alanBossExpressions;
    [System.Serializable]
    public class ExpressionSprite
    {
        public string expressionName;
        public Sprite sprite;
    }
    [Header("Alan Expressions")]
    // Publik List untuk diisi di Inspector
    public List<ExpressionSprite> alanExpressionList;
    // Dictionary untuk menyimpan ekspresi dan sprite
    private Dictionary<string, Sprite> alanExpressionSprites;

    [Header("Cindy Expressions")]
    public List<ExpressionSprite> cindyExpressionList;
    private Dictionary<string, Sprite> cindyExpressionSprites;

    [Header("Alan Mom Expressions")]
    public List<ExpressionSprite> alanMomExpressionList;
    private Dictionary<string, Sprite> alanMomExpressionSprites;

    [Header("Alan Dad Expressions")]
    public List<ExpressionSprite> alanDadExpressionList;
    private Dictionary<string, Sprite> alanDadExpressionSprites;

    [Header("Alan Boss Expressions")]
    public List<ExpressionSprite> alanBossExpressionList;
    private Dictionary<string, Sprite> alanBossExpressionSprites;
    [Header("Chaca Expressions")]
    public Dictionary<string, Dictionary<string, Sprite>> characterExpressions = new Dictionary<string, Dictionary<string, Sprite>>();

    private void Start()
    {
        alanExpressionSprites = new Dictionary<string, Sprite>();
        foreach (ExpressionSprite alanExpression in alanExpressionList)
        {
            alanExpressionSprites.Add(alanExpression.expressionName, alanExpression.sprite);
        }

        cindyExpressionSprites = new Dictionary<string, Sprite>();
        foreach (ExpressionSprite cindyExpression in cindyExpressionList)
        {
            cindyExpressionSprites.Add(cindyExpression.expressionName, cindyExpression.sprite);
        }

        alanMomExpressionSprites = new Dictionary<string, Sprite>();
        foreach (ExpressionSprite alanMomExpression in alanMomExpressionList)
        {
            alanMomExpressionSprites.Add(alanMomExpression.expressionName, alanMomExpression.sprite);
        }

        alanDadExpressionSprites = new Dictionary<string, Sprite>();
        foreach (ExpressionSprite alanDadExpression in alanDadExpressionList)
        {
            alanDadExpressionSprites.Add(alanDadExpression.expressionName, alanDadExpression.sprite);
        }

        alanBossExpressionSprites = new Dictionary<string, Sprite>();
        foreach (ExpressionSprite alanBossExpression in alanBossExpressionList)
        {
            alanBossExpressionSprites.Add(alanBossExpression.expressionName, alanBossExpression.sprite);
        }

        // characterExpressions 
        characterExpressions.Add("Alan", alanExpressionSprites);
        characterExpressions.Add("Cindy", cindyExpressionSprites);
        characterExpressions.Add("Alan's Mom", alanMomExpressionSprites);
        characterExpressions.Add("Alan's Dad", alanDadExpressionSprites);
        characterExpressions.Add("Alan's Boss", alanBossExpressionSprites);
    }
}
