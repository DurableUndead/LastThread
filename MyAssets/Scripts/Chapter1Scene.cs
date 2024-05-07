using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class Chapter1Scene : MonoBehaviour
{
    private List<System.Action> flowFunctionsChapter1 = new List<System.Action>();
    private List<System.Action> flowTransitionChapter1 = new List<System.Action>();
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public ExpressionCharacters scriptExpressionCharacters;
    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("GameObjects")]
    public GameObject playerGO;
    public GameObject cindyGO;
    public GameObject homeAlanGO;
    public GameObject riverSideTreesGO;
    public GameObject GuideGO;
    public GameObject minigameTreeGO;
    public GameObject wallMinigameTreeGO;
    public GameObject round1TreeGO;
    public GameObject round2TreeGO;
    public GameObject parallaxBackgroundGO;
    public GameObject centerMiniGameGO;

    [Header("GameObject Image for Scene")]
    public Image imageScene;
    public Sprite spriteImageScene;
    private bool imageSceneFadeIn = false;
    private bool imageSceneFadeOut = false;

    [Header("Character Text")]
    private float defaultDelayText;
    public float delayTextTime;
    public float fadeInText = 1.5f;
    public float fadeOutText = 1.5f;
    public int intCharacterText = 0;
    public int intFunctionNumbersNow = 0;
    public int intTransitionNumbersNow = 0;
    public Text middleText;
    public Text targetTextForTransition;
    // private Text characterText;
    [Header("Character Thoughts")]
    public bool isThought = false;
    private Text thoughtText;
    public string[] characterThoughts;
    
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
    public Image expressionCharacterImage;
    public GameObject nextIcon;
    [Header("Character Name and Expression")]
    Dictionary<string, Sprite[]> characterExpressions = new Dictionary<string, Sprite[]>();


    [Header("Memories Scene")]
    public string[] familyPictureString = 
    {
        "Alan: Smile : Me alongside the best parents in the world.:italic"
    };
    public string[] certificateString =
    {
        "Alan: Smile : I feel like I can do anything after I got this.:italic"
    };
    public string[] dialogueWithParents1 = 
    {
        "Alan: Smile : I never imagined this day would come so soon :normal",
        "Alan's Dad: Flat : Don't worry about your parents, we will do just fine here :normal",
        "Alan's Mom: Smile : Not everyone could get this opportunity, so make sure you take advantage of this and enjoy :normal",
        "Alan's Dad: Flat : I'll send pocket money, so make sure to get a girlfriend, my boy :normal",
        "Alan: Sad : No, not interested :normal",
        "Alan's Mom: Smile : Ara~",
        "Alan's Mom: Sad : Your dad is just joking around Al, but I also do not prohibit you for having a girlfriend :normal",
        "Alan: Flat : My parents just being a parents",
        "Alan: Smile : Hehe…",
        "Alan: Sad : Anyway, thank you, Mom, Dad :normal",
        "Alan: Smile : Someday, I will do or bring something to make you guys proud of me :normal",
        "Alan's Mom: Sad : Just so you know, we always love you whatever happens and wherever you are :normal",
        "Alan's Dad: Flat : Yes boy, do whatever you think is right and achieve your dreams :normal",
        "Alan's MoM: Flat : If anything goes south, our door is always open. Don't forget!. :normal",
    };

    public string[] dialogueWithParents2 = 
    {
        "Alan: Smile :Goodbye, Mom. Goodbye, Dad.:normal",
        "Alan's Mom: Smile :Goodbye, my sweet boy. Take care.:normal",
        "Alan's Dad: Smile :Goodbye, son. We'll be waiting.:normal",
    };
    //string jika player belum berinteraksi dengan objek foto, sertifikat, dan orang tua
    public string[] dialogueBeforeExitMemories = 
    {
        "Alan: Flat : I think I must check everything before leaving.:italic"
    };

    public string[] dialogueAfterOpenDoor = 
    {
        "???: Smile :Hello… :normal",
        "???: Sad :Alan, are you still alive? :normal",
        "Alan: Flat :Whose voice is that? :normal",
    };

    public bool haveTalkedToParents = false;
    public bool canOpenDoor = false;

    [Header("Wake Up Scane")]
    [SerializeField] public string[] dialogueWakeUp = 
    {
        "???: Flat :Looks like you're not dead, thankfully… :normal",
        "Alan: Sad :Who? :normal",
        "Cindy: Smile :Oh, I'm Cindy. Nice to meet you, if I'm not wrong, Alan? :normal",
        "Alan: Flat :That's me, how do you know? :normal",
        "Cindy: Sad :I took a glance of your ID Card. :normal",
        "Cindy: Smile :Not on purpose though. It just dropped out of your wallet, for real. Peace… :normal",
        "Cindy: Sad :By the way, if you're a 02 liner, then we're both 22. But I'm older, hehe… :normal",
        "Cindy: Smile :Even though I'm an art student,",
        "Cindy: Flat :I think we could get along with each other. :normal",
        "Alan: Flat :I can't process everything she said. :normal",
        "Alan: Sad :Oh, I don't… :normal",
        "Alan: Smile :(Spouting water) :normal",
        "Cindy: Smile :Wow, hold on there. Get up and throw your wet shirt, you could catch a cold. :normal",
        "Cindy: Flat :This is not how a guy should talk to an older girl. :normal",
        "Alan: Flat :Why is she acting like she's so close with me? :normal",
    };
    public Sprite ImageWakeUpSprite;

    [Header("Encounter Scene")]
    public string[] dialogueNoSwimming = 
    {
        "Alan: Sad :I don't swim to get here. I think I'm safe. :bold_italic"
    };
    public string[] dialogueBeach = 
    {
        "Alan: Smile :Even the thought of sitting feels like too much effort. :bold_italic"
    };
    public string[] dialogueTrees =
    {
        "Alan: Flat :Funny how even a solid wood standing feels more alive than I do. :bold_italic"
    };
    public string[] dialoguePickingUpWatch =
    {
        "Alan: Flat :Whose watch is this? It looks like it's been broken for years. :bold_italic",
        "Alan: Flat :I think I'll just keep it for now. :bold_italic"
    };
    public string[] dialogueBeforePickingUpWallet =
    {
        "Cindy: Smile :You might wanna get your wallet first before a ghost steal it. :normal"
    };

    public string[] dialogueWithCindyAfterPickingUpWallet =
    {
        "Alan: Smile :So, what a girl doing here alone in the middle of the night? :normal",
        "Cindy: Flat :Why are you so nosy, huh? :normal",
        "Alan: Sad :Said a girl who being overly familiar with a guy she just met. :normal",
        "Cindy: Smile :Uh… you know what night is it today? :normal",
        "Cindy: Flat :It's Thursday night. :normal",
        "Alan: Sad :So? :normal",
        "Cindy: Flat :Do you believe in ghost? Are you scared of ghost? :normal",
        "Alan: Smile :No, haven't seen any. :normal",
        "Cindy: Sad :Cool. A folk lore around here said that you can sight a ghost on Thursday night. :normal",
        "Cindy: Smile :So help me hunt this ghost. I'm just very curious. :normal",
        "Alan: Flat :I don't understand. Why me? :normal",
        "Alan: Sad :Why are you so interested in this? :normal",
        "Cindy: Flat :Just for fun. The thrill of hunting ghosts with somebody excites me. :normal",
        "Cindy: Smile :And just so you know, I also easily get scared when I'm alone. :normal",
        "Alan: Sad :She is totally weird. :normal",
        "Alan: Smile :But she make me feels less lonely. :normal",
        "Cindy: Flat :Let's go, I don't wanna waste any more time. I'm going home before midnight. :normal",
    };

    public bool foundWalletAndWatch = false;
    public GameObject walletAndWatchGO;
    public bool isDialogueWithCindy = false;

    public string[] afterFoundWatchAndTalkWithCindy = 
    {
        "I'm walking…",
        "But it feels different than the last…",
        "No more despair…",
        "I can keep moving forward."
    };

    [Header("Happiness Scene")]
    public string[] dialogueAlanCindyWalkTogether =
    {
        "Cindy: Flat :You know? Before we met, I was chased by a creep. That was very scary.: normal",
        "Alan: Smile :Are you still scared?: normal",
        "Cindy: Flat :Maybe…: normal",
        "Alan: Sad :Why are you scared?: normal",
        "Cindy: Smile :I don't know… More like, I just have a feeling that he has a bad intention.: normal",
        "Cindy: Sad :Alan, do you want to know a story of me?: normal",
        "Alan: Flat :Even if I said no, you would still tell me.: normal",
        "Cindy: Smile :Hehe…  : normal",
        "Cindy: Flat :Me and my parents… we don't have a strong bond.: normal",
        "Cindy: Sad :We are not fighting, we are just not close.: normal",
        "Cindy: Smile :I always try to be a daughter they can be proud of.: normal",
        "Cindy: Flat :They give me everything I want and need materially, but… they were never paying their attention to me.: normal",
        "Cindy: Smile :I don't know why I'm telling you this… You must be thinking I'm weird right now.: normal",
        "Alan: Sad :Yes, you really are. This girl is very unpredictable, the first that I can't read.: normal",
        "Alan: Flat :I…: normal"
    };
    public GameObject loopingBackgroundGO;
    public float distanceCindyAfterGoAway = 10f;

    [Header("MiniGame Scene")]
    public string[] dialogueRound1MiniGameTree =
    {
        "Cindy: Flat :Al, this hunt is getting boring.: normal",
        "Cindy: Smile :How about we play something?: normal",
        "Alan: Flat :Huh? Where are you?: normal",
        "Cindy: Smile :Behind one of the trees.: normal",
        "Cindy: Flat :Try to find me... That’s the game.: normal",
        "Cindy: Smile :Now go, don’t keep a girl waiting.: normal",
        "Alan: Flat :This girl… I don’t know what she’s up to.: bold_italic",
        "Alan: Smile :I just want to follow her game.: bold_italic",
        "Cindy: Flat :Eh, I’ll reward you if you can find me quickly enough.: normal"
    };

    public string[] dialogueWrongTree = 
    {
        "Alan: Flat :She’s not here…: normal"
    };

    public string[] dialogueRound2MiniGameTree =
    {
        "Cindy: Smile :Ahahahaha:normal",
        "Cindy: Flat :Took you to long to find me.:normal",
        "Cindy: Flat :But it’s fun…:normal",
        "Cindy: Smile :Let’s do it one more time.:normal",
        "Cindy: Smile :Close your eyes, Al. No cheating.:normal",
        "Alan: Flat :This is so childish and silly…:normal",
        "Alan: Smile :But it’s not the fun I seek, I just wanna be with someone.:normal"
    };

    public string[] dialogueFoundCindyRound2 =
    {
        "Alan: Flat :Cindy? I know you’re here.:normal",
        "Alan: Flat :…:normal"
    };

    public string[] dialogueCindyOnTop = 
    {
        "Cindy: Smile :AH!!!! normal",
        "Alan: Flat :Is this the reward? normal",
        "Cindy: Sad :No no no… you st- stupid. Perv- normal",
        "Alan: Smile :Ahahahahaha normal",
        "Alan: Flat :Cindy… Thanks… normal",
        "Cindy: Sad :Ff- for waht?? normal",
        "Alan: Smile :I feel like I want to give it a try again after I know you… normal",
        "Cindy: Sad :Try again for what? normal",
        "Alan: Flat :To live normal",
        "Alan: Smile :I bounced back thanks to you… normal",
        "Alan: Flat :You told me your stories, now let me tell you mine. normal",
        "Cindy: Sad :Oww… fair enough. normal",
        "Alan: Smile :But beforehand, could you please stand up? normal",
        "Cindy: Smile :!!! normal",
        "Alan: Sad :To be honest, I'd be lying if I don't like this moment. normal",
    };

    public string[] thoughtsAfterMiniGameTree =
    {
        "I tell her my stories.",
        "Everything I need to share with someone.",
        "I realized I only need somebody to lean on for now.",
        "I feel so stupid for what I did before.",
        "And now, this night may come to an end.",
    };

    public bool isFirstTree = true;
    public bool isRound1 = true;
    public bool isMiniGameTree = false;
    public int intTreeRandomizer;
    public int intCheckTree = 0;
    public Sprite ImageCindyOnTopSprite;

    [Header("Cindy Scene - Level 4")]
    public string[] dialogueAfterAlanDroppedWatch = 
    {
        "Cindy:	neutral	:That watch…:normal",
        "Alan:	neutral	:You knew about it? Seems broken for years.:normal",
        "Cindy:	neutral	:…:normal",
        "Alan:	worried	:Cindy?:normal",
        "Cindy:	neutral	:That's mine, a gift from my mom.:normal",
        "Alan:	neutral	:Uh.. take it then…:normal",
        "Cindy:	neutral	:Hey Al, do you want to listen my last story before we part?:normal",
        "Alan:	neutral	:I don't mind, I… kinda wanna hear it.:normal",
        "Cindy:	neutral	:I- I think I can tell you this now…:normal",
        "Cindy:	neutral	:That watch is the only gift from my mother,:normal",
        "Cindy:	neutral	:More like I asked her that gift.:normal",
        "Cindy:	neutral	:Still… I bragged about it to my friends…:normal",
        "Cindy:	neutral	:A gift from a mother.:normal",
        "Cindy:	pensive	:Alan, sorry...:normal",
        "Cindy:	pensive	:I lied about being older than you.:normal",
        "Cindy:	pensive	:We both are born in the same year, but I was never aged.:normal",
        "Alan:	neutral	:I don't like where this is going.:italicbold",
        "Cindy:	pensive	:2 years ago in this riverbank, I was murdered.:normal",
        "Cindy:	pensive	:The creep I told, that guy dropped my cold body under this river.:normal",
        "Alan:	worried	:Don't be scared, Al. Stay cool. She might need help.:italicbold",
        "Cindy:	pensive	:I'm sorry… for this truth.:normal",
        "Cindy:	pensive	:Something in my chest feels so itchy…:normal",
        "Cindy:	pensive	:I always wanted to tell somebody... about this. You…:normal",
        "Cindy:	cry	:Alan…:normal",
        "Cindy:	cry	:I'm scared… the fact that I actually don't want to be like this…:normal",
        "Cindy:	cry	:And this last 2 years feels so lonely until you came…:normal",
        "Cindy:	tears_smile	:I'm happy… with you…:normal",
        "Cindy:	tears_smile	:Feels like I can do anything with you.:normal",
        "Cindy:	cry	:I want it… but-:normal",
        "Alan:	neutral	:It's hurt to know the truth… but if she's not alive, It doesn't matter…:italicbold",
        "Alan:	relieved	:She helped me, after all… Now it's my turn to repay her the favor.:italicbold",
        "Alan:	slightly_smile	:Cindy, I-:normal",
    };

    public bool droppedWatch = false;

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
        middleText.color = new Color(255, 255, 255, 0);

        delayFadeInOutTextInBlackscreen = fadeInBlackscreen + fadeOutBlackscreen;
        // delayTextTime = delayFadeInOutTextInBlackscreen;

        delayTextTime = fadeInText + fadeOutText;
        defaultDelayText = fadeInText + fadeOutText;

        flowTransitionChapter1.Add(NothingHappend); //0
        flowTransitionChapter1.Add(() => TransitionCharacterText(targetTextForTransition)); //1
        flowTransitionChapter1.Add(Dialogue); //2
        flowTransitionChapter1.Add(FadeInBlackscreenTransition); //3
        flowTransitionChapter1.Add(FadeOutBlackscreenTransition); //4
        flowTransitionChapter1.Add(FadeInImageSceneTransition); //5
        flowTransitionChapter1.Add(FadeOutImageSceneTransition); //6
        // flowTransitionChapter1.Add(FadeInOutBlackScreeTransition);

        flowFunctionsChapter1.Add(NothingHappend); // 0
        flowFunctionsChapter1.Add(OpeningAfterFadeOutBlackScreen); // 1
        flowFunctionsChapter1.Add(DialogueWithCindyInBlackScreen); // 2
        flowFunctionsChapter1.Add(GoWakeUp); // 3
        flowFunctionsChapter1.Add(LookAtandFirstConversationWithCindy); // 4
        flowFunctionsChapter1.Add(FinishedFirstConversation); // 5
        flowFunctionsChapter1.Add(StandUpAfterConversationWithCindy); // 6
        flowFunctionsChapter1.Add(TeleportRiverSideTrees); // 7
        flowFunctionsChapter1.Add(TalkWithCindy); // 8
        flowFunctionsChapter1.Add(FinishedTalkWithCindy); // 9
        flowFunctionsChapter1.Add(AlanThoughtAfterFoundWatch); // 10
        flowFunctionsChapter1.Add(AfterAlanThought); // 11
        flowFunctionsChapter1.Add(FadeOutAfterThought); // 12
        flowFunctionsChapter1.Add(WalkTogetherWithCindy); // 13
        flowFunctionsChapter1.Add(CindyHidBehindTree); // 14
        flowFunctionsChapter1.Add(FadeInAfterCindyGoAway); // 15
        flowFunctionsChapter1.Add(FadeOutAfterCindyGoAway); // 16
        flowFunctionsChapter1.Add(FindCindyBehindTree); // 17
        flowFunctionsChapter1.Add(DialogueMiniGameTree); // 18
        flowFunctionsChapter1.Add(StartMiniGameTree); // 19
        flowFunctionsChapter1.Add(AfterFoundCindyInRound1); // 20
        flowFunctionsChapter1.Add(FadeInAfterRound1MiniGameTree); // 21
        flowFunctionsChapter1.Add(FadeOutAfterRound1MiniGameTree); // 22
        flowFunctionsChapter1.Add(DialogueAfterRound1MiniGameTree); // 23
        flowFunctionsChapter1.Add(FadeInGoToRound2); // 24
        flowFunctionsChapter1.Add(FadeOutGoToRound2); // 25
        flowFunctionsChapter1.Add(FinalMiniGameTree); // 26
        flowFunctionsChapter1.Add(FadeInBlackScreenCutScene); // 27
        flowFunctionsChapter1.Add(FadeInCutSceneOnTop); // 28
        flowFunctionsChapter1.Add(DialogueInCutsceneOnTop); // 29
        flowFunctionsChapter1.Add(FadeOutCutSceneOnTop); // 30
        flowFunctionsChapter1.Add(ThoughtAfterOnTop); // 31
        flowFunctionsChapter1.Add(FadeOutBlackScreenOnTop); // 32
        flowFunctionsChapter1.Add(AlanDroppedWatch); // 33
        flowFunctionsChapter1.Add(DialogueAfterDroppedWatch); // 34

        InitialGame();
        AddExpressionCharacter();
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
        intFunctionNumbersNow = 1;

        thoughtText = middleText;
        targetTextForTransition = thoughtText;
    }

    void AddExpressionCharacter()
    {
        characterExpressions.Add("Alan", scriptExpressionCharacters.alanExpressions);
        characterExpressions.Add("Cindy", scriptExpressionCharacters.cindyExpressions);
        characterExpressions.Add("???", scriptExpressionCharacters.cindyExpressions);
        characterExpressions.Add("Alan's Dad", scriptExpressionCharacters.alanDadExpressions);
        characterExpressions.Add("Alan's Mom", scriptExpressionCharacters.alanMomExpressions);
    }

    void TransitionCharacterText(Text textTarget) // 1
    {
        delayTextTime -= Time.deltaTime;
        if (delayTextTime < defaultDelayText / 2)
            textTarget.color = new Color(255, 255, 255, textTarget.color.a - Time.deltaTime / fadeInText);
        else
            textTarget.color = new Color(255, 255, 255, textTarget.color.a + Time.deltaTime / fadeOutText);

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
        thoughtText.text = characterThoughts[intCharacterText];
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
            textAnimationFinished = false;
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
            }
        }

        // if (Input.GetKeyDown(KeyCode.Z))
        //menggunakan click kanan mouse
        if (Input.GetMouseButtonDown(1) && textAnimationFinished)
        {
            if (intCharacterText > 0)
            {
                textAnimationFinished = false;
                nextIcon.SetActive(false);
                // delayDialogue = 0;
                intCharacterText--;
                // dialogueText.text = characterDialogue[intCharacterText];
                SplitNameExpressionDialogue();
                StartCoroutine(ShowText());
                CheckLengthWords();
            }
        }
        //skip dialog
        // if (Input.GetKeyDown(KeyCode.C))
        //menggunakan click tengah mouse
        if (Input.GetMouseButtonDown(2) && textAnimationFinished)
        {
            //stop Coroutine
            StopAllCoroutines();
            textAnimationFinished = false;
            nextIcon.SetActive(false);
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
                nextIcon.SetActive(true);
            }
        }
    }

    void SplitNameExpressionDialogue() //DelimiterDialogueText
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

    void ChangeExpressionCharacterDialogue(string name, string expression)
    {
        if (characterExpressions.ContainsKey(name))
        {
            expressionCharacterImage.sprite = characterExpressions[name][0];
            if (expression == "Flat")
                expressionCharacterImage.sprite = characterExpressions[name][0];
            else if (expression == "Smile")
                expressionCharacterImage.sprite = characterExpressions[name][1];
            else if (expression == "Sad")
                expressionCharacterImage.sprite = characterExpressions[name][2];
        }
        // if (name == "Alan")
        //     if (expression == "Flat")
        //         expressionCharacterImage.sprite = scriptExpressionCharacters.alanExpressions[0];
        // if (name == "Cindy" || name == "???")
        //     else if (expression == "Smile")
        //         expressionCharacterImage.sprite = scriptExpressionCharacters.cindyExpressions[1];
    }

    void FadeInBlackscreenTransition() //3
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

        if (isDialogueWithCindy)
            isDialogueWithCindy = false;
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
        SplitNameExpressionDialogue();
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
            scriptPlayerMovement.rb2D.velocity = Vector2.zero;
            intTransitionNumbersNow = 3;
            intFunctionNumbersNow = 2;
        }
        else
        {
            // DefaultTriggerMechanism();
            characterDialogue = dialogueBeforeExitMemories;
            // dialogueText.text = characterDialogue[intCharacterText];
            DefaultTriggerMechanism();
        }
    }

    public void TriggerObjectNoSwimmingSign()
    {
        characterDialogue = dialogueNoSwimming;
        // dialogueText.text = characterDialogue[intCharacterText];
        DefaultTriggerMechanism();
    }

    public void TriggerObjectBeach()
    {
        characterDialogue = dialogueBeach;
        // dialogueText.text = characterDialogue[intCharacterText];
        DefaultTriggerMechanism();
    }

    public void TriggerObjectTrees(GameObject treeGameObject)
    {
        if (!isMiniGameTree)
        {
            characterDialogue = dialogueTrees;
            DefaultTriggerMechanism();
        }
        else
            TriggerObjectMiniGame(treeGameObject);
    }

    public void TriggerObjectWatch()
    {
        characterDialogue = dialoguePickingUpWatch;
        foundWalletAndWatch = true;
        walletAndWatchGO.SetActive(false);
        DefaultTriggerMechanism();
    }

    public void TriggerObjectCindy()
    {
        if (foundWalletAndWatch)
            characterDialogue = dialogueWithCindyAfterPickingUpWallet;
        else
            characterDialogue = dialogueBeforePickingUpWallet;
        isDialogueWithCindy = true;

        DefaultTriggerMechanism();
    }

    public void TriggerObjectMiniGame(GameObject treeGameObject)
    {
        playerGO.GetComponent<ObjectInterect>().ManualTriggerExit2DTree(treeGameObject);

        if (isRound1)
        {
            if (isFirstTree)
            {
                isFirstTree = false;
                characterDialogue = dialogueWrongTree;
                // temp.tag = "Untagged";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
            }
            else
            {
                isRound1 = false;
                characterDialogue = dialogueRound2MiniGameTree;
                intTreeRandomizer = Random.Range(2, 10);
                // isFirstTree = true;
                // temp.tag = "Trees";

                //remove all game objects trees
                foreach (GameObject tree in playerGO.GetComponent<ObjectInterect>().tempTrees)
                    tree.tag = "Trees";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Clear();
            }
        }
        else
        {
            if (intCheckTree >= intTreeRandomizer)
            {
                characterDialogue = dialogueFoundCindyRound2;
                isMiniGameTree = false;
                foreach (GameObject tree in playerGO.GetComponent<ObjectInterect>().tempTrees)
                    tree.tag = "Trees";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Clear();
                DefaultTriggerMechanism();
                return;
            }
            else
            {
                characterDialogue = dialogueWrongTree;
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
            }
            intCheckTree++;
        }
        DefaultTriggerMechanism();
    }

    void OpeningAfterFadeOutBlackScreen()
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intFunctionNumbersNow = 0;
            intTransitionNumbersNow = 0;
        }
    }

    void DialogueWithCindyInBlackScreen() // 2 = Scene after open door / dialogue in black screen with cindya
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

    void GoWakeUp() // 3 = Scene after dialogue with cindya in black screen
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

    void LookAtandFirstConversationWithCindy() // 4 = Scene after wake up
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

    void FinishedFirstConversation() // 5 = Scene after first conversation with cindy
    {
        if (isDialogue)
            return;
        intTransitionNumbersNow = 6;
        intFunctionNumbersNow++;
    }

    void StandUpAfterConversationWithCindy() // 6 = Scene after finished first conversation with cindy
    {
        if (imageSceneFadeOut)
        {
            intTransitionNumbersNow = 4; //fade out blackscreen
            intFunctionNumbersNow++;
            imageSceneFadeOut = false;

            homeAlanGO.SetActive(false);
            riverSideTreesGO.SetActive(true);

            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
        }
    }

    void TeleportRiverSideTrees() // 7 = Scene after stand up after conversation with cindy
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intTransitionNumbersNow = 0;
            intFunctionNumbersNow++;
        }
    }

    void TalkWithCindy() // 8 = Scene after teleport to river side trees
    {
        if (!foundWalletAndWatch)
            return;

        if (!isDialogueWithCindy)
            return;

        intFunctionNumbersNow++;
    }

    void FinishedTalkWithCindy()
    {
        if (isDialogue && isDialogueWithCindy)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 3;
        blackscreenImage.gameObject.SetActive(true);
    }

    void AlanThoughtAfterFoundWatch() // 10 = Scene after fade in to after talk
    {
        if (blackscreenFadeIn)
        {
            blackscreenFadeIn = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 1;

            characterThoughts = afterFoundWatchAndTalkWithCindy;
            thoughtText.text = characterThoughts[intCharacterText];
            isThought = true;
            middleText.gameObject.SetActive(true);
        }
    }

    void AfterAlanThought() // 11 = Scene after Alan thought after found watch
    {
        if (isThought)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 4;
        
        loopingBackgroundGO.SetActive(true); // enable looping background
        GuideGO.SetActive(false);

        playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(5f, playerGO.transform.position.y);

        riverSideTreesGO.SetActive(false);
    }

    // Level: LC1_03 (Happiness)
    void FadeOutAfterThought() // 12
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intFunctionNumbersNow++;

            characterDialogue = dialogueAlanCindyWalkTogether;
            DefaultTriggerMechanism();
        }
    }

    void WalkTogetherWithCindy() //13
    {
        if (isDialogue)
            return;
        
        // loopingBackgroundGO.SetActive(false);
        scriptPlayerMovement.canMove = false;
        cindyGO.GetComponent<PlayerMovement>().canMove = true;
        cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
        cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // GuideGO.SetActive(true);
        intFunctionNumbersNow++;
        intTransitionNumbersNow = 0;
    }

    void CindyHidBehindTree() // 14
    {
        if (cindyGO.transform.position.x > distanceCindyAfterGoAway)
        {
            cindyGO.GetComponent<PlayerMovement>().canMove = false;
            cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
            cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            cindyGO.SetActive(false);
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 3;
            // GuideGO.SetActive(true);
            blackscreenImage.gameObject.SetActive(true);
        } 
    }

    void FadeInAfterCindyGoAway() // 15
    {
        if (blackscreenFadeIn)
        {
            loopingBackgroundGO.SetActive(false);
            parallaxBackgroundGO.SetActive(true);
            minigameTreeGO.SetActive(true);
            round1TreeGO.SetActive(true);
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 4;
        }
    }

    void FadeOutAfterCindyGoAway() // 16
    {
        if (blackscreenFadeOut)
        {
            scriptPlayerMovement.canMove = true;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 0;
        }
    }

    void FindCindyBehindTree() // 17
    {
        // intFunctionNumbersNow = 0;
        //jika alan berada di posisi minigameTreeGO
        if (playerGO.transform.position.x >= centerMiniGameGO.transform.position.x)
        {
            scriptPlayerMovement.rb2D.velocity = Vector2.zero;

            intFunctionNumbersNow++;
            GuideGO.SetActive(true);

            // riverSideTreesGO.SetActive(false);
            wallMinigameTreeGO.SetActive(true);
            //change follow target
            virtualCamera.m_Follow = centerMiniGameGO.transform;
            // intTransitionNumbersNow = 5;
        }
    }

    void DialogueMiniGameTree() // 18
    {
        characterDialogue = dialogueRound1MiniGameTree;
        DefaultTriggerMechanism();

        intFunctionNumbersNow++;
    }

    void StartMiniGameTree() // 19
    {
        if (isDialogue)
            return;
        intFunctionNumbersNow++;
        isMiniGameTree = true;
    }

    void AfterFoundCindyInRound1() // 20
    {
        if (isRound1)
            return;
        intFunctionNumbersNow++;
        intTransitionNumbersNow = 3;
        blackscreenImage.gameObject.SetActive(true);
    }

    void FadeInAfterRound1MiniGameTree() // 21
    {
        if (blackscreenFadeIn)
        {
            blackscreenFadeIn = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 4;

            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
        }
    }

    void FadeOutAfterRound1MiniGameTree() // 22
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 2;
        }
    }

    void DialogueAfterRound1MiniGameTree() // 23
    {
        if (isDialogue)
            return;
        intFunctionNumbersNow++;
        intTransitionNumbersNow = 3;
        blackscreenImage.gameObject.SetActive(true);
    }

    void FadeInGoToRound2() // 24
    {
      if (blackscreenFadeIn)
        {
            blackscreenFadeIn = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 4;

            cindyGO.SetActive(false);
            wallMinigameTreeGO.SetActive(false);
            round1TreeGO.SetActive(false);
            round2TreeGO.SetActive(true);
            virtualCamera.m_Follow = playerGO.transform;
        }
    }

    void FadeOutGoToRound2() // 25
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow=0;
        }
    }

    void FinalMiniGameTree() // 26
    {
        if (isMiniGameTree)
            return;
        
        if (isDialogue)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 3;
        blackscreenImage.gameObject.SetActive(true);
    }

    void FadeInBlackScreenCutScene() // 27
    {
        if (blackscreenFadeIn)
        {
            blackscreenFadeIn = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 5;

            imageScene.gameObject.SetActive(true);
            spriteImageScene = ImageCindyOnTopSprite;
            imageScene.sprite = spriteImageScene;
        }
    }

    void FadeInCutSceneOnTop() // 28
    {
        if (imageSceneFadeIn)
        {
            characterDialogue = dialogueCindyOnTop;
            DefaultTriggerMechanism();
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 2;
            imageSceneFadeIn = false;
        }

    }

    void DialogueInCutsceneOnTop() // 29
    {
        if (isDialogue)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 6;
    }

    void FadeOutCutSceneOnTop() // 30
    {
        if (imageSceneFadeOut)
        {
            imageSceneFadeOut = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 1;

            characterThoughts = thoughtsAfterMiniGameTree;
            thoughtText.text = characterThoughts[intCharacterText];
            isThought = true;
            middleText.gameObject.SetActive(true);
        }
    }

    void ThoughtAfterOnTop()
    {
        if (isThought)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 4;
        blackscreenImage.gameObject.SetActive(true);


        cindyGO.SetActive(true);
        playerGO.transform.position = new Vector2(10f, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(playerGO.transform.position.x - 2, cindyGO.transform.position.y);
    }

    void FadeOutBlackScreenOnTop() // 32
    {
        if (blackscreenFadeOut)
        {
            blackscreenFadeOut = false;
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 0;

            // scriptPlayerMovement.canMove = true;
            scriptPlayerMovement.intAutoMovement = 2; // movet to left
            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x - 2, cindyGO.transform.position.y);
            cindyGO.GetComponent<PlayerMovement>().canMove = true;
            cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 2;
        }
    }

    void AlanDroppedWatch()
    {
        // input key a and d
        if (!droppedWatch)
            if (playerGO.transform.position.x <= 0)
                droppedWatch = true;
        else
        {
            characterDialogue = dialogueAfterAlanDroppedWatch;
            DefaultTriggerMechanism();
            intFunctionNumbersNow++;
            intTransitionNumbersNow = 2;
        }
    }

    void DialogueAfterDroppedWatch()
    {
        if (isDialogue)
            return;

        intFunctionNumbersNow++;
        intTransitionNumbersNow = 3;
    }
}