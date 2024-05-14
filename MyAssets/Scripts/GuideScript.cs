using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideScript : MonoBehaviour
{
    public bool isChapter0 = true;
    private List<System.Action> functionList = new List<System.Action>();
    [Header("Guide Config")]
    public int transitionGuideNow = 0;
    public float fadeInGuide = 1f;
    public float fadeOutGuide = 0.5f;
    private bool canFadeOutGuideR = true;
    private bool canFadeOutGuideL = true; 
    private bool endTransitionL = false;
    private bool endTransitionR = false;
    public GameObject transparentWallGuide;
    public bool enableTransitionRight = false;
    public bool enableTransitionLeft = false;
    public bool isFadeInGuideJump;

    [Header("Guide Gameobjects")]
    public GameObject guideGO;
    public Image guideImages;
    public Text guideText;
    public Image IconA;
    public Image bgA;
    public Image IconD;
    public Image bgD;
    public GameObject guideJumpGO;
    public Image guideImagesJump;
    public Text guideTextJump;
    public Image IconJump;
    public Image bgJump;

    [Header("Guide Interaction [E]")]
    public Image guideInteractionImage;
    public Image iconE;
    public Image bgE;

    // Start is called before the first frame update
    void Start()
    {
        functionList.Add(NothingHappend);
        functionList.Add(FadeOutGuide);
        functionList.Add(FadeInGuide);
        functionList.Add(FadeInGuideJump);

        guideImages.color = new Color(255, 255, 255, 0);
        guideText.color = new Color(0, 0, 0, 0);
        IconA.color = new Color(255, 255, 255, 0);
        IconD.color = new Color(255, 255, 255, 0);
        bgA.color = new Color(255, 255, 255, 0);
        bgD.color = new Color(255, 255, 255, 0);


        if (!isChapter0)
            return;
        guideImagesJump.color = new Color(255, 255, 255, 0);
        guideTextJump.color = new Color(0, 0, 0, 0);
        IconJump.color = new Color(255, 255, 255, 0);
        bgJump.color = new Color(255, 255, 255, 0);
        guideJumpGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionGuideNow != 0)
            functionList[transitionGuideNow]();
    }

    void NothingHappend()
    {
        // Nothing Happend
    }

    void FadeInGuide()
    {
        //Guide Image transition
        guideImages.color = new Color(255, 255, 255, guideImages.color.a + Time.deltaTime / fadeInGuide);
        guideText.color = new Color(0, 0, 0, guideText.color.a + Time.deltaTime / fadeInGuide);
        IconA.color = new Color(255, 255, 255, IconA.color.a + Time.deltaTime / fadeInGuide);
        IconD.color = new Color(255, 255, 255, IconD.color.a + Time.deltaTime / fadeInGuide);
        bgA.color = new Color(255, 255, 255, bgA.color.a + Time.deltaTime / fadeInGuide);
        bgD.color = new Color(255, 255, 255, bgD.color.a + Time.deltaTime / fadeInGuide);

        if (guideImages.color.a >= 1)
            transitionGuideNow = 1;
    }

    void FadeOutGuide()
    {
        if (Input.GetKeyDown(KeyCode.A) && canFadeOutGuideL)
        {
            enableTransitionLeft = true;
            canFadeOutGuideL = false;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && canFadeOutGuideR)
        {
            enableTransitionRight = true;
            canFadeOutGuideR = false;
        }

        if(enableTransitionRight)
            FadeOutGuideRight();

        if(enableTransitionLeft)
            FadeOutGuideLeft();

        if (endTransitionR && endTransitionL)
            FadeOutGuideTitle();
    }

    void FadeOutGuideTitle()
    {
        guideImages.color = new Color(255, 255, 255, guideImages.color.a - Time.deltaTime / fadeOutGuide);
        guideText.color = new Color(0, 0, 0, guideText.color.a - Time.deltaTime / fadeOutGuide);
        
        if (guideImages.color.a <= 0)
        {
            transitionGuideNow = 0;
            // this.enabled = false;
        }
    }

    void FadeOutGuideRight()
    {
        IconD.color = new Color(255, 255, 255, IconD.color.a - Time.deltaTime / fadeOutGuide);
        bgD.color = new Color(255, 255, 255, bgD.color.a - Time.deltaTime / fadeOutGuide);

        if (IconD.color.a <= 0)
        {
            IconD.color = new Color(255, 255, 255, 0);
            bgD.color = new Color(255, 255, 255, 0);
            enableTransitionRight = false;
            endTransitionR = true;
        }
    }

    void FadeOutGuideLeft()
    {
        IconA.color = new Color(255, 255, 255, IconA.color.a - Time.deltaTime / fadeOutGuide);
        bgA.color = new Color(255, 255, 255, bgA.color.a - Time.deltaTime / fadeOutGuide);

        if (IconA.color.a <= 0)
        {
            IconA.color = new Color(255, 255, 255, 0);
            bgA.color = new Color(255, 255, 255, 0);
            enableTransitionLeft = false;
            endTransitionL = true;
            transparentWallGuide.SetActive(false);
        }
    }

    void FadeInGuideJump()
    {
        guideImagesJump.color = new Color(255, 255, 255, guideImagesJump.color.a + Time.deltaTime / fadeInGuide);
        guideTextJump.color = new Color(0, 0, 0, guideTextJump.color.a + Time.deltaTime / fadeInGuide);
        IconJump.color = new Color(255, 255, 255, IconJump.color.a + Time.deltaTime / fadeInGuide);
        bgJump.color = new Color(255, 255, 255, bgJump.color.a + Time.deltaTime / fadeInGuide);

        if (guideImagesJump.color.a >= 1)
        {
            transitionGuideNow = 0;
            isFadeInGuideJump = true;
        }
            // transitionGuideNow = 4;
    }

    public void FadeInGuideInteraction()
    {
        guideGO.SetActive(true);
        guideImages.color = new Color(255, 255, 255, 1);
        guideText.color = new Color(0, 0, 0, 1);
        iconE.color = new Color(255, 255, 255, 1);
        bgE.color = new Color(255, 255, 255, 1);
        guideText.text = "Press [E] to interact";
    }

    public void FadeOutGuideInteraction()
    {
        guideGO.SetActive(false);
        guideImages.color = new Color(255, 255, 255, 0);
        guideText.color = new Color(0, 0, 0, 0);
        iconE.color = new Color(255, 255, 255, 0);
        bgE.color = new Color(255, 255, 255, 0);
        guideText.text = "Press [A/D] to Move";
    }
}
