using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningGameplay : MonoBehaviour
{
    private List<System.Action> functionList = new List<System.Action>();
    // private List<System.Action> funcGuideList = new List<System.Action>();
    public int runFunc = 1;
    // public int transitionGuideNow = 0;
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public GuideScript scriptGuide;

    [Header("Black Screen")]
    public GameObject blackScreenGO;
    public Image blackScreenImage;
    [SerializeField] float fadeOutBlackScreen = 5f; //time to fade out blackscreen
    // public bool canMove = false;

    [Header("Title Game")]
    public string[] titleChapter = 
    {
        "Chapter 1",
        "Chapter 2",
        "Chapter 3"
    };
    public string[] titleGame = 
    {
        "The Beginning",
        "The Middle",
        "The End"
    };
    public int chapterNowInt = 0;

    [Header("Title Game Fade In Out")]
    public GameObject titleGameGO;
    public Text titleGameText;
    public Text titleChapterText;
    public Image iconTitleGame;
    public float fadeInText = 3f;
    public float fadeOutText = 3f;

    // Start is called before the first frame update
    void Start()
    {
        blackScreenGO.SetActive(true);
        titleGameGO.SetActive(true);

        functionList.Add(NothingHappend);
        functionList.Add(BlackScreenFadeOut);
        functionList.Add(TitleGameFadeIn);
        functionList.Add(TitleGameFadeOut);
        
        titleChapterText.text = titleChapter[chapterNowInt];
        titleGameText.text = titleGame[chapterNowInt];
        
        titleGameText.color = new Color(255, 255, 255, 0);
        titleChapterText.color = new Color(255, 255, 255, 0);
        iconTitleGame.color = new Color(255, 255, 255, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (runFunc != 0)
            functionList[runFunc]();
    }

    void BlackScreenFadeOut()
    {
        blackScreenImage.color = new Color(0, 0, 0, blackScreenImage.color.a - Time.deltaTime / fadeOutBlackScreen);
        
        if (blackScreenImage.color.a < 0)
        {
            blackScreenImage.color = new Color(0, 0, 0, 0);
            blackScreenGO.SetActive(false);
            scriptPlayerMovement.canMove = true;
            // transitionGuideNow = 1;
            runFunc++;
            // runFunc = 0;
            scriptGuide.transitionGuideNow = 2;
        }
    }

    void TitleGameFadeIn()
    {
        titleGameText.color = new Color(255, 255, 255, titleGameText.color.a + Time.deltaTime / fadeInText);
        titleChapterText.color = new Color(255, 255, 255, titleChapterText.color.a + Time.deltaTime / fadeInText);
        iconTitleGame.color = new Color(255,255,255, iconTitleGame.color.a + Time.deltaTime / fadeInText);

        if (titleGameText.color.a >= 1)
            runFunc++;
    }

    void TitleGameFadeOut()
    {
        titleGameText.color = new Color(255, 255, 255, titleGameText.color.a - Time.deltaTime / fadeOutText);
        titleChapterText.color = new Color(255, 255, 255, titleChapterText.color.a - Time.deltaTime / fadeOutText);
        iconTitleGame.color = new Color(255,255,255, iconTitleGame.color.a - Time.deltaTime / fadeOutText);

        if (titleGameText.color.a <= 0)
        {
            runFunc = 0;
            //disabled script
            this.enabled = false;
        }
    }

    void NothingHappend()
    {
        //do nothing
    }
}
