using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransitionFunction : MonoBehaviour
{
    public bool isChapter1 = false;
    [Header("Scripts")]
    public ExpressionCharacters scriptExpressionCharacters;
    // public PlayerMovement scriptPlayerMovement;
    public Chapter1Scene scriptChapter1Scene;
    [Header("Character GameObject")]
    public GameObject playerGO;
    public GameObject cindyGO;
    
    [Header("Character Text")]
    public TextMeshProUGUI textWithOutline;
    private float defaultDelayText;
    public float delayTextTime;
    public float fadeInText = 1.5f;
    public float fadeOutText = 1.5f;
    public int intCharacterText = 0;
    public int intFunctionNumbersNow = 0;
    public int intTransitionNumbersNow = 0;
    public Text middleText;
    public Text targetTextForTransition;

    [Header("Character Dialogue")]
    public int valueBlackOrYellow = 255;
    public int valueWhiteOrBlack = 255;
    [SerializeField] float delayDialogue;
    private float targetDelay;
    [SerializeField] float minDelayDialogue = 3f;
    [SerializeField] float maxDelayDialogue = 5f;
    [SerializeField] float maxWordLengthofText = 20f;
    public bool isDialogue = false;
    public GameObject dialogueGO;
    public GameObject panelDialogueOnlyGO;
    public Text dialogueText;
    public string currentDialogue;
    public Text nameCharacterInDialogue;
    public string[] characterDialogue;
    public float delayAnimationDialogue = 0.01f;
    public bool textAnimationFinished = false;
    public Image expressionCharacterImage;
    public GameObject nextIcon;
    public GameObject ObjectImageGO;
    public GameObject bgObjectImageGO;
    public Image ObjectImageSprite;
    public Text textNameObject;
    public GameObject nextPreviousSkipDialogueGO;
    public bool mouseEnter = false;
    public bool isInteractObjectImage = false;

    [Header("Hide UI")]
    public GameObject topBlackBackgroundGO;
    public GameObject bottomBlackBackgroundGO;
    public bool isHideUIEnable = false;
    public bool isHidingUI = false;
    public float speedHideUI = 10f;
    public GameObject transparantBtnEnableUI;
    public float defaultPosYTopBlackBackground;
    public float defaultPosYBottomBlackBackground;

    [Header("Auto Next Dialogue")]
    public Image imageAutoDialogue;
    public bool enableDelayDialogue = false;
    public Sprite iconAutoDialogue;
    public Sprite iconStopAutoDialogue;
    public Text textAutoDialogue;

    [Header("Character Thoughts")]
    public bool isThought = false;
    public Text thoughtText;
    public string[] characterThoughts;

    // [Header("Expression Character")]
    // Dictionary<string, Sprite[]> characterExpressions = new Dictionary<string, Sprite[]>();

    [Header("Transition Scene")]
    public Image blackscreenImage;
    public Image blackscreenImage2;
    public float fadeInBlackscreen = 1f;
    public float fadeOutBlackscreen = 1f;
    private float delayFadeInOutTextInBlackscreen;
    public bool blackscreenFadeIn = false;
    public bool blackscreenFadeOut = false;

    [Header("GameObject Image for Scene")]
    public Image imageScene;
    // public Sprite spriteImageScene;
    public bool imageSceneFadeIn = false;
    public bool imageSceneFadeOut = false;
    [Header("White Screen Transition")]
    public bool whiteScreenFadeIn = false;
    public bool whiteScreenFadeOut = false;
    public Image imageWhiteScreen;
    public float fadeInWhiteScreen = 1f;
    public float fadeOutWhiteScreen = 1f;
    public float delayFadeInOutTextInWhiteScreen;

    [Header("Audio Transition")]
    public AudioSource UIAudioSource;
    public AudioClip UIClipAudio;
    public float durationFadeInAudio = 1f;
    public float durationFadeOutAudio = 1f;


    void Start()
    {
        defaultPosYBottomBlackBackground = bottomBlackBackgroundGO.transform.position.y;
        defaultPosYTopBlackBackground = topBlackBackgroundGO.transform.position.y;

        middleText.color = new Color(255, 255, 255, 0);

        delayFadeInOutTextInBlackscreen = fadeInBlackscreen + fadeOutBlackscreen;
        delayFadeInOutTextInWhiteScreen = fadeInWhiteScreen + fadeOutWhiteScreen;
        // delayTextTime = delayFadeInOutTextInBlackscreen;

        delayTextTime = fadeInText + fadeOutText;
        defaultDelayText = fadeInText + fadeOutText;

        defaultDelayText = delayTextTime;
        // AddExpressionCharacter();
        InitialGame();
    }

    void InitialGame()
    {
        //inisialisasi game
        // blackscreenImage.gameObject.SetActive(true);
        // blackscreenImage.color = new Color(0, 0, 0, 1);

        imageScene.gameObject.SetActive(false);
        imageScene.color = new Color(255, 255, 255, 0);

        // intTransitionNumbersNow = 4;
        // intFunctionNumbersNow = 1;

        thoughtText = middleText;
        targetTextForTransition = thoughtText;
    }

    // public void AddExpressionCharacter()
    // {
    //     // characterExpressions.Add("Alan", scriptExpressionCharacters.alanExpressions);
    //     // characterExpressions.Add("Cindy", scriptExpressionCharacters.cindyExpressions);
    //     // characterExpressions.Add("???", scriptExpressionCharacters.cindyExpressions);
    //     // characterExpressions.Add("Alan's Dad", scriptExpressionCharacters.alanDadExpressions);
    //     // characterExpressions.Add("Alan's Mom", scriptExpressionCharacters.alanMomExpressions);
    //     // characterExpressions.Add("Alan's Boss", scriptExpressionCharacters.alanBossExpressions);
    // }

    public void TransitionCharacterText(Text textTarget) // 1
    {
        delayTextTime -= Time.deltaTime;
        // if (delayTextTime < defaultDelayText / 2)
        //     textTarget.color = new Color(valueWhiteOrBlack, valueWhiteOrBlack, valueBlackOrYellow, textTarget.color.a - Time.deltaTime / fadeInText);
        // else
        //     textTarget.color = new Color(valueWhiteOrBlack, valueWhiteOrBlack, valueBlackOrYellow, textTarget.color.a + Time.deltaTime / fadeOutText);
        float alpha = textTarget.color.a;

        if (delayTextTime > defaultDelayText * 2 / 3)
            alpha = Mathf.Lerp(0, 1, (defaultDelayText - delayTextTime) / fadeInText * 1.5f);
        else if (delayTextTime > defaultDelayText / 3)
            alpha = 1;
        else if (delayTextTime <= defaultDelayText / 3)
            alpha = Mathf.Lerp(1, 0, (defaultDelayText / 3 - delayTextTime) / fadeOutText * 1.5f);
        textTarget.color = new Color(valueWhiteOrBlack, valueWhiteOrBlack, valueBlackOrYellow, alpha);

        if (delayTextTime >= 0)
            return;

        delayTextTime = defaultDelayText;
        if (intCharacterText >= characterThoughts.Length - 1)
        {
            intTransitionNumbersNow = 0;
            intCharacterText = 0;
            isThought = false;

            return;
        }
        intCharacterText++;
        thoughtText.text = characterThoughts[intCharacterText];
    }
    public void Dialogue() // 2
    {   
        if (isHideUIEnable || isHidingUI)
            return;

        if (enableDelayDialogue && textAnimationFinished)
        {
            delayDialogue += Time.deltaTime;
            if (delayDialogue > targetDelay)
            {
                delayDialogue = 0;
                if (intCharacterText < characterDialogue.Length - 1)
                {
                    intCharacterText++;
                    // dialogueText.text = characterDialogue[intCharacterText];
                    SplitNameExpressionDialogue();
                    StartCoroutine(ShowText());
                    CheckLengthWords();
                }
                else
                {
                    DefaultAfterDialogueTrigger();
                }
            }
        }

        // if (!mouseEnter Input.GetMouseButtonDown(0) ||  && textAnimationFinished)
        if (!mouseEnter && Input.GetKeyDown(KeyCode.Space) && textAnimationFinished || !mouseEnter && Input.GetMouseButtonDown(0) && textAnimationFinished)
        {
            delayDialogue = 0;
            textAnimationFinished = false;
            UIAudioSource.PlayOneShot(UIClipAudio);
            if (intCharacterText < characterDialogue.Length - 1)
            {
                
                nextIcon.SetActive(false);
                intCharacterText++;
                // dialogueText.text = characterDialogue[intCharacterText];
                SplitNameExpressionDialogue();
                StartCoroutine(ShowText());
                CheckLengthWords();
            }
            else
            {
                DefaultAfterDialogueTrigger();
                isInteractObjectImage = false;
                nextIcon.SetActive(false);
            }
        }

        // if (!mouseEnter && Input.GetKeyDown(KeyCode.Z) && textAnimationFinished)
        if (!mouseEnter && Input.GetMouseButtonDown(1) && textAnimationFinished)
        {
            if (intCharacterText > 0)
            {
                mouseEnter = false;
                textAnimationFinished = false;
                nextIcon.SetActive(false);
                delayDialogue = 0;
                intCharacterText--;
                // dialogueText.text = characterDialogue[intCharacterText];
                SplitNameExpressionDialogue();
                StartCoroutine(ShowText());
                CheckLengthWords();
            }
        }

        // if (!mouseEnter Input.GetKeyDown(KeyCode.C) && textAnimationFinished)
        if (!mouseEnter && Input.GetMouseButtonDown(2) && textAnimationFinished)
        // if (Input.GetKeyDown(KeyCode.C) && textAnimationFinished)
        {
            mouseEnter = false;
            //stop Coroutine
            StopAllCoroutines();
            textAnimationFinished = false;
            nextIcon.SetActive(false);
            delayDialogue = 0;
            DefaultAfterDialogueTrigger();
            isInteractObjectImage = false;
        }
    }
    public IEnumerator ShowText()
    {
        for (int i = 0; i <= currentDialogue.Length; i++)
        {
            dialogueText.text = currentDialogue.Substring(0, i);
            yield return new WaitForSeconds(delayAnimationDialogue);
            if (i == currentDialogue.Length)
            {
                textAnimationFinished = true;
                nextIcon.SetActive(true);
            }
        }
    }
    public void SplitNameExpressionDialogue() //DelimiterDialogueText
    {
        string[] parts = characterDialogue[intCharacterText].Split(':');
        string name = parts[0].Trim(); //name character
        string expression = parts[1].Trim(); //expression
        string dialogue = parts[2].Trim(); //dialogue
        if (parts.Length > 3)
        {
            string fontStyle = parts[3].Trim(); //font style
            ChangeFontStyle(fontStyle);  
        }
        else
            dialogueText.fontStyle = FontStyle.Normal;
        
        nameCharacterInDialogue.text = name;
        currentDialogue = dialogue;
        dialogueText.text = currentDialogue;

              
        ChangeExpressionCharacterDialogue(name, expression);
    }
    void ChangeFontStyle(string fontStyle)
    {
        if (fontStyle == "italic")
            dialogueText.fontStyle = FontStyle.Italic;
        else if (fontStyle == "bold")
            dialogueText.fontStyle = FontStyle.Bold;
        else if (fontStyle == "bold_italic")
            dialogueText.fontStyle = FontStyle.BoldAndItalic;
        else
            dialogueText.fontStyle = FontStyle.Normal;
    }
    void ChangeExpressionCharacterDialogue(string characterName, string expressionName)
    {
        Debug.Log(characterName + " " + expressionName);
        if (scriptExpressionCharacters.characterExpressions.ContainsKey(characterName))
        {
            expressionCharacterImage.sprite = scriptExpressionCharacters.characterExpressions[characterName][expressionName];
        }
        else
            expressionCharacterImage.sprite = null;

        // if (characterExpressions.ContainsKey(name))
        // {
        //     expressionCharacterImage.sprite = characterExpressions[name][0];
        //     if (expression == "Flat")
        //         expressionCharacterImage.sprite = characterExpressions[name][0];
        //     else if (expression == "Smile")
        //         expressionCharacterImage.sprite = characterExpressions[name][1];
        //     else if (expression == "Sad")
        //         expressionCharacterImage.sprite = characterExpressions[name][2];
        // }
        // if (name == "Alan")
        //     if (expression == "Flat")
        //         expressionCharacterImage.sprite = scriptExpressionCharacters.alanExpressions[0];
        // if (name == "Cindy" || name == "???")
        //     else if (expression == "Smile")
        //         expressionCharacterImage.sprite = scriptExpressionCharacters.cindyExpressions[1];
    }
    public void FadeInBlackscreenTransition() //3
    {
        // if (blackscreenImage.gameObject.activeSelf == false)
        //     blackscreenImage.gameObject.SetActive(true);

        blackscreenImage.color = new Color(0, 0, 0, blackscreenImage.color.a + Time.deltaTime / fadeInBlackscreen);
        if (blackscreenImage.color.a >= 1)
        {
            blackscreenImage.color = new Color(0, 0, 0, 1);
            blackscreenFadeIn = true;
        }
    }

    public void FadeOutBlackscreenTransition() //4
    {
        blackscreenImage.color = new Color(0, 0, 0, blackscreenImage.color.a - Time.deltaTime / fadeOutBlackscreen);
        if (blackscreenImage.color.a <= 0)
        {
            blackscreenImage.color = new Color(0, 0, 0, 0);
            blackscreenFadeOut = true;
            blackscreenImage.gameObject.SetActive(false);
        }
    }

    public void FadeInImageSceneTransition() //5
    {
        imageScene.color = new Color(255, 255, 255, imageScene.color.a + Time.deltaTime / fadeInBlackscreen);
        if (imageScene.color.a >= 1)
        {
            imageScene.color = new Color(255, 255, 255, 1);
            imageSceneFadeIn = true;
        }
    }

    public void FadeOutImageSceneTransition() //6
    {
        imageScene.color = new Color(255, 255, 255, imageScene.color.a - Time.deltaTime / fadeOutBlackscreen);
        if (imageScene.color.a <= 0)
        {
            imageScene.color = new Color(255, 255, 255, 0);
            imageSceneFadeOut = true;
            imageScene.gameObject.SetActive(false);
        }
    }

    public void FadeInWhiteScreenTransition() //7
    {
        imageWhiteScreen.color = new Color(255, 255, 255, imageWhiteScreen.color.a + Time.deltaTime / fadeInWhiteScreen);
        if (imageWhiteScreen.color.a >= 1)
        {
            imageWhiteScreen.color = new Color(255, 255, 255, 1);
            whiteScreenFadeIn = true;
        }
    }

    public void FadeOutWhiteScreenTransition() //8
    {
        imageWhiteScreen.color = new Color(255, 255, 255, imageWhiteScreen.color.a - Time.deltaTime / fadeOutWhiteScreen);
        if (imageWhiteScreen.color.a <= 0)
        {
            imageWhiteScreen.color = new Color(255, 255, 255, 0);
            whiteScreenFadeOut = true;
            imageWhiteScreen.gameObject.SetActive(false);
        }
    }

    public void TransitionTMPCharacter() // 9
    {
        delayTextTime -= Time.deltaTime;
        if (delayTextTime < defaultDelayText / 3)
            textWithOutline.color = new Color(255, 255, 0, textWithOutline.color.a - Time.deltaTime / fadeInText * 1.5f);
        else if (delayTextTime > defaultDelayText * 2 / 3)
            textWithOutline.color = new Color(255, 255, 0, textWithOutline.color.a + Time.deltaTime / fadeOutText * 1.5f);
        else
            textWithOutline.color = new Color(255, 255, 0, textWithOutline.color.a); // No change


        if (delayTextTime > 0)
            return;

        delayTextTime = defaultDelayText;
        if (intCharacterText >= characterThoughts.Length - 1)
        {
            intTransitionNumbersNow = 0;
            intCharacterText = 0;
            isThought = false;
            return;
        }

        intCharacterText++;
        textWithOutline.text = characterThoughts[intCharacterText];
    }

    public void TransitionsPerSentenceDialogue(Text textTarget) // 1
    {
        delayTextTime -= Time.deltaTime;
        float alpha = textTarget.color.a;

        if (delayTextTime > defaultDelayText * 2 / 3)
            alpha = Mathf.Lerp(0, 1, (defaultDelayText - delayTextTime) / fadeInText * 1.5f);
        else if (delayTextTime > defaultDelayText / 3)
            alpha = 1;
        else if (delayTextTime <= defaultDelayText / 3)
            alpha = Mathf.Lerp(1, 0, (defaultDelayText / 3 - delayTextTime) / fadeOutText * 1.5f);
        textTarget.color = new Color(valueWhiteOrBlack, valueWhiteOrBlack, valueBlackOrYellow, alpha);

        if (delayTextTime >= 0)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !textAnimationFinished)
        {
            delayTextTime = defaultDelayText;
            if (intCharacterText < characterThoughts.Length - 1)
            {
                intCharacterText++;
                textTarget.text = characterThoughts[intCharacterText];
            }
            else
            {
                intTransitionNumbersNow = 0;
                intCharacterText = 0;
                isThought = false;
            }
        }
    }

    public void DefaultTriggerMechanism()
    {
        playerGO.GetComponent<PlayerMovement>().animator.SetTrigger("Idle");
        playerGO.GetComponent<PlayerMovement>().animator.ResetTrigger("Walk");
        
        SplitNameExpressionDialogue();
        // scriptPlayerMovement.canMove = false;
        // scriptPlayerMovement.rb2D.velocity = Vector2.zero;
        playerGO.GetComponent<PlayerMovement>().canMove = false;
        playerGO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        intTransitionNumbersNow = 2;
        dialogueGO.SetActive(true);
        isDialogue = true;
        CheckLengthWords();
        StartCoroutine(ShowText());
    }

    void DefaultAfterDialogueTrigger()
    {
        // scriptPlayerMovement.canMove = true;
        playerGO.GetComponent<PlayerMovement>().canMove = true;
        isDialogue = false;
        dialogueGO.SetActive(false);
        intTransitionNumbersNow = 0;
        intCharacterText = 0;

        if (isChapter1)
        {
            if (scriptChapter1Scene.isDialogueWithCindy)
                scriptChapter1Scene.isDialogueWithCindy = false;
        }


        if (ObjectImageGO.activeSelf)
        {
            bgObjectImageGO.SetActive(false);
            ObjectImageGO.SetActive(false);
            textNameObject.text = "";
        }
    }

    public void CheckLengthWords()
    {
        string[] words = characterDialogue[intCharacterText].Split(' ');
        if (words.Length > maxWordLengthofText)
            targetDelay = maxDelayDialogue;
        else
            targetDelay = minDelayDialogue;
    }

    IEnumerator EnableUIDialogue()
    {
        RectTransform rectTransform = topBlackBackgroundGO.GetComponent<RectTransform>();
        if (isInteractObjectImage)
                ObjectImageGO.SetActive(true);
        while (rectTransform.anchoredPosition.y >= -50)
        {
            isHidingUI = true;

            float newPosY = topBlackBackgroundGO.transform.position.y - speedHideUI * Time.deltaTime;
            topBlackBackgroundGO.transform.position = new Vector2(topBlackBackgroundGO.transform.position.x, newPosY);

            float newPosY2 = bottomBlackBackgroundGO.transform.position.y + speedHideUI * 2.5f * Time.deltaTime;
            bottomBlackBackgroundGO.transform.position = new Vector2(bottomBlackBackgroundGO.transform.position.x, newPosY2);
            yield return null;
        }

        delayDialogue = 0;
        isHideUIEnable = false;
        isHidingUI = false;
        // transparantBtnEnableUI.SetActive(false);
        nextPreviousSkipDialogueGO.SetActive(true);
        bottomBlackBackgroundGO.transform.position = new Vector2(bottomBlackBackgroundGO.transform.position.x, defaultPosYBottomBlackBackground);
        topBlackBackgroundGO.transform.position = new Vector2(topBlackBackgroundGO.transform.position.x, defaultPosYTopBlackBackground);
        if (isInteractObjectImage)
            bgObjectImageGO.SetActive(true);
    }

    IEnumerator DisableUIDialogue()
    {
        RectTransform rectTransform = topBlackBackgroundGO.GetComponent<RectTransform>();
        while (rectTransform.anchoredPosition.y <= 50)
        {
            isHidingUI = true;
            
            float newPosY = topBlackBackgroundGO.transform.position.y + speedHideUI * Time.deltaTime;
            topBlackBackgroundGO.transform.position = new Vector2(topBlackBackgroundGO.transform.position.x, newPosY);

            float newPosY2 = bottomBlackBackgroundGO.transform.position.y - speedHideUI * 2.5f * Time.deltaTime;
            bottomBlackBackgroundGO.transform.position = new Vector2(bottomBlackBackgroundGO.transform.position.x, newPosY2);
            
            yield return null;
        }
        if (isInteractObjectImage)
            ObjectImageGO.SetActive(false);

        delayDialogue = 0;
        isHideUIEnable = true;
        isHidingUI = false;
        panelDialogueOnlyGO.SetActive(false);
        nextPreviousSkipDialogueGO.SetActive(false);
        transparantBtnEnableUI.SetActive(true);
        bgObjectImageGO.SetActive(false);
    }

    public void OpenDialogue()
    {
        panelDialogueOnlyGO.SetActive(true);
        transparantBtnEnableUI.SetActive(false);
        StartCoroutine(EnableUIDialogue());
    }

    public void CloseDialogue()
    {
        StartCoroutine(DisableUIDialogue());
    }

    public void AutoNextDialogue()
    {
        if (enableDelayDialogue)
        {
            delayDialogue = 0;
            enableDelayDialogue = false;
            imageAutoDialogue.sprite = iconAutoDialogue;
            textAutoDialogue.text = "Auto";
        }
        else
        {
            delayDialogue = 0;
            enableDelayDialogue = true;
            imageAutoDialogue.sprite = iconStopAutoDialogue;
            textAutoDialogue.text = "Playing";
        }
    }

    // public void ButtonNextDialogue()
    // {
    //     if (textAnimationFinished)
    //     {
    //         delayDialogue = 0;
    //         textAnimationFinished = false;
    //         if (intCharacterText < characterDialogue.Length - 1)
    //         {
    //             nextIcon.SetActive(false);
    //             intCharacterText++;
    //             // dialogueText.text = characterDialogue[intCharacterText];
    //             SplitNameExpressionDialogue();
    //             StartCoroutine(ShowText());
    //             CheckLengthWords();
    //         }
    //         else
    //             DefaultAfterDialogueTrigger();
    //     }
    // }
    public void MouseEnterToAutoOrHide()
    {
        mouseEnter = true;
    }

    public void MouseExitToAutoOrHide()
    {
        mouseEnter = false;
    }

    public IEnumerator FadeInAudio(AudioSource audioSource, AudioClip audioClip, float targetVolume)
    {
        audioSource.volume = 0;
        audioSource.clip = audioClip;
        audioSource.Play();
        float currentTime = 0;
        while (audioSource.volume < targetVolume)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, currentTime / durationFadeInAudio);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    public IEnumerator FadeOutAudio(AudioSource audioSource, float volumeNow, string status="Stop")
    {
        float currentTime = 0;
        while (audioSource.volume > 0)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volumeNow, 0, currentTime / durationFadeOutAudio);
            yield return null;
        }
        audioSource.volume = 0;
        if (status == "Stop")
            audioSource.Stop();
        else if (status == "Pause")
            audioSource.Pause();
        audioSource.volume = volumeNow;
    }
}
