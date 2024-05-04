using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1Scene : MonoBehaviour
{
    private List<System.Action> flowFunctionsChapter1 = new List<System.Action>();
    private List<System.Action> flowTransitionChapter1 = new List<System.Action>();
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    [Header("GameObject Image for Scene")]
    public Image imageScene;
    public Sprite spriteImageScene;
    private bool imageSceneFadeIn = false;
    private bool imageSceneFadeOut = false;

    [Header("Character Text")]
    private float defaultDelayText;
    public float delayTextTime = 0;
    public float fadeInText = 0;
    public float fadeOutText = 0;
    public int intCharacterText = 0;
    public int intFunctionNumbersNow = 0;
    public int intTransitionNumbersNow = 0;
    public Text middleText;
    // private Text characterText;
    private Text targetTextForTransition;
    
    [Header("Character Thoughts")]
    private Text thoughtText;
    
    [Header("Character Dialogue")]
    [SerializeField] float delayDialogue;
    private float targetDelay;
    [SerializeField] float minDelayDialogue = 3f;
    [SerializeField] float maxDelayDialogue = 5f;
    [SerializeField] float maxWordLengthofText = 20f;
    public bool isDialogue = false;
    public GameObject dialogueGO;
    public Text dialogueText;
    public string currentDialogue;
    public Text nameCharacterInDialogue;
    public string[] characterDialogue;
    public float delayAnimationDialogue = 0.01f;
    public bool textAnimationFinished = false;

    [Header("Memories Scene")]
    public string[] familyPictureString = 
    {
        "Alan: <i>Me alongside the best parents in the world.</i>"
    };
    public string[] certificateString =
    {
        "Alan: <i>I feel like I can do anything after I got this.</i>"
    };
    public string[] dialogueWithParents1 = 
    {
        "Alan: I never imagined this day would come so soon.",
        "Alan`s Dad: Don`t worry about your parents, we will do just fine here.",
        "Alan`s Mom: Not everyone could get this opportunity, so make sure you take advantage of this and enjoy.",
        "Alan`s Dad: I`ll send pocket money, so make sure to get a girlfriend, my boy.",
        "Alan: No, not interested.",
        "Alan`s Mom: Ara~",
        "Alan`s Mom: Your dad is just joking around Al, but I also do not prohibit you for having a girlfriend.",
        "Alan: My parents just being a parents",
        "Alan: Hehe…",
        "Alan: Anyway, thank you, Mom, Dad.",
        "Alan: Someday, I will do or bring something to make you guys proud of me.",
        "Alan`s Mom: Just so you know, we always love you whatever happens and wherever you are.",
        "Alan`s Dad: Yes boy, do whatever you think is right and achieve your dreams.",
        "Alan`s MoM: If anything goes south, our door is always open. Don’t forget."
    };

    public string[] dialogueWithParents2 = 
    {
        "Alan:  Goodbye, Mom. Goodbye, Dad.",
        "Alan`s Mom: Goodbye, my sweet boy. Take care.",
        "Alan`s Dad: Goodbye, son. We`ll be waiting."
    };
    //string jika player belum berinteraksi dengan objek foto, sertifikat, dan orang tua
    public string[] dialogueBeforeExitMemories = 
    {
        "Alan: <i>I think I must check everything before leaving.</i>"
    };

    public string[] dialogueAfterOpenDoor = 
    {
        "???: Hello…",
        "???: Alan, are you still alive?",
        "Alan: <i>Whose voice is that?</i>",
    };

    public bool haveTalkedToParents = false;
    public bool canOpenDoor = false;

    [Header("Wake Up Scane")]
    public string[] dialogueWakeUp = 
    {
        "???:   Looks like you're not dead, thankfully…",
        "Alan:  Who?",
        "Cindy: Oh, I'm Cindy. Nice to meet you, if I'm not wrong, Alan?",
        "Alan:  That's me, how do you know?",
        "Cindy: I took a glance of your ID Card.",
        "Cindy: Not on purpose though. It just dropped out of your wallet, for real. Peace…",
        "Cindy: By the way, if you're a 02 liner, then we're both 22. But I'm older, hehe…",
        "Cindy: Even though I'm an art student,",
        "Cindy: I think we could get along with each other.",
        "Alan:  I can't process everything she said.",
        "Alan:  Oh, I don't…",
        "Alan:  (Spouting water)",
        "Cindy: Wow, hold on there. Get up and throw your wet shirt, you could catch a cold.",
        "Cindy: This is not how a guy should talk to an older girl.",
        "Alan:  Why is she acting like she's so close with me?",
    };
    public Sprite ImageWakeUpSprite;

    [Header("Transition Scene")]
    public Image blackscreenImage;
    public float fadeInBlackscreen = 1f;
    public float fadeOutBlackscreen = 1f;
    private float delayFadeInOutTextInBlackscreen;
    [SerializeField] bool blackscreenFadeIn = false;
    [SerializeField] bool blackscreenFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        delayFadeInOutTextInBlackscreen = fadeInBlackscreen + fadeOutBlackscreen;
        delayTextTime = delayFadeInOutTextInBlackscreen;

        flowTransitionChapter1.Add(NothingHappend);
        flowTransitionChapter1.Add(() => TransitionCharacterText(targetTextForTransition));
        flowTransitionChapter1.Add(Dialogue);
        flowTransitionChapter1.Add(FadeInBlackscreenTransition);
        flowTransitionChapter1.Add(FadeOutBlackscreenTransition);
        flowTransitionChapter1.Add(FadeInImageSceneTransition);
        flowTransitionChapter1.Add(FadeOutImageSceneTransition);

        flowFunctionsChapter1.Add(NothingHappend);
        flowFunctionsChapter1.Add(DialogueWithCindyInBlackScreen);
        flowFunctionsChapter1.Add(GoWakeUp);
        flowFunctionsChapter1.Add(LookAtandFirstConversationWithCindy);
        flowFunctionsChapter1.Add(FinishedFirstConversation);
        flowFunctionsChapter1.Add(StandUpAfterConversationWithCindy);

        InitialGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(intFunctionNumbersNow != 0)
            flowFunctionsChapter1[intFunctionNumbersNow]();

        if(intTransitionNumbersNow != 0)
            flowTransitionChapter1[intTransitionNumbersNow]();
    }

    void NothingHappend()
    {
        //do nothing
    }

    void InitialGame()
    {
        //inisialisasi game
        blackscreenImage.gameObject.SetActive(true);
        blackscreenImage.color = new Color(0, 0, 0, 1);

        imageScene.gameObject.SetActive(false);
        imageScene.color = new Color(255, 255, 255, 0);

        intTransitionNumbersNow = 4;

        thoughtText = middleText;
        // characterText.text = characterDialogue[intCharacterText];

        // dialogueText = dial;
        // dialogueText.text = characterDialogue[intCharacterText];
    }

    void TransitionCharacterText(Text textTarget) // 1
    {
        delayTextTime -= Time.deltaTime;
        if (delayTextTime < defaultDelayText / 2)
        {
            textTarget.color = new Color(255, 255, 255, textTarget.color.a - Time.deltaTime / fadeInText);
            return;
        }
        textTarget.color = new Color(255, 255, 255, textTarget.color.a + Time.deltaTime / fadeOutText);
    }

    public void Dialogue() // 2
    {   
        // delayDialogue += Time.deltaTime;
        // if (delayDialogue > targetDelay)
        // {
        //     delayDialogue = 0;
            // if (intCharacterText < characterDialogue.Length - 1)
            // {
            //     intCharacterText++;
            //     // dialogueText.text = characterDialogue[intCharacterText];
            //     StartCoroutine(ShowText());
            //     CheckLengthWords();
            // }
            // else
            // {
            //     DefaultAfterDialogueTrigger();
            // }
        // }


        //menekan tombol panah kanan atau kiri untuk melanjutkan dialog
        // if (Input.GetKeyDown(KeyCode.X))
        //menggunakan click kiri mouse
        if (Input.GetMouseButtonDown(0) && textAnimationFinished)
        {
            // delayDialogue = 0;
            if (intCharacterText < characterDialogue.Length - 1)
            {
                textAnimationFinished = false;
                intCharacterText++;
                // dialogueText.text = characterDialogue[intCharacterText];
                SplitNameAndDialogue();
                StartCoroutine(ShowText());
                CheckLengthWords();
            }
            else
            {
                DefaultAfterDialogueTrigger();
            }
        }

        // if (Input.GetKeyDown(KeyCode.Z))
        //menggunakan click kanan mouse
        if (Input.GetMouseButtonDown(1) && textAnimationFinished)
        {
            if (intCharacterText > 0)
            {
                textAnimationFinished = false;
                // delayDialogue = 0;
                intCharacterText--;
                // dialogueText.text = characterDialogue[intCharacterText];
                SplitNameAndDialogue();
                StartCoroutine(ShowText());
                CheckLengthWords();
            }
        }
        //skip dialog
        // if (Input.GetKeyDown(KeyCode.C))
        //menggunakan click tengah mouse
        if (Input.GetMouseButtonDown(2) && textAnimationFinished)
        {
            textAnimationFinished = false;
            // delayDialogue = 0;
            DefaultAfterDialogueTrigger();
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= currentDialogue.Length; i++)
        {
            dialogueText.text = currentDialogue.Substring(0, i);
            yield return new WaitForSeconds(delayAnimationDialogue);
            if (i == currentDialogue.Length)
            {
                textAnimationFinished = true;
            }
        }
    }

    void SplitNameAndDialogue() //DelimiterDialogueText
    {
        string[] parts = characterDialogue[intCharacterText].Split(':');
        string name = parts[0].Trim();
        string dialogue = parts[1].Trim();

        nameCharacterInDialogue.text = name;
        currentDialogue = dialogue;
        dialogueText.text = currentDialogue;
    }

    void FadeInBlackscreenTransition() //3
    {
        blackscreenImage.color = new Color(0, 0, 0, blackscreenImage.color.a + Time.deltaTime / fadeInBlackscreen);
        if (blackscreenImage.color.a >= 1)
        {
            blackscreenImage.color = new Color(0, 0, 0, 1);
            blackscreenFadeIn = true;
        }
    }

    void FadeOutBlackscreenTransition() //4
    {
        blackscreenImage.color = new Color(0, 0, 0, blackscreenImage.color.a - Time.deltaTime / fadeOutBlackscreen);
        if (blackscreenImage.color.a <= 0)
        {
            blackscreenImage.color = new Color(0, 0, 0, 0);
            blackscreenFadeOut = true;
            blackscreenImage.gameObject.SetActive(false);
        }
    }

    void FadeInImageSceneTransition() //5
    {
        imageScene.color = new Color(255, 255, 255, imageScene.color.a + Time.deltaTime / fadeInBlackscreen);
        if (imageScene.color.a >= 1)
        {
            imageScene.color = new Color(255, 255, 255, 1);
            imageSceneFadeIn = true;
        }
    }

    void FadeOutImageSceneTransition() //6
    {
        imageScene.color = new Color(255, 255, 255, imageScene.color.a - Time.deltaTime / fadeOutBlackscreen);
        if (imageScene.color.a <= 0)
        {
            imageScene.color = new Color(255, 255, 255, 0);
            imageSceneFadeOut = true;
            imageScene.gameObject.SetActive(false);
        }
    }

    void DefaultAfterDialogueTrigger()
    {
        scriptPlayerMovement.canMove = true;
        isDialogue = false;
        dialogueGO.SetActive(false);
        intTransitionNumbersNow = 0;
        intCharacterText = 0;
    }

    void CheckLengthWords()
    {
        string[] words = characterDialogue[intCharacterText].Split(' ');
        if (words.Length > maxWordLengthofText)
            targetDelay = maxDelayDialogue;
        else
            targetDelay = minDelayDialogue;
    }

    void DefaultTriggerMechanism()
    {
        SplitNameAndDialogue();
        scriptPlayerMovement.canMove = false;
        scriptPlayerMovement.rb2D.velocity = Vector2.zero;
        intTransitionNumbersNow = 2;
        dialogueGO.SetActive(true);
        isDialogue = true;
        CheckLengthWords();
        StartCoroutine(ShowText());
    }
    public void TriggerObjectPhoto()
    {
        characterDialogue = familyPictureString;
        // dialogueText.text = characterDialogue[intCharacterText];
        DefaultTriggerMechanism();
    }

    public void TriggerObjectCertificate()
    {
        characterDialogue = certificateString;
        // dialogueText.text = characterDialogue[intCharacterText];
        DefaultTriggerMechanism();
    }

    public void TriggerObjectParent()
    {
        // DefaultTriggerMechanism();
        if (!haveTalkedToParents)
        {
            characterDialogue = dialogueWithParents1;
            haveTalkedToParents = true;
            canOpenDoor = true;
        }
        else
            characterDialogue = dialogueWithParents2;
        // dialogueText.text = characterDialogue[intCharacterText];
        DefaultTriggerMechanism();
    }

    public void TriggerObjectDoor()
    {
        if (canOpenDoor)
        {
            blackscreenImage.gameObject.SetActive(true);
            scriptPlayerMovement.canMove = false;
            intTransitionNumbersNow = 3;
            intFunctionNumbersNow = 1;
        }
        else
        {
            // DefaultTriggerMechanism();
            characterDialogue = dialogueBeforeExitMemories;
            // dialogueText.text = characterDialogue[intCharacterText];
            DefaultTriggerMechanism();
        }
    }

    void DialogueWithCindyInBlackScreen() // 1 = Scene after open door / dialogue in black screen with cindya
    {
        if (blackscreenFadeIn)
        {
            characterDialogue = dialogueAfterOpenDoor;
            // dialogueText.text = characterDialogue[intCharacterText];
            DefaultTriggerMechanism();
            intFunctionNumbersNow++;
            blackscreenFadeIn = false;
        }
    }

    void GoWakeUp() // 2 = Scene after dialogue with cindya in black screen
    {
        //do nothing
        if (isDialogue)
            return;

        imageScene.gameObject.SetActive(true);
        spriteImageScene = ImageWakeUpSprite;
        imageScene.sprite = spriteImageScene;
        
        intTransitionNumbersNow = 5;
        intFunctionNumbersNow++;
    }

    void LookAtandFirstConversationWithCindy()
    {
        if (imageSceneFadeIn)
        {
            characterDialogue = dialogueWakeUp;
            // dialogueText.text = characterDialogue[intCharacterText];
            DefaultTriggerMechanism();
            intFunctionNumbersNow++;
            imageSceneFadeIn = false;
        }
    }

    void FinishedFirstConversation()
    {
        if (isDialogue)
            return;
        intTransitionNumbersNow = 6;
        intFunctionNumbersNow++;
    }

    void StandUpAfterConversationWithCindy()
    {
        if (imageSceneFadeOut)
        {
            intTransitionNumbersNow = 0;
            intFunctionNumbersNow = 0;
            imageSceneFadeOut = false;
        }
    }
}
