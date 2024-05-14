using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
public class Chapter1Scene : MonoBehaviour
{
    private List<System.Action> flowFunctionsChapter1 = new List<System.Action>();
    private List<System.Action> flowTransitionChapter1 = new List<System.Action>();
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public ExpressionCharacters scriptExpressionCharacters;
    public DialogueChapter1 scriptDialogueChapter1;
    public TransitionFunction scriptTransitionFunction;
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

    [Header("Sprites For Dialogue")]
    public Sprite spriteCertificate;
    public Sprite spriteFamilyPicture;
    public Sprite spriteCindyWatch;

    // [Header("GameObject Image for Scene")]
    // public Image imageScene;
    // public Sprite spriteImageScene;

    [Header("Memories Scene")]
    private string[] familyPictureString;
    private string[] certificateString;
    private string[] dialogueWithParents1;
    private string[] dialogueWithParents2;
    //string jika player belum berinteraksi dengan objek foto, sertifikat, dan orang tua
    private string[] dialogueBeforeExitMemories;
    private string[] dialogueAfterOpenDoor;

    public bool haveTalkedToParents = false;
    public bool canOpenDoor = false;

    [Header("Wake Up Scane")]
    private string[] dialogueWakeUp;
    public Sprite ImageWakeUpSprite;

    [Header("Encounter Scene")]
    private string[] dialogueNoSwimming;
    private string[] dialogueBeach;
    private string[] dialogueTrees;
    private string[] dialoguePickingUpWatch;
    private string[] dialogueBeforePickingUpWallet;
    private string[] dialogueWithCindyAfterPickingUpWallet;
    private string[] afterFoundWatchAndTalkWithCindy;
    public bool foundWalletAndWatch = false;
    public GameObject walletAndWatchGO;
    public bool isDialogueWithCindy = false;

    [Header("Happiness Scene")]
    private string[] dialogueAlanCindyWalkTogether;
    public GameObject loopingBackgroundGO;
    public float distanceCindyAfterGoAway = 10f;
    // public bool isWalkTogether = false;

    [Header("MiniGame Scene")]
    private string[] dialogueRound1MiniGameTree;
    private string[] dialogueWrongTree;
    private string[] dialogueRound2MiniGameTree;
    private string[] dialogueFoundCindyRound2;
    private string[] dialogueCindyOnTop;
    private string[] thoughtsAfterMiniGameTree;
    public bool isFirstTree = true;
    public bool isRound1 = true;
    public bool isMiniGameTree = false;
    public int intTreeRandomizer;
    public int intCheckTree = 0;
    public Sprite ImageCindyOnTopSprite;

    [Header("Cindy Scene - Level 4")]
    private string[] dialogueAfterAlanDroppedWatch;
    public bool droppedWatch = false;

    // Start is called before the first frame update
    void Start()
    {

        flowTransitionChapter1.Add(NothingHappend); //0
        flowTransitionChapter1.Add(() => scriptTransitionFunction.TransitionCharacterText(scriptTransitionFunction.targetTextForTransition)); //1
        flowTransitionChapter1.Add(scriptTransitionFunction.Dialogue); //2
        flowTransitionChapter1.Add(scriptTransitionFunction.FadeInBlackscreenTransition); //3
        flowTransitionChapter1.Add(scriptTransitionFunction.FadeOutBlackscreenTransition); //4
        flowTransitionChapter1.Add(scriptTransitionFunction.FadeInImageSceneTransition); //5
        flowTransitionChapter1.Add(scriptTransitionFunction.FadeOutImageSceneTransition); //6
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
        flowFunctionsChapter1.Add(FadeInOutBlackScreeTransition); // 35
        flowFunctionsChapter1.Add(ChangeChapterToChapter2); // 36

        // InitialGame();
        // AddExpressionCharacter();
        InitialAddDialogue();

        scriptTransitionFunction.isChapter1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(scriptTransitionFunction.intFunctionNumbersNow != 0)
            flowFunctionsChapter1[scriptTransitionFunction.intFunctionNumbersNow]();

        if(scriptTransitionFunction.intTransitionNumbersNow != 0)
            flowTransitionChapter1[scriptTransitionFunction.intTransitionNumbersNow]();
    }

    void NothingHappend()
    {
        //do nothing
    }

    // void InitialGame()
    // {
    //     //inisialisasi game
    //     scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
    //     scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);

    //     imageScene.gameObject.SetActive(false);
    //     imageScene.color = new Color(255, 255, 255, 0);

    //     scriptTransitionFunction.intTransitionNumbersNow = 4;
    //     scriptTransitionFunction.intFunctionNumbersNow = 1;

    //     scriptTransitionFunction.thoughtText = scriptTransitionFunction.middleText;
    //     scriptTransitionFunction.targetTextForTransition = scriptTransitionFunction.thoughtText;
    // }

    void InitialAddDialogue()
    {
        familyPictureString = scriptDialogueChapter1.familyPictureString;
        certificateString = scriptDialogueChapter1.certificateString;
        dialogueWithParents1 = scriptDialogueChapter1.dialogueWithParents1;
        dialogueWithParents2 = scriptDialogueChapter1.dialogueWithParents2;
        dialogueBeforeExitMemories = scriptDialogueChapter1.dialogueBeforeExitMemories;
        dialogueAfterOpenDoor = scriptDialogueChapter1.dialogueAfterOpenDoor;

        dialogueWakeUp = scriptDialogueChapter1.dialogueWakeUp;
        dialogueNoSwimming = scriptDialogueChapter1.dialogueNoSwimming;
        dialogueBeach = scriptDialogueChapter1.dialogueBeach;
        dialogueTrees = scriptDialogueChapter1.dialogueTrees;
        dialoguePickingUpWatch = scriptDialogueChapter1.dialoguePickingUpWatch;
        dialogueBeforePickingUpWallet = scriptDialogueChapter1.dialogueBeforePickingUpWallet;
        dialogueWithCindyAfterPickingUpWallet = scriptDialogueChapter1.dialogueWithCindyAfterPickingUpWallet;
        afterFoundWatchAndTalkWithCindy = scriptDialogueChapter1.afterFoundWatchAndTalkWithCindy;

        dialogueAlanCindyWalkTogether = scriptDialogueChapter1.dialogueAlanCindyWalkTogether;

        dialogueRound1MiniGameTree = scriptDialogueChapter1.dialogueRound1MiniGameTree;
        dialogueWrongTree = scriptDialogueChapter1.dialogueWrongTree;
        dialogueRound2MiniGameTree = scriptDialogueChapter1.dialogueRound2MiniGameTree;
        dialogueFoundCindyRound2 = scriptDialogueChapter1.dialogueFoundCindyRound2;
        dialogueCindyOnTop = scriptDialogueChapter1.dialogueCindyOnTop;
        thoughtsAfterMiniGameTree = scriptDialogueChapter1.thoughtsAfterMiniGameTree;

        dialogueAfterAlanDroppedWatch = scriptDialogueChapter1.dialogueAfterAlanDroppedWatch;
    }
    public void TriggerObjectPhoto()
    {
        scriptTransitionFunction.characterDialogue = familyPictureString;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism();
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteFamilyPicture;
        scriptTransitionFunction.textNameObject.text = "Family Picture";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void TriggerObjectCertificate()
    {
        scriptTransitionFunction.characterDialogue = certificateString;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism();
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteCertificate;
        scriptTransitionFunction.textNameObject.text = "#1 Rank Certificate";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void TriggerObjectParent()
    {
        // scriptTransitionFunction.DefaultTriggerMechanism();
        if (!haveTalkedToParents)
        {
            scriptTransitionFunction.characterDialogue = dialogueWithParents1;
            haveTalkedToParents = true;
            canOpenDoor = true;
        }
        else
            scriptTransitionFunction.characterDialogue = dialogueWithParents2;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    public void TriggerObjectDoor()
    {
        if (canOpenDoor)
        {
            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.rb2D.velocity = Vector2.zero;
            scriptTransitionFunction.intTransitionNumbersNow = 3;
            scriptTransitionFunction.intFunctionNumbersNow = 2;
        }
        else
        {
            // scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.characterDialogue = dialogueBeforeExitMemories;
            // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.DefaultTriggerMechanism();
        }
    }

    public void TriggerObjectNoSwimmingSign()
    {
        scriptTransitionFunction.characterDialogue = dialogueNoSwimming;
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    public void TriggerObjectBeach()
    {
        scriptTransitionFunction.characterDialogue = dialogueBeach;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    public void TriggerObjectTrees(GameObject treeGameObject)
    {
        if (!isMiniGameTree)
        {
            scriptTransitionFunction.characterDialogue = dialogueTrees;
            scriptTransitionFunction.DefaultTriggerMechanism();
        }
        else
            TriggerObjectMiniGame(treeGameObject);
    }

    public void TriggerObjectWatch()
    {
        scriptTransitionFunction.characterDialogue = dialoguePickingUpWatch;
        foundWalletAndWatch = true;
        walletAndWatchGO.SetActive(false);
        scriptTransitionFunction.DefaultTriggerMechanism();
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteCindyWatch;
        scriptTransitionFunction.textNameObject.text = "Broken Watch";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void TriggerObjectCindy()
    {
        if (foundWalletAndWatch)
            scriptTransitionFunction.characterDialogue = dialogueWithCindyAfterPickingUpWallet;
        else
            scriptTransitionFunction.characterDialogue = dialogueBeforePickingUpWallet;
        isDialogueWithCindy = true;

        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    public void TriggerObjectMiniGame(GameObject treeGameObject)
    {
        playerGO.GetComponent<ObjectInterect>().ManualTriggerExit2DTree(treeGameObject);

        if (isRound1)
        {
            if (isFirstTree)
            {
                isFirstTree = false;
                scriptTransitionFunction.characterDialogue = dialogueWrongTree;
                // temp.tag = "Untagged";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
            }
            else
            {
                isRound1 = false;
                scriptTransitionFunction.characterDialogue = dialogueRound2MiniGameTree;
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
                scriptTransitionFunction.characterDialogue = dialogueFoundCindyRound2;
                isMiniGameTree = false;
                foreach (GameObject tree in playerGO.GetComponent<ObjectInterect>().tempTrees)
                    tree.tag = "Trees";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Clear();
                scriptTransitionFunction.DefaultTriggerMechanism();
                return;
            }
            else
            {
                scriptTransitionFunction.characterDialogue = dialogueWrongTree;
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
            }
            intCheckTree++;
        }
        scriptTransitionFunction.DefaultTriggerMechanism();
    }

    void OpeningAfterFadeOutBlackScreen()
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow = 0;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
        }
    }

    void DialogueWithCindyInBlackScreen() // 2 = Scene after open door / dialogue in black screen with cindya
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.characterDialogue = dialogueAfterOpenDoor;
            // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptPlayerMovement.canMove = false;
        }
    }

    void GoWakeUp() // 3 = Scene after dialogue with cindya in black screen
    {
        //do nothing
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.imageScene.gameObject.SetActive(true);
        // spriteImageScene = ImageWakeUpSprite;
        scriptTransitionFunction.imageScene.sprite = ImageWakeUpSprite;
        
        scriptTransitionFunction.intTransitionNumbersNow = 5;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptPlayerMovement.canMove = false;
    }

    void LookAtandFirstConversationWithCindy() // 4 = Scene after wake up
    {
        if (scriptTransitionFunction.imageSceneFadeIn)
        {
            scriptTransitionFunction.characterDialogue = dialogueWakeUp;
            // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.imageSceneFadeIn = false;
        }
    }

    void FinishedFirstConversation() // 5 = Scene after first conversation with cindy
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intTransitionNumbersNow = 6;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptPlayerMovement.canMove = false;
    }

    void StandUpAfterConversationWithCindy() // 6 = Scene after finished first conversation with cindy
    {
        if (scriptTransitionFunction.imageSceneFadeOut)
        {
            scriptTransitionFunction.intTransitionNumbersNow = 4; //fade out blackscreen
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.imageSceneFadeOut = false;

            homeAlanGO.SetActive(false);
            riverSideTreesGO.SetActive(true);

            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
        }
    }

    void TeleportRiverSideTrees() // 7 = Scene after stand up after conversation with cindy
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptPlayerMovement.canMove = true;
        }
    }

    void TalkWithCindy() // 8 = Scene after teleport to river side trees
    {
        if (!foundWalletAndWatch)
            return;

        if (!isDialogueWithCindy)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
    }

    void FinishedTalkWithCindy()
    {
        if (scriptTransitionFunction.isDialogue && isDialogueWithCindy)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptPlayerMovement.canMove = false;
    }

    void AlanThoughtAfterFoundWatch() // 10 = Scene after fade in to after talk
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;

            scriptTransitionFunction.characterThoughts = afterFoundWatchAndTalkWithCindy;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;
            scriptTransitionFunction.middleText.gameObject.SetActive(true);
        }
    }

    void AfterAlanThought() // 11 = Scene after Alan thought after found watch
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        
        loopingBackgroundGO.SetActive(true); // enable looping background
        GuideGO.SetActive(false);

        playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(5f, playerGO.transform.position.y);

        riverSideTreesGO.SetActive(false);
        // scriptPlayerMovement.canMove = false;
    }

    // Level: LC1_03 (Happiness)
    void FadeOutAfterThought() // 12
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;

            scriptTransitionFunction.characterDialogue = dialogueAlanCindyWalkTogether;
            scriptTransitionFunction.DefaultTriggerMechanism();
            // scriptPlayerMovement.canMove = false;

            // isWalkTogether = true;
        }
    }

    void WalkTogetherWithCindy() //13
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        
        // loopingBackgroundGO.SetActive(false);
        scriptPlayerMovement.canMove = false;
        cindyGO.GetComponent<PlayerMovement>().canMove = true;
        cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
        cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // GuideGO.SetActive(true);
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void CindyHidBehindTree() // 14
    {
        if (cindyGO.transform.position.x > distanceCindyAfterGoAway)
        {
            cindyGO.GetComponent<PlayerMovement>().canMove = false;
            cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
            cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            cindyGO.SetActive(false);
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 3;
            // GuideGO.SetActive(true);
            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        } 
    }

    void FadeInAfterCindyGoAway() // 15
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            loopingBackgroundGO.SetActive(false);
            parallaxBackgroundGO.SetActive(true);
            minigameTreeGO.SetActive(true);
            round1TreeGO.SetActive(true);
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 4;
        }
    }

    void FadeOutAfterCindyGoAway() // 16
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptPlayerMovement.canMove = true;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
        }
    }

    void FindCindyBehindTree() // 17
    {
        // scriptTransitionFunction.intFunctionNumbersNow = 0;
        //jika alan berada di posisi minigameTreeGO
        if (playerGO.transform.position.x >= centerMiniGameGO.transform.position.x)
        {
            scriptPlayerMovement.rb2D.velocity = Vector2.zero;

            scriptTransitionFunction.intFunctionNumbersNow++;
            GuideGO.SetActive(true);

            // riverSideTreesGO.SetActive(false);
            wallMinigameTreeGO.SetActive(true);
            //change follow target
            virtualCamera.m_Follow = centerMiniGameGO.transform;
            // scriptTransitionFunction.intTransitionNumbersNow = 5;
        }
    }

    void DialogueMiniGameTree() // 18
    {
        scriptTransitionFunction.characterDialogue = dialogueRound1MiniGameTree;
        scriptTransitionFunction.DefaultTriggerMechanism();

        scriptTransitionFunction.intFunctionNumbersNow++;
    }

    void StartMiniGameTree() // 19
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        isMiniGameTree = true;
    }

    void AfterFoundCindyInRound1() // 20
    {
        if (isRound1)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
    }

    void FadeInAfterRound1MiniGameTree() // 21
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 4;

            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
        }
    }

    void FadeOutAfterRound1MiniGameTree() // 22
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
            scriptPlayerMovement.canMove = true;
        }
    }

    void DialogueAfterRound1MiniGameTree() // 23
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptPlayerMovement.canMove = false;
    }

    void FadeInGoToRound2() // 24
    {
      if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 4;

            cindyGO.SetActive(false);
            wallMinigameTreeGO.SetActive(false);
            round1TreeGO.SetActive(false);
            round2TreeGO.SetActive(true);
            virtualCamera.m_Follow = playerGO.transform;
        }
    }

    void FadeOutGoToRound2() // 25
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow=0;
            scriptPlayerMovement.canMove = true;
        }
    }

    void FinalMiniGameTree() // 26
    {
        if (isMiniGameTree)
            return;
        
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptPlayerMovement.canMove = false;
    }

    void FadeInBlackScreenCutScene() // 27
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 5;

            scriptTransitionFunction.imageScene.gameObject.SetActive(true);
            // spriteImageScene = ImageCindyOnTopSprite;
            scriptTransitionFunction.imageScene.sprite = ImageCindyOnTopSprite;

            riverSideTreesGO.SetActive(true);
            minigameTreeGO.SetActive(false);
        }
    }

    void FadeInCutSceneOnTop() // 28
    {
        if (scriptTransitionFunction.imageSceneFadeIn)
        {
            scriptTransitionFunction.characterDialogue = dialogueCindyOnTop;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
            scriptTransitionFunction.imageSceneFadeIn = false;
        }

    }

    void DialogueInCutsceneOnTop() // 29
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 6;
        scriptPlayerMovement.canMove = false;
    }

    void FadeOutCutSceneOnTop() // 30
    {
        if (scriptTransitionFunction.imageSceneFadeOut)
        {
            scriptTransitionFunction.imageSceneFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;

            scriptTransitionFunction.characterThoughts = thoughtsAfterMiniGameTree;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;
            scriptTransitionFunction.middleText.gameObject.SetActive(true);

            playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);
        }
    }

    void ThoughtAfterOnTop()
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);


        cindyGO.SetActive(true);
        GuideGO.SetActive(false);
        
        playerGO.transform.position = new Vector2(10, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
    }

    void FadeOutBlackScreenOnTop() // 32
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 0;

            scriptPlayerMovement.canMove = true;
            scriptPlayerMovement.intAutoMovement = 2; // movet to left
            cindyGO.SetActive(true);
            cindyGO.GetComponent<PlayerMovement>().canMove = true;
            cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 2;
            cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void AlanDroppedWatch()
    {
        // input key a and d
        if (!droppedWatch)
        {
            if (playerGO.transform.position.x <= 0)
                droppedWatch = true;
        }
        else
        {
            scriptTransitionFunction.characterDialogue = dialogueAfterAlanDroppedWatch;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
    
            cindyGO.GetComponent<PlayerMovement>().canMove = false;
            cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 0;
            cindyGO.GetComponent<PlayerMovement>().rb2D.velocity = Vector2.zero;

            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.intAutoMovement = 0;

            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        }
    }

    void DialogueAfterDroppedWatch()
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptPlayerMovement.canMove = false;
    }

    void FadeInOutBlackScreeTransition()
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
        }
    }

    void ChangeChapterToChapter2()
    {
        SceneManager.LoadScene("Chapter2");
        PlayerPrefs.SetString("ChapterNow", "Chapter2");
    }
}