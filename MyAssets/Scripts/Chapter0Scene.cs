using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chapter0Scene : MonoBehaviour
{
    private List<System.Action> scriptAlanFunction = new List<System.Action>();
    private List<System.Action> transitionAlanFunction = new List<System.Action>();
    [SerializeField] int alanFunc = 1;
    [SerializeField] int alanTransition = 1;
    // [SerializeField] bool thisCanRun = true;

    [Header("Player")]
    public PlayerMovement scriptPlayerMovement;
    public Vector3 playerPositionSpawn;
    public Transform playerTransform;

    [Header("Stalker")]
    public Transform stalkerTransform;
    public StalkerMovement scriptStalkerMovement;

    [Header("Scripts")]
    public OpeningGameplay openingGameplay;
    public GuideScript scriptGuide;

    [Header("Character Text")]
    public GameObject characterTextGO;
    public GameObject middleTextGO;
    public GameObject rightTextGO;
    public Text characterText;
    public Text middleText;
    public Text rightText;
    public Text targetTextForTransition;
    [SerializeField] int intCharacterText = 0;
    [SerializeField] float delayTextTime;
    [SerializeField] float fadeInText = 1f;
    [SerializeField] float fadeOutText = 1f;
    private float defaultDelayText;
    
    [Header("BlackScreen Scene")]
    public bool blackScreenFadeOut = false;
    public bool blackScreenFadeIn = false;
    public string[] blackScreenScene;
    public GameObject blackScreenGO;
    public Image blackScreenImage;
    public Text blackScreenText;
    [SerializeField] float fadeInBlackScreen = 1.5f;
    [SerializeField] float fadeOutBlackScreen = 1.5f;
    private float delayTextBlackScreen;

    [Header("Dialogue")]
    public string[] characterDialogue;
    public GameObject dialogueGO;
    public Text dialogueText;

    [Header("Alan Scene")]
    public string[] alanBlackScreenScene = new string[] {
        "Life was very smooth,",
        "until last year.",
        "It has been a painful year.",
    };
    public string[] alanDialogueWithBoss = new string[] {
        "Alan`s boss: \"Even after a year, all of your work are still trash!\"",
        "Alan`s boss: \"You were never grown, shame on you.\"",
        "Alan`s boss: \"Alan, no thanks to you. You`re fired!\"",
        "Alan_blank:  \"…\""
    };
    public string[] thoughts1 = new string[]
    {
        "How did it come to this?",
        "Wasn't I good enough?",
        "Wasn't my dedication worth anything?",
        "I’ve done whatever they asked.",
        "I thought things would just gonna run perfectly.",
        //Music Stop
        "Maybe they were right to let me go.",
        "Maybe I am just a failure.",
        "I may have never been a grown man."
    };
    public string[] thoughts2 = new string[]
    {
        "I keep walking…",
        "Why do I keep walking?",
        "There’s no future for me…",
        "Maybe they were right to let me go.",
        "Maybe I am just a failure.",
        "I may have never been a grown man."
    };
    public string[] alanDialogueOnBridge = new string[] {
        "Alan: \"The river whispers promises of peace, of an end to this pain. \"",
        "Alan: \"Maybe it's time to let its cold embrace take me.\"",
        "Alan: \"Mum, Dad... I'm sorry. Your son could only make it this far.\""
    };
    public string[] alanThoughtsBridge = new string[]
    {
        "Just jump…",
        "End it…",
        "End your suffering…"
    };

    [Header("Thoughts Scene")]
    [SerializeField] bool isThougts2Done = false;
    public string[] characterThoughts;
    [SerializeField] float maxDistanceThoughts = 10f;
    [SerializeField] Text thoughtsText;
    [SerializeField] float fadeInTextThoughtScene = 1f;
    [SerializeField] float fadeOutTextThoughtScene = 1f;
    private float delayTextThoughtScene;

    [Header("Bridge Scene")]
    public Text alanBridgeText;
    public bool isJump = false;
    public bool isBridgeScene = false;
    public GameObject bridgeGO;
    public GameObject bridgeSpawn;
    public GameObject roadGO;
    // public float fadeInTextBridge = 1f;
    // public float fadeOutTextBridge = 1f;


    //Cindy
    [Header("Cindy Scene")]
    public string[] cindyBlackScene = new string[] {
        "That guy creeps me out.",
        "I guess he still following me.",
        "He’s getting closer.",
        "I think I have to run now."
    };
    public string[] cindyThoughts = new string[] {
        "Why is this happening?",
        "Just keep moving, Cindy",
        "Almost outrun him…"
    };
    public string[] cindyDialogue = new string[] {
        "???: \"Ah… this is wearing me down.\"",
        "???: \"Is he gone already?\"",
        "???: \"I think it’s a good time to leave.\""
    };
    bool can = true;
    [SerializeField] bool cindyMoved = false;
    public bool isCindyScene = false; 
    [SerializeField] float roamingTimeCindy = 10f;
    public float stalkerDistanceAway = 15f;
    public Image exclamationMark;
    [SerializeField] bool endOfChapter0 = false;

    // public GameObject cindyGO;
    // public GameObject cindySpawn;
    // public GameObject stalkerCindy;
    // [SerializeField] float fadeInTextCindy = 1f;
    // [SerializeField] float fadeOutTextCindy = 1f;
    // [SerializeField] float delayTextCindy = 2f;
    // [SerializeField] float fadeInCindy = 2f;
    // [SerializeField] float fadeOutCindy = 2f;

    // public GameObject stalkerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // alanBridgeText.enabled = false;
        rightTextGO.SetActive(false);
        middleTextGO.SetActive(true);
        blackScreenGO.SetActive(true);
        dialogueGO.SetActive(false);
        // alanThougtsGO.SetActive(false);

        openingGameplay.enabled = false;

        scriptAlanFunction.Add(DoNothing); // 0
        scriptAlanFunction.Add(CharactersOPBlackScreen); // 1
        scriptAlanFunction.Add(Dialogue); // 2
        scriptAlanFunction.Add(AutoWalk); // 3
        scriptAlanFunction.Add(Thoughts); // 4
        scriptAlanFunction.Add(JumpToRiver); // 5
        scriptAlanFunction.Add(CindyFadeOut); // 6
        scriptAlanFunction.Add(CindyGoHiding); // 7
        scriptAlanFunction.Add(CindyIsHiding); // 8
        scriptAlanFunction.Add(StalkerAppears); // 9
        scriptAlanFunction.Add(CindyOutOfHiding); // 10
        scriptAlanFunction.Add(CindyRoaming); // 11

        //Cindy
        // scriptAlanFunction.Add(CindyOpening);

        thoughtsText = middleText;
        alanBridgeText = rightText;
        blackScreenText = middleText;

        blackScreenScene = alanBlackScreenScene;
        blackScreenText.text = alanBlackScreenScene[intCharacterText];
        blackScreenText.color = new Color(255, 255, 255, 0);
        thoughtsText.color = new Color(255, 255, 255, 0);
        alanBridgeText.color = new Color(255, 255, 255, 0);

        transitionAlanFunction.Add(DoNothing); //0
        transitionAlanFunction.Add(() => TransitioncharacterText(targetTextForTransition)); //1
        transitionAlanFunction.Add(FadeInBlackscreenTransition); //2
        transitionAlanFunction.Add(FadeOutBlackscreenTransition); //3

        playerPositionSpawn = playerTransform.position;
        targetTextForTransition = blackScreenText;
        characterText = thoughtsText;

        // Invoke("Opening", 1f);

        fadeInText = fadeInBlackScreen;
        fadeOutText = fadeOutBlackScreen;
        delayTextBlackScreen = fadeInBlackScreen + fadeOutBlackScreen;
        delayTextThoughtScene = fadeInTextThoughtScene + fadeOutTextThoughtScene;
        delayTextTime = delayTextBlackScreen;
        defaultDelayText = delayTextTime;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!thisCanRun)
        //     return;
        if (alanFunc != 0)
            scriptAlanFunction[alanFunc]();

        if (alanTransition != 0)
            transitionAlanFunction[alanTransition]();
    }

    void Opening()
    {
        alanFunc = 1;
        alanTransition = 1;
    }

    void TransitioncharacterText(Text textTarget)
    {
        delayTextTime -= Time.deltaTime;
        if (delayTextTime < defaultDelayText / 2)
        {
            textTarget.color = new Color(255, 255, 255, textTarget.color.a - Time.deltaTime / fadeInText);
            return;
        }
        textTarget.color = new Color(255, 255, 255, textTarget.color.a + Time.deltaTime / fadeOutText);
    }

    void DoNothing()
    {
        return;
    }

    void CharactersOPBlackScreen()
    {   
        if (delayTextTime > 0)
            return;

        delayTextTime = defaultDelayText;
        intCharacterText++;
        if (intCharacterText >= blackScreenScene.Length)
        {
            blackScreenText.text = "";
            // openingGameplay.enabled = true;
            
            intCharacterText = 0;
            alanTransition = 0;
            if (!isCindyScene)
            {            
                alanFunc = 2;
                dialogueGO.SetActive(true);

                characterDialogue = alanDialogueWithBoss;
                dialogueText.text = characterDialogue[intCharacterText];
            }
            else
            {
                alanFunc = 6;
                alanTransition = 3;


            }
            // this.enabled = false; //disabled this scripts
            return;
        }
        blackScreenText.text = blackScreenScene[intCharacterText];
    }
    
    //if get clicked
    public void Dialogue() // 2
    {   
        //menekan tombol panah kanan atau kiri untuk melanjutkan dialog
        if (Input.GetKeyDown(KeyCode.X) )
        {
            if (intCharacterText < characterDialogue.Length - 1)
            {
                intCharacterText++;
                dialogueText.text = characterDialogue[intCharacterText];
            }
            else
            {
                dialogueGO.SetActive(false);
                
                intCharacterText = 0;
                if(isCindyScene)
                {
                    alanTransition = 2;
                    alanFunc = 10;
                    return;
                }

                // this.enabled = false;
                if (isThougts2Done)
                {
                    IfIsThougts2Done();
                }
                else
                {
                    alanFunc++;
                    openingGameplay.enabled = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (intCharacterText > 0)
            {
                intCharacterText--;
                dialogueText.text = characterDialogue[intCharacterText];
            }
        }

        //skip dialog
        if (Input.GetKeyDown(KeyCode.C))
        {
            dialogueGO.SetActive(false);
            
            intCharacterText = 0;

            if(isCindyScene)
            {
                alanTransition = 2;
                alanFunc = 10;
                return;
            }
            // this.enabled = false;
            if (isThougts2Done)
            {
                IfIsThougts2Done();
            }
            else
            {
                alanFunc++;
                openingGameplay.enabled = true;
            }
        }
    }



    void IfIsThougts2Done()
    {
        alanFunc = 4;
        alanTransition = 1;

        // bridge
        // alanBridgeText.enabled = true;
        // alanThoughtsText.enabled = false;
        middleTextGO.SetActive(false);
        rightTextGO.SetActive(true);
        // alanThoughtsText = alanBridgeText;
        // alanThoughtsText.transform.position = new Vector3(200, 0, 0);
        
        characterText = alanBridgeText;
        targetTextForTransition = characterText;

        characterThoughts = alanThoughtsBridge; //mengambil percakapan bridge ke alanThoughts
        characterText.text = characterThoughts[intCharacterText];
        
        // fadeInText = fadeInTextBridge;
        // fadeOutText = fadeOutTextBridge;
    }
    void AutoWalk()
    {
        //jika posisi x player lebih dari melewati jarak 10f
        if (playerTransform.position.x > playerPositionSpawn.x + maxDistanceThoughts)
        {
            scriptPlayerMovement.intAutoMovement = 1;

            alanFunc = 4;
            

            // alanThougtsGO.SetActive(true);
            characterText = thoughtsText;
            targetTextForTransition = characterText;

            alanTransition = 1;
            // if (!isCindyScene)
            // {
                characterThoughts = thoughts1;
                characterText.text = characterThoughts[intCharacterText];
            // }
            // else
            // {
            //     characterThoughts = cindyThoughts;
            //     characterText.text = characterThoughts[intCharacterText];
            // }

            fadeInText = fadeInTextThoughtScene;
            fadeOutText = fadeOutTextThoughtScene;
            delayTextTime = delayTextThoughtScene;
            defaultDelayText = delayTextTime;
            return;
        }
    }

    void Thoughts() // 4
    {        
        if (delayTextTime > 0)
        return;

        delayTextTime = defaultDelayText;
        intCharacterText++;
        if (intCharacterText >= characterThoughts.Length)
        {   
            blackScreenGO.SetActive(true);
            thoughtsText.text = "";
            if (isCindyScene)
            {
                // alanTransition = 2;
                alanFunc = 7;
                return;
            }

            // openingGameplay.enabled = true;

            if (isBridgeScene)
            {
                alanTransition = 0;
                alanFunc = 5;

                scriptGuide.transitionGuideNow = 3;
                scriptGuide.guideJumpGO.SetActive(true);
            }
            else {
                alanTransition++;
                alanFunc = 0;
                intCharacterText = 0;
            }
            
            
            // this.enabled = false; //disabled this scripts
            return;
        }
        // alanThoughtsText.text = alanThoughts[intCharacterText];
        characterText.text = characterThoughts[intCharacterText];
    }

    void FadeInBlackscreenTransition() //2
    {
        blackScreenImage.color = new Color(0, 0, 0, blackScreenImage.color.a + Time.deltaTime / fadeInBlackScreen);
        if (blackScreenImage.color.a >= 1)
        {
            blackScreenImage.color = new Color(0, 0, 0, 1);
            blackScreenFadeIn = true;
            
            if (endOfChapter0)
            {
                EndGameplay();
                return;
            }

            if (isCindyScene)
                return;
            
            alanFunc = 0;
            intCharacterText = 0;
            alanTransition = 0;

            Invoke("TransitionToFadeOut", 1f);
            
            
            if (isThougts2Done)
            {
                // scriptPlayerMovement.canMove = false;
                scriptPlayerMovement.intAutoMovement = 0;
                bridgeGO.SetActive(true);
                playerTransform.position = bridgeSpawn.transform.position;
                roadGO.SetActive(false);
                return;
            }
            else
            {
                scriptPlayerMovement.intAutoMovement = 1;
                playerTransform.position = playerPositionSpawn;
            }

            return;
        }
    }

    void TransitionToFadeOut()
    {
        alanTransition = 3;
    }

    //Level 2 & Level 3
    void FadeOutBlackscreenTransition() // 3
    {
        blackScreenImage.color = new Color(0, 0, 0, blackScreenImage.color.a - Time.deltaTime / fadeOutBlackScreen);
        if (blackScreenImage.color.a <= 0.1)
        {
            blackScreenImage.color = new Color(0, 0, 0, 0);
            blackScreenGO.SetActive(false);
            blackScreenFadeOut = true;
            
            if (!isCindyScene)
            {
                if (!isThougts2Done)
                {
                    GoOutAlanThoughts2();
                }
                else
                {
                    GoOutAlanInBridge();
                }
            }
            return;
        }
        // else if (blackScreenImage.color.a <= 0.5)
        // if (!scriptPlayerMovement.canMove)
        //     scriptPlayerMovement.canMove = true;
    }
    void GoInAlanThoughts2()
    {

    }
    void GoOutAlanThoughts2()
    {
        blackScreenFadeOut = false;
        // scriptPlayerMovement.canMove = true;
        alanTransition = 1; //TransitioncharacterText
        characterThoughts = thoughts2;
        alanFunc = 4; //alanThoughts
        thoughtsText.text = characterThoughts[intCharacterText];
        // scriptPlayerMovement.intAutoMovement = 1;
        isThougts2Done = true;
    }

    void GoOutAlanInBridge()
    {
        blackScreenFadeOut = false;
        scriptPlayerMovement.canMove = false;
        dialogueGO.SetActive(true);
        characterDialogue = alanDialogueOnBridge;
        dialogueText.text = characterDialogue[intCharacterText];

        alanTransition = 0;
        alanFunc = 2; //AlanDialogue

        isBridgeScene = true;
    }

    void JumpToRiver() // 5
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scriptPlayerMovement.canMove = true;
            scriptPlayerMovement.intAutoMovement = 1;

            isJump = true;
        }
        if (isJump)
        {
            blackScreenImage.color = new Color(0, 0, 0, blackScreenImage.color.a + Time.deltaTime / fadeInBlackScreen);
            if (blackScreenImage.color.a >= 1)
            {
                blackScreenImage.color = new Color(0, 0, 0, 1);

                alanFunc = 0;
                alanTransition = 0;

                bridgeGO.SetActive(false);
                roadGO.SetActive(true);
                playerTransform.position = playerPositionSpawn;
                scriptPlayerMovement.canMove = false;

                Invoke("CindyOpening", 1f);
            }
        }
    }

    void CindyOpening()
    {
        intCharacterText = 0;
        alanFunc = 1;
        alanTransition = 1;
        isJump = false;

        middleTextGO.SetActive(true);
        rightTextGO.SetActive(false);
        targetTextForTransition = blackScreenText;
        characterText = blackScreenText;
        blackScreenScene = cindyBlackScene;
        blackScreenText.text = cindyBlackScene[intCharacterText];

        isCindyScene = true;

        // bridgeGO.SetActive(false);
        // roadGO.SetActive(true);
        // playerTransform.position = playerPositionSpawn;
        
        scriptPlayerMovement.canMove = true;
        scriptPlayerMovement.intAutoMovement = 0;
    }

    void CindyFadeOut() // 6
    {
        Debug.Log("CindyFadeOut");
        if (blackScreenFadeOut)
        {
            Debug.Log("CindyFadeOut2");
            blackScreenFadeOut = false;
            alanFunc = 4;
            alanTransition = 1;
            scriptPlayerMovement.intAutoMovement = 1;


            intCharacterText = 0;
            characterThoughts = cindyThoughts;
            characterText.text = characterThoughts[intCharacterText];
        }
    }

    void CindyGoHiding() //7
    {
        if (can)
        {
            can = false;
            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.intAutoMovement = 0;
            alanTransition = 2;
            blackScreenFadeIn = false;
        }

        if (blackScreenFadeIn)
        {
            playerTransform.gameObject.SetActive(false);
            blackScreenFadeIn = false;
            alanFunc = 8;
            alanTransition = 3;
        }
    }

    void CindyIsHiding() // 8
    {
        if (blackScreenFadeOut)
        {
            alanFunc = 9; //
            alanTransition = 0;
            blackScreenFadeOut = false;

            stalkerTransform.gameObject.SetActive(true);
            scriptStalkerMovement.canMove = true;
        }
    }

    void StalkerAppears() // 9
    {
        if (stalkerTransform.position.x < playerTransform.position.x + stalkerDistanceAway)
            return;
        
        scriptStalkerMovement.canMove = false;
        stalkerTransform.gameObject.SetActive(false);

        dialogueGO.SetActive(true);
        intCharacterText = 0;
        characterDialogue = cindyDialogue;
        dialogueText.text = characterDialogue[intCharacterText];

        alanFunc = 2; //
        alanTransition = 0;        
    }

    void CindyRoaming() // 11
    {
        if (!blackScreenFadeOut)
            return;

        if (!cindyMoved)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                cindyMoved = true;
        }
        else
        {
            roamingTimeCindy -= Time.deltaTime;

            if (roamingTimeCindy < 0)
            {
                blackScreenFadeOut = false;

                alanFunc = 0;
                alanTransition = 0;
                Invoke("FadeOutEnding", 1f);
                exclamationMark.gameObject.SetActive(true);

                scriptPlayerMovement.canMove = false;
                scriptPlayerMovement.intAutoMovement = 0;

                blackScreenGO.SetActive(true);
                endOfChapter0 = true;
                return;
            }
        }
    }

    void CindyOutOfHiding() //10
    {
        if (!blackScreenFadeIn)
            return;

        playerTransform.gameObject.SetActive(true);

        scriptPlayerMovement.canMove = true;
        scriptPlayerMovement.intAutoMovement = 0;

        alanFunc = 11;
        alanTransition = 3;

        blackScreenGO.SetActive(true);
        blackScreenFadeIn = false;
        return;
    }

    void FadeOutEnding()
    {
        alanTransition = 2;
    }

    void EndGameplay()
    {
        scriptPlayerMovement.canMove = false;
        scriptPlayerMovement.intAutoMovement = 0;
        exclamationMark.gameObject.SetActive(false);
        alanFunc = 0;
        alanTransition = 0;

        Invoke("NextChapter", 1f);
    }

    void NextChapter()
    {
        SceneManager.LoadScene("Chapter1");
    }
}
