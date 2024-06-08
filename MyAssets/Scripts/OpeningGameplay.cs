using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningGameplay : MonoBehaviour
{
    private List<System.Action> functionList = new List<System.Action>();
    public int runFunc = 1;

    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public GuideScript scriptGuide;
    private Chapter0Scene scriptChapter0Scene;
    private Chapter1Scene scriptChapter1Scene;
    private Chapter2Scene scriptChapter2Scene;
    public AnimRotationPlayer scriptAnimRotationPlayer;

    [Header("Title Game")]
    public Image bgTitleGameImage;
    public Image titleGameImage;
    public float fadeInImage = 1.5f;
    public float fadeOutImage = 3f;

    // Start is called before the first frame update
    void Start()
    {
        scriptChapter0Scene = GetComponent<Chapter0Scene>();
        scriptChapter1Scene = GetComponent<Chapter1Scene>();
        scriptChapter2Scene = GetComponent<Chapter2Scene>();

        bgTitleGameImage.gameObject.SetActive(true);

        functionList.Add(NothingHappend);
        functionList.Add(TitleGameOnlyFadeIn);
        functionList.Add(TitleGameFadeOut);
        
        bgTitleGameImage.color = new Color(0, 0, 0, 1);
        titleGameImage.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (runFunc != 0)
            functionList[runFunc]();
    }

    void TitleGameOnlyFadeIn()
    {
        titleGameImage.color = new Color(255, 255, 255, titleGameImage.color.a + Time.deltaTime / fadeInImage);

        if (titleGameImage.color.a >= 1)
        {
            titleGameImage.color = new Color(255, 255, 255, 1);
            runFunc = 2;
        }
    }

    void TitleGameFadeOut()
    {
        bgTitleGameImage.color = new Color(0, 0, 0, bgTitleGameImage.color.a - Time.deltaTime / fadeOutImage);
        titleGameImage.color = new Color(255, 255, 255, titleGameImage.color.a - Time.deltaTime / fadeOutImage);

        if (bgTitleGameImage.color.a <= 0)
        {
            bgTitleGameImage.color = new Color(0, 0, 0, 0);
            titleGameImage.color = new Color(255, 255, 255, 0);
            bgTitleGameImage.gameObject.SetActive(false);

            // scriptPlayerMovement.canMove = true;
            // scriptGuide.transitionGuideNow = 2;

            if (scriptChapter0Scene != null)
            {
                scriptChapter0Scene.GoToOpeningAlanThoughts();
                // scriptGuide.transitionGuideNow = 2;
            }
            
            if (scriptChapter1Scene != null)
                scriptPlayerMovement.canMove = true;
                // scriptChapter1Scene.GoToOpeningAlanThoughts();
                
            if (scriptChapter2Scene != null)
            {
                scriptChapter2Scene.OpeningGameplayChapter2();
            }
            
            runFunc = 0;
            this.enabled = false;
        }
    }

    void NothingHappend()
    {
        return;
    }
}
