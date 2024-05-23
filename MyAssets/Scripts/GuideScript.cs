using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Guide Movement [A/D]")]
    public Image iconA;
    public Image iconD;
    
    [Header("Guide Jump [Space]")]
    public GameObject guideJumpGO;
    public Image iconJumpImage;

    [Header("Guide Interaction [E]")]
    public Text guideText;
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

        iconA.color = new Color(255, 255, 255, 0);
        iconD.color = new Color(255, 255, 255, 0);


        if (SceneManager.GetActiveScene().name != "Chapter0")
            return;
        iconJumpImage.color = new Color(255, 255, 255, 0);
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
        iconA.color = new Color(255, 255, 255, iconA.color.a + Time.deltaTime / fadeInGuide);
        iconD.color = new Color(255, 255, 255, iconD.color.a + Time.deltaTime / fadeInGuide);

        if (iconA.color.a >= 1)
        {
            transitionGuideNow = 1;
            iconA.color = new Color(255, 255, 255, 1);
            iconD.color = new Color(255, 255, 255, 1);
        }
    }

    void FadeOutGuide()
    {
        if (Input.GetKeyDown(KeyCode.A) && canFadeOutGuideL)
        {
            StartCoroutine(FadeOutGuideLeft());
            canFadeOutGuideL = false;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && canFadeOutGuideR)
        {
            StartCoroutine(FadeOutGuideRight());
            canFadeOutGuideR = false;
        }

        if (endTransitionR && endTransitionL)
            transitionGuideNow = 0;
    }

    IEnumerator FadeOutGuideRight()
    {
        while (iconD.color.a > 0)
        {
            iconD.color = new Color(255, 255, 255, iconD.color.a - Time.deltaTime / fadeOutGuide);
            yield return null;
        }
        iconD.color = new Color(255, 255, 255, 0);
        enableTransitionRight = false;
        endTransitionR = true;
    }

    IEnumerator FadeOutGuideLeft()
    {
        while (iconA.color.a > 0)
        {
            iconA.color = new Color(255, 255, 255, iconA.color.a - Time.deltaTime / fadeOutGuide);
            yield return null;
        }
        iconA.color = new Color(255, 255, 255, 0);
        enableTransitionLeft = false;
        endTransitionL = true;
        transparentWallGuide.SetActive(false);
    }


    void FadeInGuideJump()
    {
        iconJumpImage.color = new Color(255, 255, 255, iconJumpImage.color.a + Time.deltaTime / fadeInGuide);

        if (iconJumpImage.color.a >= 1)
        {
            transitionGuideNow = 0;
            isFadeInGuideJump = true;
        }
    }

    public IEnumerator IEFadeInGuideJump()
    {
        isFadeInGuideJump = false;
        while (iconJumpImage.color.a < 1)
        {
            iconJumpImage.color = new Color(255, 255, 255, iconJumpImage.color.a + Time.deltaTime / fadeInGuide);
            yield return null;
        }
        isFadeInGuideJump = true;
    }

    public IEnumerator IEFadeOutGuideJump()
    {
        isFadeInGuideJump = false;
        while (iconJumpImage.color.a > 0)
        {
            iconJumpImage.color = new Color(255, 255, 255, iconJumpImage.color.a - Time.deltaTime / fadeOutGuide);
            yield return null;
        }
        isFadeInGuideJump = true;
    }
}
