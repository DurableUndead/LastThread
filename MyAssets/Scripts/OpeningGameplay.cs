using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningGameplay : MonoBehaviour
{
    private List<System.Action> functionList = new List<System.Action>();
    public int runFunc = 0;

    [Header("Black Screen")]
    public GameObject blackScreenGO;
    public Image blackscreen;
    [SerializeField] float fadeOutBlackScreen = 5f; //time to fade out blackscreen
    public bool canPlayGame = false;

    [Header("Title Game")]
    public string[] titleChapter = new string[3];
    public string[] titleGame = new string[3];
    public int chapterNowInt = 0;

    [Header("Title Game Fade In Out")]
    public GameObject titleGameGO;
    public Text titleGameText;
    public Text titleChapterText;
    public Image iconTitleGame;
    public float fadeInText = 3f;
    public float fadeOutText = 3f;

    [Header("Guide Gameobjects")]
    public float fadeInGuide = 1f;
    public Image guideImages;
    public Text guideText;
    public Image IconA;
    public Image IconD;
    public Image bgA, bgB;

    // Start is called before the first frame update
    void Start()
    {
        blackScreenGO.SetActive(true);
        titleGameGO.SetActive(true);

        functionList.Add(BlackScreenFadeOut);
        functionList.Add(TitleGameFadeIn);
        functionList.Add(TitleGameFadeOut);
        functionList.Add(NothingHappend);

        // Set Title Game
        titleChapter[0] = "Chapter 1";
        titleChapter[1] = "Chapter 2";
        titleChapter[2] = "Chapter 3";

        titleGame[0] = "The Beginning";
        titleGame[1] = "The Middle";
        titleGame[2] = "The End";

        titleChapterText.text = titleChapter[chapterNowInt];
        titleGameText.text = titleGame[chapterNowInt];
        
        titleGameText.color = new Color(255, 255, 255, 0);
        titleChapterText.color = new Color(255, 255, 255, 0);
        iconTitleGame.color = new Color(255, 255, 255, 0);

        guideImages.color = new Color(255, 255, 255, 0);
        guideText.color = new Color(0, 0, 0, 0);
        IconA.color = new Color(255, 255, 255, 0);
        IconD.color = new Color(255, 255, 255, 0);
        bgA.color = new Color(255, 255, 255, 0);
        bgB.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        functionList[runFunc]();
    }

    void BlackScreenFadeOut()
    {
        blackscreen.color = new Color(0, 0, 0, blackscreen.color.a - Time.deltaTime / fadeOutBlackScreen);
        
        if (blackscreen.color.a < 0)
        {
            blackScreenGO.SetActive(false);
            canPlayGame = true;
            runFunc++;
        }
    }

    void TitleGameFadeIn()
    {
        titleGameText.color = new Color(255, 255, 255, titleGameText.color.a + Time.deltaTime / fadeInText);
        titleChapterText.color = new Color(255, 255, 255, titleChapterText.color.a + Time.deltaTime / fadeInText);
        iconTitleGame.color = new Color(255,255,255, iconTitleGame.color.a + Time.deltaTime / fadeInText);

        //Guide Image transition
        guideImages.color = new Color(255, 255, 255, guideImages.color.a + Time.deltaTime / fadeInGuide);
        guideText.color = new Color(0, 0, 0, guideText.color.a + Time.deltaTime / fadeInGuide);
        IconA.color = new Color(255, 255, 255, IconA.color.a + Time.deltaTime / fadeInGuide);
        IconD.color = new Color(255, 255, 255, IconD.color.a + Time.deltaTime / fadeInGuide);
        bgA.color = new Color(255, 255, 255, bgA.color.a + Time.deltaTime / fadeInGuide);
        bgB.color = new Color(255, 255, 255, bgB.color.a + Time.deltaTime / fadeInGuide);

        if (titleGameText.color.a >= 1)
        {
            runFunc++;
        }
    }

    void TitleGameFadeOut()
    {
        titleGameText.color = new Color(255, 255, 255, titleGameText.color.a - Time.deltaTime / fadeOutText);
        titleChapterText.color = new Color(255, 255, 255, titleChapterText.color.a - Time.deltaTime / fadeOutText);
        iconTitleGame.color = new Color(255,255,255, iconTitleGame.color.a - Time.deltaTime / fadeOutText);

        if (titleGameText.color.a <= 0)
        {
            runFunc++;
            //disabled script
            this.enabled = false;
        }
    }

    void NothingHappend()
    {
        //do nothing
    }
}
