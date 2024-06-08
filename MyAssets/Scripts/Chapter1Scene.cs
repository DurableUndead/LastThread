using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class Chapter1Scene : MonoBehaviour
{
    private List<System.Action> flowFunctionsChapter1 = new List<System.Action>();
    private List<System.Action> flowTransitionChapter1 = new List<System.Action>();
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public CindyMovement scriptCindyMovement;
    public ObjectInterect scriptObjectInterect;
    private DialogueChapter1 scriptDialogueChapter1;
    private TransitionFunction scriptTransitionFunction;
    private PauseGameplay scriptPauseGameplay;
    private AudioManager scriptAudioManager;
    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("GameObjects")]
    public GameObject playerGO;
    public GameObject cindyGO;
    public GameObject shadingGO;

    [Header("Sprites For Dialogue")]
    public Sprite spriteTrophy;
    public Sprite spriteFamilyPicture;
    public Sprite spriteCindyWatch;

    [Header("Memories Scene")]
    public GameObject puzzleJigsawGO;
    public GameObject homeAlanGO;
    private string[] familyPictureString;
    private string[] trophyString;
    private string[] dialogueWithParents1;
    private string[] dialogueWithParents2;
    private string[] dialogueBeforeExitMemories;
    private string[] dialogueAfterOpenDoor;
    public bool haveTalkedToParents = false;
    public bool canOpenDoorAfterDialogue = false;
    public bool canOpenDoorAfterPuzzle = false;
    public bool puzzleJigsawSolved = false;

    [Header("Wake Up Scane")]
    private string[] dialogueWakeUp;
    public Sprite ImageWakeUpSprite;

    [Header("Encounter Scene")]
    public GameObject riverBankGO;
    public GameObject alanWalletGO;
    private string[] dialogueNoSwimming;
    private string[] dialogueBench;
    private string[] dialogueTrees;
    private string[] dialoguePickingUpWatch;
    private string[] dialogueBeforePickingUpWallet;
    private string[] dialogueWithCindyAfterPickingUpWallet;
    private string[] afterFoundWatchAndTalkWithCindy;
    public bool foundWalletAndWatch = false;
    public bool isDialogueWithCindy = false;

    [Header("Happiness Scene")]
    public GameObject exclamationMarkCindyGO;
    public GameObject loopingRiverBankGO;
    private string[] dialogueAlanCindyWalkTogether;
    // public GameObject loopingBackgroundGO;
    public float distanceCindyAfterGoAway = 10f;
    public bool isWalkTogether = false;

    [Header("MiniGame Scene")]
    public GameObject minigameTreeGO;
    public GameObject wallMinigameTreeGO;
    public GameObject round1TreeGO;
    public GameObject round2TreeGO;
    public GameObject centerMiniGameGO;
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
    public GameObject cindyWatchGO;
    public bool droppedWatch = false;
    public bool spawnWatch = true;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    [Header("Video Outro Gameplay")]
    public VideoClip videoClipOutro;
    // Start is called before the first frame update
    void Start()
    {
        scriptPauseGameplay = GetComponent<PauseGameplay>();
        scriptAudioManager = GetComponent<AudioManager>();
        scriptDialogueChapter1 = GetComponent<DialogueChapter1>();
        scriptTransitionFunction = GetComponent<TransitionFunction>();

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
        flowFunctionsChapter1.Add(StartMiniGameTree); // 18
        flowFunctionsChapter1.Add(AfterFoundCindyInRound1); // 19
        flowFunctionsChapter1.Add(FadeInAfterRound1MiniGameTree); // 20
        flowFunctionsChapter1.Add(FadeOutAfterRound1MiniGameTree); // 21
        flowFunctionsChapter1.Add(DialogueAfterRound1MiniGameTree); // 22
        flowFunctionsChapter1.Add(FadeInGoToRound2); // 23
        flowFunctionsChapter1.Add(FadeOutGoToRound2); // 24
        flowFunctionsChapter1.Add(FinalMiniGameTree); // 25
        flowFunctionsChapter1.Add(DialogueFinalMiniGame); // 26
        flowFunctionsChapter1.Add(FadeInBlackScreenCutScene); // 27
        flowFunctionsChapter1.Add(FadeInCutSceneOnTop); // 28
        flowFunctionsChapter1.Add(DialogueInCutsceneOnTop); // 29
        flowFunctionsChapter1.Add(FadeOutCutSceneOnTop); // 30
        flowFunctionsChapter1.Add(ThoughtAfterOnTop); // 31
        flowFunctionsChapter1.Add(FadeOutBlackScreenOnTop); // 32
        flowFunctionsChapter1.Add(AlanDroppedWatch); // 33
        flowFunctionsChapter1.Add(DialogueAfterDroppedWatch); // 34
        flowFunctionsChapter1.Add(EndGameplayChapter1); // 35

        // InitialGame();
        // AddExpressionCharacter();
        InitialAddDialogue();

        scriptTransitionFunction.isChapter1 = true;
        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);

        scriptPauseGameplay.StartInvokeESC();

        audioSource = scriptAudioManager.musicAudioSource;
        audioClip = scriptAudioManager.musicClips[0];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));

        scriptPlayerMovement.footstepSounds = scriptPlayerMovement.footstepWoodenFloor;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptPauseGameplay.isPaused)
            return;

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
        trophyString = scriptDialogueChapter1.trophyString;
        dialogueWithParents1 = scriptDialogueChapter1.dialogueWithParents1;
        dialogueWithParents2 = scriptDialogueChapter1.dialogueWithParents2;
        dialogueBeforeExitMemories = scriptDialogueChapter1.dialogueBeforeExitMemories;
        dialogueAfterOpenDoor = scriptDialogueChapter1.dialogueAfterOpenDoor;

        dialogueWakeUp = scriptDialogueChapter1.dialogueWakeUp;
        dialogueNoSwimming = scriptDialogueChapter1.dialogueNoSwimming;
        dialogueBench = scriptDialogueChapter1.dialogueBench;
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
        if (!puzzleJigsawSolved)
        {
            puzzleJigsawGO.SetActive(true);
            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.StopWalkingOrRunning();
            scriptObjectInterect.enabled = false;
            return;
        }
        
        
        scriptTransitionFunction.characterDialogue = familyPictureString;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism(true);
        scriptTransitionFunction.bgObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteFamilyPicture;
        scriptTransitionFunction.textNameObject.text = "Family Picture";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void ClosePuzzleJigsaw(bool puzzleWin=false)
    {
        puzzleJigsawGO.SetActive(false);
        scriptPlayerMovement.canMove = true;
        scriptObjectInterect.enabled = true;

        if (!puzzleWin)
            return;
        canOpenDoorAfterPuzzle = true;
        puzzleJigsawSolved = true;
        TriggerObjectPhoto();
    }

    public void TriggerObjectTrophy()
    {
        scriptTransitionFunction.characterDialogue = trophyString;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism(true);
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteTrophy;
        scriptTransitionFunction.textNameObject.text = "My Trophy";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void TriggerObjectParent()
    {
        // scriptTransitionFunction.DefaultTriggerMechanism();
        if (!haveTalkedToParents)
        {
            scriptTransitionFunction.characterDialogue = dialogueWithParents1;
            haveTalkedToParents = true;
            canOpenDoorAfterDialogue = true;
        }
        else
            scriptTransitionFunction.characterDialogue = dialogueWithParents2;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism(true);
    }

    public void TriggerObjectDoor()
    {
        if (canOpenDoorAfterDialogue && canOpenDoorAfterPuzzle)
        {
            scriptObjectInterect.canInteract = false;
            scriptObjectInterect.currentCollider = null;

            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.rb2D.velocity = Vector2.zero;
            scriptPlayerMovement.StopWalkingOrRunning();
            scriptTransitionFunction.intTransitionNumbersNow = 0;
            // scriptTransitionFunction.intFunctionNumbersNow = 0;

            // Audio for opening door
            //play oneshoot
            scriptAudioManager.gameplayAudioSource.PlayOneShot(scriptAudioManager.gameplayClips[0]);
            scriptTransitionFunction.intTransitionNumbersNow = 3;
            scriptTransitionFunction.intFunctionNumbersNow = 2;
            // Invoke("IEGoToWakeUp", scriptAudioManager.gameplayClips[0].length);

            // scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[0];
            // scriptAudioManager.gameplayAudioSource.Play();
            
        }
        else
        {
            // scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.characterDialogue = dialogueBeforeExitMemories;
            // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.DefaultTriggerMechanism(true);
        }
    }

    public void TriggerObjectNoSwimmingSign()
    {
        scriptTransitionFunction.characterDialogue = dialogueNoSwimming;
        scriptTransitionFunction.DefaultTriggerMechanism(true);
    }

    public void TriggerObjectBench()
    {
        scriptTransitionFunction.characterDialogue = dialogueBench;
        // dialogueText.text = scriptTransitionFunction.characterDialogue[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.DefaultTriggerMechanism(true);
    }

    public void TriggerObjectTrees(GameObject treeGameObject)
    {
        if (!isMiniGameTree)
        {
            scriptTransitionFunction.characterDialogue = dialogueTrees;
            scriptTransitionFunction.DefaultTriggerMechanism(true);
        }
        else
            TriggerObjectMiniGame(treeGameObject);
    }

    public void TriggerObjectWatch()
    {
        scriptTransitionFunction.characterDialogue = dialoguePickingUpWatch;
        foundWalletAndWatch = true;
        alanWalletGO.SetActive(false);
        scriptTransitionFunction.DefaultTriggerMechanism(true);
        scriptTransitionFunction.ObjectImageGO.SetActive(true);
        scriptTransitionFunction.ObjectImageSprite.sprite = spriteCindyWatch;
        scriptTransitionFunction.textNameObject.text = "Broken Watch";
        scriptTransitionFunction.isInteractObjectImage = true;
    }

    public void TriggerObjectCindy()
    {
        bool canMove = false;
        if (foundWalletAndWatch)
            scriptTransitionFunction.characterDialogue = dialogueWithCindyAfterPickingUpWallet;
        else
        {
            scriptTransitionFunction.characterDialogue = dialogueBeforePickingUpWallet;
            canMove = true;
        }
        isDialogueWithCindy = true;

        scriptTransitionFunction.DefaultTriggerMechanism(canMove);
    }

    public void TriggerObjectMiniGame(GameObject treeGameObject)
    {
        playerGO.GetComponent<ObjectInterect>().ManualTriggerExit2DTree(treeGameObject);
        bool canMove = false;
        if (isRound1)
        {
            if (isFirstTree)
            {
                isFirstTree = false;
                scriptTransitionFunction.characterDialogue = dialogueWrongTree;
                // temp.tag = "Untagged";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
                canMove = true;
            }
            else
            {
                isRound1 = false;
                // scriptTransitionFunction.characterDialogue = dialogueRound2MiniGameTree;
                intTreeRandomizer = Random.Range(2, 10);
                // isFirstTree = true;
                // temp.tag = "Trees";

                //remove all game objects trees
                foreach (GameObject tree in playerGO.GetComponent<ObjectInterect>().tempTrees)
                    tree.tag = "Trees";
                playerGO.GetComponent<ObjectInterect>().tempTrees.Clear();
                scriptPlayerMovement.canMove = false;
                return;
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
                scriptObjectInterect.enabled = false;
                scriptObjectInterect.canDetectObject = false;
                return;
            }
            else
            {
                scriptTransitionFunction.characterDialogue = dialogueWrongTree;
                playerGO.GetComponent<ObjectInterect>().tempTrees.Add(treeGameObject);
                treeGameObject.tag = "Untagged";
                canMove = true;
            }
            intCheckTree++;
        }
        scriptTransitionFunction.DefaultTriggerMechanism(canMove);
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
            StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
            Invoke("DelayGoToDialogue", scriptAudioManager.gameplayClips[0].length - scriptTransitionFunction.fadeInBlackscreen);
            scriptTransitionFunction.blackscreenFadeIn = false;
            // scriptPlayerMovement.canMove = false;
            scriptTransitionFunction.intFunctionNumbersNow = 0;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
        }
    }

    void DelayGoToDialogue()
    {
        scriptTransitionFunction.characterDialogue = dialogueAfterOpenDoor;
        scriptTransitionFunction.DefaultTriggerMechanism();
        scriptTransitionFunction.intFunctionNumbersNow = 3;
        scriptTransitionFunction.intTransitionNumbersNow = 2;
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
        // scriptPlayerMovement.canMove = false;
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

            audioClip = scriptAudioManager.musicClips[1];
            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
        }
    }

    void FinishedFirstConversation() // 5 = Scene after first conversation with cindy
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intTransitionNumbersNow = 6;
        scriptTransitionFunction.intFunctionNumbersNow++;
        // scriptPlayerMovement.canMove = false;

        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
    }

    void StandUpAfterConversationWithCindy() // 6 = Scene after finished first conversation with cindy
    {
        if (scriptTransitionFunction.imageSceneFadeOut)
        {
            Invoke("DelayGoToTeleportRiverSideTrees", 0.5f);
            scriptTransitionFunction.intTransitionNumbersNow = 0; // 4; //fade out blackscreen
            scriptTransitionFunction.intFunctionNumbersNow = 0;
            scriptTransitionFunction.imageSceneFadeOut = false;

            homeAlanGO.SetActive(false);
            riverBankGO.SetActive(true);

            cindyGO.SetActive(true);
            playerGO.transform.position = new Vector2(17f, playerGO.transform.position.y);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
            scriptCindyMovement.StopMovement();

            scriptPlayerMovement.footstepSounds = scriptPlayerMovement.footstepOnGrass;
            shadingGO.SetActive(true);
        }
    }

    void DelayGoToTeleportRiverSideTrees()
    {
        scriptTransitionFunction.intTransitionNumbersNow = 4; //fade out blackscreen
        scriptTransitionFunction.intFunctionNumbersNow = 7;

        scriptCindyMovement.cindyLookAtTransform = playerGO.transform;
    }

    void TeleportRiverSideTrees() // 7 = Scene after stand up after conversation with cindy
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intTransitionNumbersNow = 0;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptPlayerMovement.canMove = true;

            //midnight ambient jika tidak ada music yang sedang dimainkan
            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[0];
            scriptAudioManager.ambientAudioSource.Play();
        }
    }

    void TalkWithCindy() // 8 = Scene after teleport to river side trees
    {
        if (!foundWalletAndWatch)
            return;

        if (!isDialogueWithCindy)
            return;

        scriptCindyMovement.cindyLookAtTransform = null;
        scriptTransitionFunction.intFunctionNumbersNow++;

        scriptAudioManager.ambientAudioSource.Stop();
        audioClip = scriptAudioManager.musicClips[2];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));

        scriptObjectInterect.canInteract = false;
    }

    void FinishedTalkWithCindy()
    {
        if (scriptTransitionFunction.isDialogue && isDialogueWithCindy)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        // scriptPlayerMovement.canMove = false;

        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
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
        
        riverBankGO.SetActive(false);
        loopingRiverBankGO.SetActive(true);

        playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(5f, playerGO.transform.position.y);

        audioClip = scriptAudioManager.musicClips[3];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
        scriptPlayerMovement.WalkInPlace("Right");
        scriptCindyMovement.ChangeMovementType(directionMove: "Right", walkInPlace: true);
        scriptCindyMovement.animatorCindy.speed = 1f;
        scriptPlayerMovement.animator.speed = 0.7f;

        isWalkTogether = true;
        scriptObjectInterect.canDetectObject = false;
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
        }
    }

    void WalkTogetherWithCindy() //13
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        // cindyGO.transform.GetChild(1).gameObject.SetActive(true); // enable exclamation mark
        exclamationMarkCindyGO.SetActive(true);
        
        // loopingBackgroundGO.SetActive(false);
        // scriptPlayerMovement.canMove = false;
        // scriptCindyMovement.intAutoMovement = 1;
        scriptCindyMovement.canMove = true;
        scriptCindyMovement.animatorCindy.speed = 1.5f;
        scriptCindyMovement.ChangeMovementType("Right", true);
        cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
        cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        // GuideGO.SetActive(true);
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void CindyHidBehindTree() // 14
    {
        if (cindyGO.transform.position.x > distanceCindyAfterGoAway)
        {
            scriptCindyMovement.canMove = false;
            cindyGO.GetComponent<CapsuleCollider2D>().isTrigger = false;
            cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            // cindyGO.transform.GetChild(1).gameObject.SetActive(false);
            exclamationMarkCindyGO.SetActive(false);
            cindyGO.SetActive(false);
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 3;
            // GuideGO.SetActive(true);
            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
            isWalkTogether = false;
            scriptCindyMovement.animatorCindy.speed = 1f;
        } 
    }

    void FadeInAfterCindyGoAway() // 15
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            loopingRiverBankGO.SetActive(false);
            // parallaxBackgroundGO.SetActive(true);
            minigameTreeGO.SetActive(true);
            round1TreeGO.SetActive(true);
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 4;

            scriptPlayerMovement.StopWalkingOrRunning();
            scriptPlayerMovement.animator.SetTrigger("Idle");
            scriptPlayerMovement.animator.ResetTrigger("Walk");
            StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
            //midnight ambient jika tidak ada music yang sedang dimainkan
            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[0];
            scriptAudioManager.ambientAudioSource.Play();
            scriptPlayerMovement.animator.speed = 1f;
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
            scriptObjectInterect.canDetectObject = true;
        }
    }

    void FindCindyBehindTree() // 17
    {
        // scriptTransitionFunction.intFunctionNumbersNow = 0;
        //jika alan berada di posisi minigameTreeGO
        if (playerGO.transform.position.x >= centerMiniGameGO.transform.position.x)
        {
            // scriptPlayerMovement.rb2D.velocity = Vector2.zero;
            scriptPlayerMovement.StopWalkingOrRunning();

            scriptTransitionFunction.intFunctionNumbersNow++;
            // GuideGO.SetActive(true);

            // riverBankGO.SetActive(false);
            wallMinigameTreeGO.SetActive(true);
            //change follow target
            virtualCamera.m_Follow = centerMiniGameGO.transform;
            // scriptTransitionFunction.intTransitionNumbersNow = 5;

            audioClip = scriptAudioManager.musicClips[4];
            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
            //stop ambient
            scriptAudioManager.ambientAudioSource.Stop();
            scriptTransitionFunction.characterDialogue = dialogueRound1MiniGameTree;
            scriptTransitionFunction.DefaultTriggerMechanism(true);
        }
    }

    // // void DialogueMiniGameTree() // 18
    // {
    //     scriptTransitionFunction.characterDialogue = dialogueRound1MiniGameTree;
    //     scriptTransitionFunction.DefaultTriggerMechanism(true);

    //     // scriptTransitionFunction.intFunctionNumbersNow++;

    //     // // audioClip = scriptAudioManager.musicClips[3];
    // }

    void StartMiniGameTree() // 18
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        isMiniGameTree = true;
    }

    void AfterFoundCindyInRound1() // 19
    {
        if (isRound1)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptObjectInterect.canDetectObject = false;
        // scriptPlayerMovement.canMove = false;


    }

    void FadeInAfterRound1MiniGameTree() // 20
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 4;

            cindyGO.SetActive(true);
            cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 2, cindyGO.transform.position.y);
            scriptCindyMovement.CindyLookAtTarget(playerGO.transform);
            scriptPlayerMovement.LookAtTarget(cindyGO.transform);
        }
    }

    void FadeOutAfterRound1MiniGameTree() // 21
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;

            scriptTransitionFunction.characterDialogue = dialogueRound2MiniGameTree;
            scriptTransitionFunction.DefaultTriggerMechanism();
        }
    }

    void DialogueAfterRound1MiniGameTree() // 22
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        // scriptPlayerMovement.canMove = false;
    }

    void FadeInGoToRound2() // 23
    {
      if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);
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

    void FadeOutGoToRound2() // 24
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow=0;
            scriptPlayerMovement.canMove = true;
            scriptObjectInterect.canDetectObject = true;
        }
    }

    void FinalMiniGameTree() // 25
    {
        if (isMiniGameTree)
            return;
        scriptTransitionFunction.intFunctionNumbersNow++;

        // if (scriptTransitionFunction.isDialogue)
        //     return;

        
        // scriptTransitionFunction.intTransitionNumbersNow = 3;
        // scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        // scriptPlayerMovement.canMove = false;

        // StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
    }

    void DialogueFinalMiniGame() // 26
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptTransitionFunction.intFunctionNumbersNow = 0; //++;
        scriptTransitionFunction.intTransitionNumbersNow = 0; // 3;
        // scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        // scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);
        scriptTransitionFunction.blackscreenFadeIn = true;

        // StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
        audioSource.Stop();
        audioClip = null;
        audioSource.clip = null;
        Invoke("GoToFadeInBlackScreenCutScene", 0.5f);
    }

    void GoToFadeInBlackScreenCutScene()
    {
        scriptTransitionFunction.intFunctionNumbersNow = 27;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
    }

    void FadeInBlackScreenCutScene() // 27
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow = 0; //++;
            scriptTransitionFunction.intTransitionNumbersNow = 0; //5;

            // scriptAudioManager.gameplayAudioSource.PlayOneShot(scriptAudioManager.gameplayClips[1]);
            scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[1];
            scriptAudioManager.gameplayAudioSource.Play();
            StartCoroutine(GoToFadeInCutSceneOnTop());

            // spriteImageScene = ImageCindyOnTopSprite;
            scriptTransitionFunction.imageScene.sprite = ImageCindyOnTopSprite;

            riverBankGO.SetActive(true);
            minigameTreeGO.SetActive(false);


        }
    }

    IEnumerator GoToFadeInCutSceneOnTop()
    {
        while (scriptAudioManager.gameplayAudioSource.isPlaying)
        {
            yield return null;
        }

        scriptTransitionFunction.intFunctionNumbersNow = 28;
        scriptTransitionFunction.intTransitionNumbersNow = 5;
        scriptTransitionFunction.imageScene.gameObject.SetActive(true);

        audioClip = scriptAudioManager.musicClips[5];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
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
        // scriptPlayerMovement.canMove = false;
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

            StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
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
        // GuideGO.SetActive(false);
        
        playerGO.transform.position = new Vector2(15, playerGO.transform.position.y);
        cindyGO.transform.position = new Vector2(playerGO.transform.position.x + 3f, cindyGO.transform.position.y);

        scriptPlayerMovement.canMove = true;
        scriptPlayerMovement.intAutoMovement = 2; // movet to left
        scriptPlayerMovement.animator.speed = 0.7f;
        scriptCindyMovement.canMove = true;
        // scriptCindyMovement.intAutoMovement = 2;
        scriptCindyMovement.ChangeMovementType("Left", true);
        scriptCindyMovement.speedMovement = 3f;
        cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        scriptObjectInterect.enabled = false;
        scriptObjectInterect.canDetectObject = false;

        // scriptPlayerMovement.animator.SetTrigger("Walk");
        // scriptPlayerMovement.animator.ResetTrigger("Idle");
        // scriptPlayerMovement.characterBodyTransform.localScale = scriptPlayerMovement.facingLeft;
        // scriptPlayerMovement.WalkInPlace();
        scriptTransitionFunction.FadeInCinematicBarTransition();
    }

    void FadeOutBlackScreenOnTop() // 32
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 0;

            // scriptPlayerMovement.canMove = true;
            // scriptPlayerMovement.intAutoMovement = 2; // movet to left
            // cindyGO.SetActive(true);
            // scriptCindyMovement.canMove = true;
            // // scriptCindyMovement.intAutoMovement = 2;
            // cindyGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void AlanDroppedWatch()
    {
        // input key a and d
        if (!droppedWatch)
        {
            if (playerGO.transform.position.x <= 0)
            {
                droppedWatch = true;
                scriptTransitionFunction.FadeOutCinematicBarTransition();
                return;
            }
            if (playerGO.transform.position.x <= 2)
            {
                if (!spawnWatch)
                    return;
                spawnWatch = false;
                cindyWatchGO.SetActive(true);
                cindyWatchGO.transform.position = new Vector2(playerGO.transform.position.x, cindyWatchGO.transform.position.y);
                scriptAudioManager.PlayTriggerOrNextSoundUI(); //sound dropped watch

                audioClip = scriptAudioManager.musicClips[6];
                StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
            }
        }
        else
        {
            // scriptPlayerMovement.StopWalkingOrRunning(true, "right");
            scriptTransitionFunction.characterDialogue = dialogueAfterAlanDroppedWatch;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
    
            scriptCindyMovement.canMove = false;
            // // scriptCindyMovement.intAutoMovement = 0;
            scriptCindyMovement.rb2D.velocity = Vector2.zero;

            // scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.intAutoMovement = 0;

            scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[1];
            scriptAudioManager.ambientAudioSource.Play();
            scriptPlayerMovement.StopWalkingOrRunning(mirrorDirection: true);
            scriptCindyMovement.StopMovement();
        }
    }

    void DialogueAfterDroppedWatch()
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++; //35;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        // scriptPlayerMovement.canMove = false;

        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));

        // sound tense flat line
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[2];
        scriptAudioManager.gameplayAudioSource.Play();
        scriptTransitionFunction.fadeInBlackscreen = scriptAudioManager.gameplayAudioSource.clip.length - scriptTransitionFunction.fadeInBlackscreen;
    }

    void SoundPlayBodyDrop()
    {
        //sound body drop
        scriptAudioManager.gameplayAudioSource.PlayOneShot(scriptAudioManager.gameplayClips[3]);
    }

    void EndGameplayChapter1()
    {
        if (scriptTransitionFunction.blackscreenFadeIn)
        {
            // Stop All Audio
            scriptAudioManager.musicAudioSource.Stop();
            scriptAudioManager.ambientAudioSource.Stop();

            scriptTransitionFunction.fadeInBlackscreen = scriptTransitionFunction.fadeOutBlackscreen;
            Invoke("SoundPlayBodyDrop", scriptTransitionFunction.fadeInBlackscreen);
            scriptTransitionFunction.blackscreenFadeIn = false;
            scriptTransitionFunction.intFunctionNumbersNow = 0;
            scriptTransitionFunction.intTransitionNumbersNow = 0;

            // Invoke("NextChapterToChapter2", 3f);
            // Debug.Log("End of Chapter 1");
            // videoPlayer.gameObject.SetActive(true);
            Invoke("GoToOutroVideoChapter1", 1f);
        }
    }

    void GoToOutroVideoChapter1()
    {
        scriptPauseGameplay.SetVideoClip(videoClipOutro);
    }


    // void GoToOutroVideoChapter1()
    // {
    //     videoPlayer.clip = videoClipOutro;
    //     videoPlayer.Play();
    //     Invoke("CheckIsPlayingOutro", 1f);
    // }

    // void CheckIsPlayingOutro()
    // {
    //     StartCoroutine(GoToNextChapterToChapter2());
    // }

    // IEnumerator GoToNextChapterToChapter2()
    // {
    //     while (videoPlayer.isPlaying)
    //     {
    //         yield return null;
    //     }
    //     Invoke("NextChapterToChapter2", 3f);
    // }

    // void NextChapterToChapter2()
    // {
    //     SceneManager.LoadScene("Chapter2");
    //     PlayerPrefs.SetString("ChapterNow", "Chapter2");
    // }
}