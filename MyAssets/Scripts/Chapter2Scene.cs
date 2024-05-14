using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2Scene : MonoBehaviour
{
    public List<System.Action> flowFunctionsChapter2 = new List<System.Action>();
    public List<System.Action> flowTransitionChapter2 = new List<System.Action>();
    [Header("Scripts")]
    public TransitionFunction scriptTransitionFunction;
    public DialogueChapter2 dialogueChapter2;
    [Header("GameObjects")]
    public GameObject fakeAlanGO;
    public GameObject playerGO;
    public GameObject guideGO;
    public GameObject parentGO;
    public Text newGuideText;

    [Header("Limbo - Level 1")]
    private string[] dialogueLimbo1;
    private string[] dialogueLimbo2Her;
    private string[] dialogueLimbo3No;
    private string[] dialogueLimbo4Tomorrow;
    public float maxDistanceToInteract = 5f;

    [Header("After Limbo - Level 2")]
    private string[] dialogueWakeUpAfterLimbo;
    public Sprite wakeUpAfterLimboSprite;
    private string[] dialogueBlackScreenAfterLimbo;
    private string[] thoughtsAfterWakeUp;
    public Sprite cindyHuggingAlanSprite;
    private string[] dialogueCindyHuggingAlan;
    private string[] cindyThoughtsYellowText;
    private string[] dialogueAlanCallMom;

    [Header("Home - Level 3")]
    private string[] dialogueAlanHome;
    private string[] alanLastThoughts;

    // Start is called before the first frame update
    void Start()
    {
        flowTransitionChapter2.Add(NothingHappend); //0
        flowTransitionChapter2.Add(() => scriptTransitionFunction.TransitionCharacterText(scriptTransitionFunction.targetTextForTransition)); //1
        flowTransitionChapter2.Add(scriptTransitionFunction.Dialogue); //2
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeInBlackscreenTransition); //3
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeOutBlackscreenTransition); //4
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeInImageSceneTransition); //5
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeOutImageSceneTransition); //6
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeInWhiteScreenTransition); //7
        flowTransitionChapter2.Add(scriptTransitionFunction.FadeOutWhiteScreenTransition); //8
        flowTransitionChapter2.Add(scriptTransitionFunction.TransitionTMPCharacter); //8

        flowFunctionsChapter2.Add(NothingHappend); // 0
        flowFunctionsChapter2.Add(AlanApproachesFakeAlan); // 1
        flowFunctionsChapter2.Add(DialogueLimbo1); // 2
        flowFunctionsChapter2.Add(GoToDialogueLimbo2Her); // 3
        flowFunctionsChapter2.Add(DialogueLimbo2No); // 4
        flowFunctionsChapter2.Add(GoToDialogueLimbo3No); // 5
        flowFunctionsChapter2.Add(DialogueLimbo3No); // 6
        flowFunctionsChapter2.Add(GoToDialogueLimbo4Tomorrow); // 7
        flowFunctionsChapter2.Add(DialogueLimbo4Tomorrow); // 8
        flowFunctionsChapter2.Add(FadeOutAfterDialogue); // 9
        flowFunctionsChapter2.Add(WakeUpAfterLimbo); // 10
        flowFunctionsChapter2.Add(DialogueAfterWakeUp); // 11
        flowFunctionsChapter2.Add(FadeInBlackScreenAfterDialogueWakeUp); // 12
        // flowFunctionsChapter2.Add(FadeOutImageSceneAfterWakeUp); // 13
        // flowFunctionsChapter2.Add(FadeOutWhiteScreenAfterWakeUp); // 14
        flowFunctionsChapter2.Add(DialogueBlackScreenAfterLimbo); // 15 // 13
        flowFunctionsChapter2.Add(FadeOutBlackScreenToWhiteScreen); // 16 // 14
        flowFunctionsChapter2.Add(AlanThoughtsWhiteScreen); // 17 // 15
        flowFunctionsChapter2.Add(FadeOutWhiteGoToHugging); // 18 // 16
        flowFunctionsChapter2.Add(DialogueIsHugging); // 19 // 17
        // flowFunctionsChapter2.Add(FadeOutImageSceneAfterThoughts); // 17
        // flowFunctionsChapter2.Add(DialogueIsHugging); // 18
        flowFunctionsChapter2.Add(FadeInWhiteAfterHug); // 19 // 18
        flowFunctionsChapter2.Add(CindyThoughtsYellowText); // 20 // 19
        flowFunctionsChapter2.Add(BlackScreenAfterCindyThoughts); // 21 // 20
        flowFunctionsChapter2.Add(AfterAlanCallMom); // 21 // 21
        flowFunctionsChapter2.Add(FadeOutCallMom); // 22 // 22
        flowFunctionsChapter2.Add(GoToHome); // 23 // 23
        flowFunctionsChapter2.Add(DialogueToLastThoughts); // 24 // 24
        flowFunctionsChapter2.Add(LastThoughts); // 25 // 25
        flowFunctionsChapter2.Add(CreditGame); // 26 // 26

        scriptTransitionFunction.intFunctionNumbersNow = 1;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        scriptTransitionFunction.isChapter1 = false;
        guideGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(scriptTransitionFunction.intFunctionNumbersNow != 0)
            flowFunctionsChapter2[scriptTransitionFunction.intFunctionNumbersNow]();

        if(scriptTransitionFunction.intTransitionNumbersNow != 0)
            flowTransitionChapter2[scriptTransitionFunction.intTransitionNumbersNow]();

        InitialAddDialogue();
    }

    void NothingHappend()
    {

    }

    void InitialAddDialogue()
    {
        //Level 1 - Limbo
        dialogueLimbo1 = dialogueChapter2.dialogueLimbo1;
        dialogueLimbo2Her = dialogueChapter2.dialogueLimbo2Her;
        dialogueLimbo3No = dialogueChapter2.dialogueLimbo3No;
        dialogueLimbo4Tomorrow = dialogueChapter2.dialogueLimbo4Tomorrow;

        //Level 2 - After Limbo
        dialogueWakeUpAfterLimbo = dialogueChapter2.dialogueWakeUpAfterLimbo;
        dialogueBlackScreenAfterLimbo = dialogueChapter2.dialogueBlackScreenAfterLimbo;
        thoughtsAfterWakeUp = dialogueChapter2.thoughtsAfterWakeUp;
        dialogueCindyHuggingAlan = dialogueChapter2.dialogueCindyHuggingAlan;
        cindyThoughtsYellowText = dialogueChapter2.cindyThoughtsYellowText;
        dialogueAlanCallMom = dialogueChapter2.dialogueAlanCallMom;

        //Level 3 - Home
        dialogueAlanHome = dialogueChapter2.dialogueAlanHome;
        alanLastThoughts = dialogueChapter2.alanLastThoughts;
    }

    void AlanApproachesFakeAlan() // 1
    {
        if (Vector3.Distance(playerGO.transform.position, fakeAlanGO.transform.position) <= maxDistanceToInteract)
        {
            playerGO.GetComponent<PlayerMovement>().canMove = false;

            scriptTransitionFunction.characterDialogue = dialogueLimbo1;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
        }
    }

    void DialogueLimbo1() // 2
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        guideGO.SetActive(true);
        newGuideText.text = "Her";
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void GoToDialogueLimbo2Her() // 3
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            guideGO.SetActive(false);
            scriptTransitionFunction.characterDialogue = dialogueLimbo2Her;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
        }
    }

    void DialogueLimbo2No() // 4
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        guideGO.SetActive(true);
        newGuideText.text = "No";
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void GoToDialogueLimbo3No() // 5
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            guideGO.SetActive(false);
            scriptTransitionFunction.characterDialogue = dialogueLimbo3No;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
        }
    
    }

    void DialogueLimbo3No() // 6
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        guideGO.SetActive(true);
        newGuideText.text = "Tomorrow";
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void GoToDialogueLimbo4Tomorrow() // 7
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            guideGO.SetActive(false);
            scriptTransitionFunction.characterDialogue = dialogueLimbo4Tomorrow;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
        }
    }

    void DialogueLimbo4Tomorrow() // 8
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 7;
        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true);
    }

    void FadeOutAfterDialogue() // 9
    {
        if (!scriptTransitionFunction.whiteScreenFadeIn)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 8;
        scriptTransitionFunction.whiteScreenFadeIn = false;

        scriptTransitionFunction.imageScene.gameObject.SetActive(true);
        scriptTransitionFunction.imageScene.sprite = wakeUpAfterLimboSprite;
        scriptTransitionFunction.imageScene.color = new Color(255, 255, 255, 1);
    }

    void WakeUpAfterLimbo() // 10
    {
        if (scriptTransitionFunction.whiteScreenFadeOut)
        {
            scriptTransitionFunction.whiteScreenFadeOut = false;
            scriptTransitionFunction.characterDialogue = dialogueWakeUpAfterLimbo;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
        }
    }

    void DialogueAfterWakeUp() // 11
    {
        if(scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true); 
    }

    void FadeInBlackScreenAfterDialogueWakeUp() // 12
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 2;
        scriptTransitionFunction.blackscreenFadeIn = false;

        scriptTransitionFunction.imageScene.gameObject.SetActive(false);
        scriptTransitionFunction.imageScene.color = new Color(255, 255, 255, 0f);

        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(false);
        scriptTransitionFunction.imageWhiteScreen.color = new Color(255, 255, 255, 0);

        scriptTransitionFunction.characterDialogue = dialogueBlackScreenAfterLimbo;
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    void DialogueBlackScreenAfterLimbo() // 13
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;

        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true);
        scriptTransitionFunction.imageWhiteScreen.color = new Color(255, 255, 255, 1);
    }

    void FadeOutBlackScreenToWhiteScreen() // 14
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 1;
        scriptTransitionFunction.blackscreenFadeOut = false;

        scriptTransitionFunction.characterThoughts = thoughtsAfterWakeUp;
        scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.isThought = true;
        
        scriptTransitionFunction.middleText.gameObject.SetActive(true);
        scriptTransitionFunction.valueWhiteOrBlack = 0; // 0 is white
        scriptTransitionFunction.valueBlackOrYellow = 0;
    }

    void AlanThoughtsWhiteScreen() // 15
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 8;

        scriptTransitionFunction.imageScene.gameObject.SetActive(true);
        scriptTransitionFunction.imageScene.sprite = cindyHuggingAlanSprite;

        // scriptTransitionFunction.imageSceneFadeIn = true;
        scriptTransitionFunction.imageScene.color = new Color(255, 255, 255, 1);
    }

    void FadeOutWhiteGoToHugging() // 16
    {
        if (!scriptTransitionFunction.whiteScreenFadeOut)
            return;
        scriptTransitionFunction.whiteScreenFadeOut = false;
        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(false);

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 2;
        scriptTransitionFunction.characterDialogue = dialogueCindyHuggingAlan;
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    void DialogueIsHugging() // 17
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 7;
        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true); 
        // scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true); 
    }

    void FadeInWhiteAfterHug() // 18
    {
        if (!scriptTransitionFunction.whiteScreenFadeIn)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 9;
        scriptTransitionFunction.whiteScreenFadeIn = false;

        scriptTransitionFunction.characterThoughts = cindyThoughtsYellowText;
        scriptTransitionFunction.textWithOutline.gameObject.SetActive(true);
        scriptTransitionFunction.textWithOutline.color = new Color(255, 255, 0, 0);
        scriptTransitionFunction.textWithOutline.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        // scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        // scriptTransitionFunction.valueBlackOrYellow = 0; // 0 is yellow
        // scriptTransitionFunction.valueWhiteOrBlack = 255; // 255 is white 
        scriptTransitionFunction.isThought = true;
        // scriptTransitionFunction.middleText.gameObject.SetActive(true);
    }

    void CindyThoughtsYellowText() // 19
    {
        if (scriptTransitionFunction.isThought)
            return;
        
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 8;
        scriptTransitionFunction.blackscreenImage2.gameObject.SetActive(true); 
        scriptTransitionFunction.blackscreenImage2.color = new Color(0, 0, 0, 1);
        scriptTransitionFunction.textWithOutline.gameObject.SetActive(false);
    }

    void BlackScreenAfterCindyThoughts() // 20
    {
        if (!scriptTransitionFunction.whiteScreenFadeOut)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 2;
        scriptTransitionFunction.blackscreenFadeOut = false;

        scriptTransitionFunction.characterDialogue = dialogueAlanCallMom;
        scriptTransitionFunction.DefaultTriggerMechanism();

        playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);

        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

        scriptTransitionFunction.imageScene.gameObject.SetActive(false); 
        scriptTransitionFunction.blackscreenImage2.gameObject.SetActive(false);
    }

    void AfterAlanCallMom()
    {
        if (scriptTransitionFunction.isDialogue) //21
            return;
        
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
    }

    void FadeOutCallMom()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
        scriptTransitionFunction.blackscreenFadeOut = false;
    }

    void GoToHome()
    {
        if (Vector3.Distance(playerGO.transform.position, parentGO.transform.position) <= maxDistanceToInteract)
        {
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2; // dialogue

            scriptTransitionFunction.characterDialogue = dialogueAlanHome;
            scriptTransitionFunction.DefaultTriggerMechanism();
        }
    }

    void DialogueToLastThoughts()
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 7;

        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true);
    }

    void LastThoughts()
    {
        if (!scriptTransitionFunction.whiteScreenFadeIn)
            return;
        
        scriptTransitionFunction.whiteScreenFadeIn = false;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 1;
    
        scriptTransitionFunction.characterThoughts = alanLastThoughts;
        scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.isThought = true;
        scriptTransitionFunction.middleText.gameObject.SetActive(true);
        scriptTransitionFunction.valueWhiteOrBlack = 0; // 0 is white
        scriptTransitionFunction.valueBlackOrYellow = 0;
    }

    void CreditGame()
    {
        if (scriptTransitionFunction.isThought)
            return;

        PlayerPrefs.DeleteKey("ChapterNow");

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }
}
