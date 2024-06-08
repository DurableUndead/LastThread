using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuideScript : MonoBehaviour
{
    public bool isChapter0 = true;
    private List<System.Action> functionList = new List<System.Action>();
    public PlayerMovement scriptPlayerMovement;
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

    [Header("Guide Gameobjects")]
    public GameObject guideGO;

    [Header("Guide Movement [A/D]")]
    public Image iconA;
    public Image iconD;
    
    [Header("Guide Jump [Space]")]
    public GameObject guideJumpGO;
    public Image iconJumpImage;
    public bool isFadeInGuideJump;
    public bool isFadeOutGuideJump;
    public bool canFadeInGuideJump = true;
    public bool canFadeOutGuideJump = true;

    [Header("Guide Interaction [E]")]
    public Text guideText;
    public Image guideInteractionImage;
    public Image iconE;
    public Image bgE;

    [Header("Guide Wake Up [Space]")]
    public bool isFadeInWakeUp;
    public bool isFadeOutWakeUp;
    public SpriteRenderer iconStandOrWakeUp;

    // Start is called before the first frame update
    void Start()
    {
        functionList.Add(NothingHappend);
        functionList.Add(FadeOutGuide);
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

    public IEnumerator FadeInGuide()
    {
        iconA.color = new Color(255, 255, 255, 0);
        iconD.color = new Color(255, 255, 255, 0);

        float currentColorA = iconA.color.a;
        float currentColorD = iconD.color.a;

        iconA.gameObject.SetActive(true);
        iconD.gameObject.SetActive(true);

        while (iconA.color.a < 1)
        {
            currentColorA += Time.deltaTime / fadeInGuide;
            currentColorD += Time.deltaTime / fadeInGuide;
            iconA.color = new Color(255, 255, 255, currentColorA);
            iconD.color = new Color(255, 255, 255, currentColorD);
            yield return null;
        }

        transitionGuideNow = 1;
        scriptPlayerMovement.canMove = true;
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
        {
            transitionGuideNow = 0;
            this.enabled = false;
        }
    }

    public void IfPlayerDoesNotPressAD()
    {
        if (canFadeOutGuideL)
        {
            StartCoroutine(FadeOutGuideLeft());
            canFadeOutGuideL = false;
        }
        if (canFadeOutGuideR)
        {
            StartCoroutine(FadeOutGuideRight());
            canFadeOutGuideR = false;
        }
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

    public IEnumerator IEFadeInGuideJump()
    {
        iconJumpImage.color = new Color(255, 255, 255, 0);
        guideJumpGO.SetActive(false);
        isFadeInGuideJump = true;
        while (iconJumpImage.color.a < 1)
        {
            iconJumpImage.color = new Color(255, 255, 255, iconJumpImage.color.a + Time.deltaTime / fadeInGuide);
            yield return null;
        }
        iconJumpImage.color = new Color(255, 255, 255, 1);
        isFadeInGuideJump = false;
    }

    public IEnumerator IEFadeOutGuideJump()
    {
        isFadeOutGuideJump = true;
        iconJumpImage.color = new Color(255, 255, 255, 1);
        while (iconJumpImage.color.a > 0)
        {
            iconJumpImage.color = new Color(255, 255, 255, iconJumpImage.color.a - Time.deltaTime / fadeOutGuide);
            yield return null;
        }
        iconJumpImage.color = new Color(255, 255, 255, 0);
        isFadeOutGuideJump = true;
    }

    public IEnumerator IEFadeInGuideWakeUp()
    {
        iconStandOrWakeUp.gameObject.SetActive(true);
        iconStandOrWakeUp.color = new Color(255, 255, 255, 0);
        isFadeInWakeUp = false;
        float currentColorA = iconStandOrWakeUp.color.a;

        while (currentColorA < 1)
        {
            // iconStandOrWakeUp.color = new Color(255, 255, 255, currentColorA + Time.deltaTime / fadeInGuide);
            currentColorA += Time.deltaTime / fadeInGuide;
            iconStandOrWakeUp.color = new Color(255, 255, 255, currentColorA);
            yield return null;
        }
        isFadeInWakeUp = true;
    }

    public IEnumerator IEFadeOutGuideWakeUp()
    {
        isFadeOutWakeUp = false;
        float currentColorA = iconStandOrWakeUp.color.a;
        while (currentColorA > 0)
        {
            // iconStandOrWakeUp.color = new Color(255, 255, 255, iconStandOrWakeUp.color.a - Time.deltaTime / fadeOutGuide);
            currentColorA -= Time.deltaTime / fadeOutGuide;
            iconStandOrWakeUp.color = new Color(255, 255, 255, currentColorA);
            yield return null;
        }
        isFadeOutWakeUp = true;
        iconStandOrWakeUp.gameObject.SetActive(false);
    }
}
