using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Video;

public class Chapter0Scene : MonoBehaviour
{
    private List<System.Action> flowFunctionsChapter0 = new List<System.Action>();
    private List<System.Action> flowTransitionChapter0 = new List<System.Action>();

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("Character GameObject")]
    public GameObject playerGO;
    public GameObject cindyGO;
    public GameObject stalkerGO;
    public Vector3 playerPositionSpawn;

    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    public CindyMovement scriptCindyMovement;
    private OpeningGameplay scriptOpeningGameplay;
    private GuideScript scriptGuide;
    private TransitionFunction scriptTransitionFunction;
    private DialogueChapter0 scriptDialogueChapter0;
    private PauseGameplay scriptPauseGameplay;
    private AudioManager scriptAudioManager;
    [Header("GameObjects")]
    public Image blackscreenImage;
    public GameObject bgCityGO;
    public GameObject bgBridgeAGO;
    public GameObject bgBridgeBGO;
    public GameObject bgRiverBankGO;
    public GameObject canvasForRiverBankGO;

    [Header("Alan Scene")]
    private string[] alanBlackScreenScene;
    private string[] alanDialogueWithBoss;
    private string[] thoughts1;
    [SerializeField] float maxDistanceThoughts = 10f;
    private string[] thoughts2;
    private string[] alanDialogueOnBridge;
    private string[] alanThoughtsBridge;

    [Header("Bridge Scene")]
    public GameObject bridgeGO;
    public GameObject bridgeSpawn;
    public GameObject roadGO;
    public GameObject faceAlanGO;
    public GameObject blurEffectGO;
    public Text textBridge;
    public bool canLoop = true;

    //Cindy
    [Header("Cindy Scene")]
    private string[] cindyThoughtBlackScene;
    private string[] cindyThoughtWalking;
    private string[] cindyDialogueBehindTree;
    public float stalkerDistanceAway = 15f;
    public bool cindyMoved = false;
    public float roamingTimeCindy = 10f;
    public GameObject exclamationMarkGO;
    [Header("Cindy After Hiding")]
    public GameObject shadingRiverBankB2GO;
    public GameObject riverBankB2GO;
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
        scriptDialogueChapter0 = GetComponent<DialogueChapter0>();
        scriptGuide = GetComponent<GuideScript>();
        scriptOpeningGameplay = GetComponent<OpeningGameplay>();
        scriptTransitionFunction = GetComponent<TransitionFunction>();

        blackscreenImage.gameObject.SetActive(true);

        flowFunctionsChapter0.Add(NothingHappend); // 0
        flowFunctionsChapter0.Add(AlanThougtsBlackScreen); // 1
        flowFunctionsChapter0.Add(GoToDialogueWithBoss); // 2
        flowFunctionsChapter0.Add(GoToFadeOutBlackScreen); // 3
        flowFunctionsChapter0.Add(GoToAutoWalk); // 4
        flowFunctionsChapter0.Add(AutoWalkAndThoughts); // 5
        flowFunctionsChapter0.Add(FadeInBlackScreenAfterThought1); // 6
        flowFunctionsChapter0.Add(FadeOutBlackScreenGoToThought2); // 7
        flowFunctionsChapter0.Add(GoToThought2); // 8
        flowFunctionsChapter0.Add(FadeInBlackScreenAfterThought2); // 9
        flowFunctionsChapter0.Add(FadeOutBlackScreenGoToBridge); // 10
        flowFunctionsChapter0.Add(GoToDialogueOnBridge); // 11
        flowFunctionsChapter0.Add(GoToFadeInJump); // 12
        flowFunctionsChapter0.Add(FadeInGuideJump); // 13
        flowFunctionsChapter0.Add(StartThoughtOnBridge); // 14
        flowFunctionsChapter0.Add(EndThoughtToJump); // 15
        flowFunctionsChapter0.Add(CindyThoughtBlackScreen); // 16
        flowFunctionsChapter0.Add(FadeOutAfterCindyThoughtBlackScreen); // 17
        flowFunctionsChapter0.Add(GoToCindyThoughtWithRunning); // 18
        flowFunctionsChapter0.Add(CindyThoughtWithRunning); // 19
        flowFunctionsChapter0.Add(FadeInToHiddenBehindTree); // 20
        flowFunctionsChapter0.Add(FadeOutToHiddenBehindTree); // 21
        flowFunctionsChapter0.Add(CindyDialogueAfterStalkerAway); // 22
        flowFunctionsChapter0.Add(StalkerWalkingGoToFadeIn); // 23
        flowFunctionsChapter0.Add(GoToFadeOutAfterStalker); // 24
        flowFunctionsChapter0.Add(FadeOutAfterStalker); // 25
        flowFunctionsChapter0.Add(CindyRoaming); // 26
        flowFunctionsChapter0.Add(EndGameplayChapter0); // 27


        flowTransitionChapter0.Add(NothingHappend); //0
        flowTransitionChapter0.Add(() => scriptTransitionFunction.TransitionCharacterText(scriptTransitionFunction.targetTextForTransition)); //1
        flowTransitionChapter0.Add(scriptTransitionFunction.Dialogue); //2
        flowTransitionChapter0.Add(scriptTransitionFunction.FadeInBlackscreenTransition); //3
        flowTransitionChapter0.Add(scriptTransitionFunction.FadeOutBlackscreenTransition); //4
        flowTransitionChapter0.Add(scriptTransitionFunction.FadeInImageSceneTransition); //5
        flowTransitionChapter0.Add(scriptTransitionFunction.FadeOutImageSceneTransition); //6
        flowTransitionChapter0.Add(() => scriptTransitionFunction.TransitionsPerSentenceDialogue(scriptTransitionFunction.targetTextForTransition)); //7
        flowTransitionChapter0.Add(scriptTransitionFunction.TransitionTMPCharacter); //8

        playerPositionSpawn = playerGO.transform.position;

        InitialAddDialogue();

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
        scriptTransitionFunction.isChapter1 = false;

        audioSource = scriptAudioManager.musicAudioSource;
        audioClip = scriptAudioManager.musicClips[0];
        // volumeAudio = scriptAudioManager.volumeMusicNow;
        scriptPlayerMovement.footstepSounds = scriptPlayerMovement.footstepSideWalk;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptPauseGameplay.isPaused)
            return;
            
        if (scriptTransitionFunction.intFunctionNumbersNow != 0)
            flowFunctionsChapter0[scriptTransitionFunction.intFunctionNumbersNow]();

        if (scriptTransitionFunction.intTransitionNumbersNow != 0)
            flowTransitionChapter0[scriptTransitionFunction.intTransitionNumbersNow]();
    }
    void InitialAddDialogue()
    {
        alanBlackScreenScene = scriptDialogueChapter0.alanBlackScreenScene;
        alanDialogueWithBoss = scriptDialogueChapter0.alanDialogueWithBoss;
        thoughts1 = scriptDialogueChapter0.thoughts1;
        thoughts2 = scriptDialogueChapter0.thoughts2;
        alanDialogueOnBridge = scriptDialogueChapter0.alanDialogueOnBridge;
        alanThoughtsBridge = scriptDialogueChapter0.alanThoughtsBridge;
        
        cindyThoughtBlackScene = scriptDialogueChapter0.cindyThoughtBlackScene;
        cindyThoughtWalking = scriptDialogueChapter0.cindyThoughtWalking;
        cindyDialogueBehindTree = scriptDialogueChapter0.cindyDialogueBehindTree;
    }

    void NothingHappend()
    {
        return;
    }

    public void GoToOpeningAlanThoughts()
    {
        scriptGuide.iconA.gameObject.SetActive(false);
        scriptGuide.iconD.gameObject.SetActive(false);

        scriptAudioManager.musicAudioSource.clip = scriptAudioManager.gameplayClips[0];
        scriptAudioManager.musicAudioSource.PlayOneShot(scriptAudioManager.musicAudioSource.clip);

        scriptTransitionFunction.intFunctionNumbersNow = 1;
        scriptTransitionFunction.intTransitionNumbersNow = 1;
    }

    void AlanThougtsBlackScreen()
    {
        scriptTransitionFunction.characterThoughts = alanBlackScreenScene;
        scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.isThought = true;
        scriptTransitionFunction.middleText.gameObject.SetActive(true);

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 1;
    }

    void GoToDialogueWithBoss()
    {
        if (scriptTransitionFunction.isThought)
            return;
        scriptAudioManager.musicAudioSource.Stop();

        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[1];
        scriptAudioManager.gameplayAudioSource.Play();

        scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[0]; //heart sound
        scriptAudioManager.ambientAudioSource.Play();

        scriptTransitionFunction.characterDialogue = alanDialogueWithBoss;
        scriptTransitionFunction.DefaultTriggerMechanism();

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 2;
    }

    void GoToFadeOutBlackScreen()
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptAudioManager.gameplayAudioSource.Stop();
        scriptAudioManager.ambientAudioSource.Stop();
 
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        scriptPauseGameplay.StartInvokeESC();
        scriptGuide.FadeInGuide();
        scriptPlayerMovement.StopWalkingOrRunning();
    }

    void GoToAutoWalk()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;
        StartCoroutine(scriptGuide.FadeInGuide());

        scriptTransitionFunction.blackscreenFadeOut = false;
        scriptOpeningGameplay.enabled = true;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void AutoWalkAndThoughts()
    {
        if (playerGO.transform.position.x > playerPositionSpawn.x + maxDistanceThoughts)
        {
            scriptGuide.IfPlayerDoesNotPressAD();

            scriptPlayerMovement.isMoving = false;
            scriptPlayerMovement.isStepPlaying = false;
            // scriptPlayerMovement.animator.speed = 0.62f;
            // scriptPlayerMovement.speedMovement = 2f;

            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[1];
            scriptAudioManager.ambientAudioSource.Play();

            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));


            // scriptTransitionFunction.middleText.gameObject.SetActive(false);
            // scriptTransitionFunction.topMiddleText.gameObject.SetActive(true);
            // scriptTransitionFunction.thoughtText = scriptTransitionFunction.topMiddleText;
            // scriptTransitionFunction.targetTextForTransition = scriptTransitionFunction.thoughtText;

            // scriptTransitionFunction.middleText.transform.position = new Vector2(0, 200f);
            scriptTransitionFunction.middleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200f);
            scriptPlayerMovement.intAutoMovement = 1;
            scriptTransitionFunction.characterThoughts = thoughts1;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;

            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;

            scriptTransitionFunction.FadeInCinematicBarTransition();
        }
    }

    void FadeInBlackScreenAfterThought1()
    {
        if (scriptTransitionFunction.isThought)
            return;

        string status = "Pause";
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow, status));

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
    }

    void FadeOutBlackScreenGoToThought2()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        //Back Position
        playerGO.transform.position = playerPositionSpawn;
        Invoke("GoToAlanThought2", 0.5f);

        bgBridgeAGO.SetActive(true);
        bgCityGO.SetActive(false);
        scriptPlayerMovement.speedMovement = 2f;
        playerGO.GetComponent<PlayerMovement>().animator.speed = 0.7f;
    }

    void GoToAlanThought2()
    {
        string status= "UnPause";
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow, status));
        
        scriptTransitionFunction.intFunctionNumbersNow = 8;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
    }

    void GoToThought2()
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;

            scriptPlayerMovement.intAutoMovement = 1;
            scriptTransitionFunction.characterThoughts = thoughts2;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;
        }
    }

    void FadeInBlackScreenAfterThought2()
    {
        if (scriptTransitionFunction.isThought)
            return;

        playerGO.GetComponent<PlayerMovement>().animator.speed = 1f;

        string status = "Stop";
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow, status));
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        scriptTransitionFunction.FadeOutCinematicBarTransition();
    }

    void FadeOutBlackScreenGoToBridge()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;
        scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[2];
        scriptAudioManager.ambientAudioSource.Play();

        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[2];
        scriptAudioManager.gameplayAudioSource.Play();

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        //Teleport to Bridge
        scriptPlayerMovement.canMove = false;
        scriptPlayerMovement.intAutoMovement = 0;
        scriptPlayerMovement.rb2D.velocity = Vector2.zero;
        bridgeGO.SetActive(true);
        playerGO.transform.position = bridgeSpawn.transform.position;
        roadGO.SetActive(false);
        Invoke("delayGoToTeleportToBridge", 0.5f);

        bgBridgeAGO.SetActive(false);
        bgBridgeBGO.SetActive(true);

        virtualCamera.Follow = faceAlanGO.transform;
        virtualCamera.m_Lens.OrthographicSize = 1f;

        playerGO.GetComponent<PlayerMovement>().animator.ResetTrigger("Walk");
        playerGO.GetComponent<PlayerMovement>().animator.SetTrigger("Idle");

        blurEffectGO.SetActive(true);
        // scriptTransitionFunction.FadeOutCinematicBarTransition();
    }

    void delayGoToTeleportToBridge()
    {
        scriptTransitionFunction.intFunctionNumbersNow = 11;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
    }

    void GoToDialogueOnBridge()
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;

            scriptTransitionFunction.characterDialogue = alanDialogueOnBridge;
            scriptTransitionFunction.DefaultTriggerMechanism();

            // scriptTransitionFunction.alanBridgeText.gameObject.SetActive(true);
        }
    }

    void GoToFadeInJump()
    {
        if (scriptTransitionFunction.isDialogue)
            return;
        scriptPlayerMovement.canMove = false;
        // scriptGuide.transitionGuideNow = 3;
        StartCoroutine(scriptGuide.IEFadeInGuideJump());
        scriptGuide.guideJumpGO.SetActive(true);
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;    
    }

    void FadeInGuideJump()
    {
        if (!scriptGuide.isFadeInGuideJump)
            return;
        
        scriptGuide.isFadeInGuideJump = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    } 

    void StartThoughtOnBridge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // StartCoroutine(scriptGuide.IEFadeOutGuideJump());
            scriptTransitionFunction.middleText.gameObject.SetActive(true);
            textBridge.gameObject.SetActive(true);
            scriptTransitionFunction.thoughtText = textBridge;
            scriptTransitionFunction.targetTextForTransition = textBridge;
            scriptTransitionFunction.characterThoughts = alanThoughtsBridge;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;

            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 7;
            scriptPlayerMovement.speedMovement = 0.5f;
        }
    }

    void EndThoughtToJump()
    {
        if (scriptTransitionFunction.isThought)
            return;
        scriptAudioManager.ambientAudioSource.Stop();
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[3]; //last space
        scriptAudioManager.gameplayAudioSource.Play();

        StartCoroutine(scriptGuide.IEFadeOutGuideJump());

        textBridge.gameObject.SetActive(false);
        scriptPlayerMovement.canMove = true;
        scriptPlayerMovement.intAutoMovement = 1;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.fadeInBlackscreen = 3f;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
    }

    void CindyThoughtBlackScreen() // 16
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[4]; //splash after jump
        scriptAudioManager.gameplayAudioSource.Play();

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.fadeInBlackscreen = 1f;

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        Invoke("DelayGoToCindyScene", 3f);
        bgBridgeBGO.SetActive(false);
        bgRiverBankGO.SetActive(true);
        canvasForRiverBankGO.SetActive(true);

        blurEffectGO.SetActive(false);
    }
    void DelayGoToCindyScene()
    {
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[5]; // cindy stalked walking
        scriptAudioManager.gameplayAudioSource.Play();

        scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[3];
        scriptAudioManager.ambientAudioSource.Play();

        scriptTransitionFunction.characterThoughts = cindyThoughtBlackScene;
        // scriptTransitionFunction.thoughtText = scriptTransitionFunction.middleText;
        // scriptTransitionFunction.targetTextForTransition = scriptTransitionFunction.middleText;
        // scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        // scriptTransitionFunction.isThought = true;
        
        // scriptTransitionFunction.valueBlackOrYellow = 0; // 0 is yellow
        // scriptTransitionFunction.valueWhiteOrBlack = 255; // 255 is white 
        scriptTransitionFunction.thoughtText.gameObject.SetActive(false);
        scriptTransitionFunction.textWithOutline.gameObject.SetActive(true);
        // scriptTransitionFunction.textWithOutline.color = new Color(255, 255, 0, 0);
        // scriptTransitionFunction.textWithOutline.color = new Color(254, 240, 138, 0);
        scriptTransitionFunction.textWithOutline.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.isThought = true;

        scriptTransitionFunction.intFunctionNumbersNow = 17;
        scriptTransitionFunction.intTransitionNumbersNow = 8; // 1;

        // cindyGO.SetActive(true);
        scriptCindyMovement.canMove = true;
        // scriptCindyMovement.intAutoMovement = 1;
        scriptCindyMovement.ChangeMovementType("Right", true);
        scriptCindyMovement.animatorCindy.speed = 2f;
        scriptCindyMovement.speedMovement = 8f;
        cindyGO.transform.position = playerPositionSpawn;

        bridgeGO.SetActive(false);
        roadGO.SetActive(true);
        playerGO.SetActive(false);
        // change target virtualCamera to cindy
        virtualCamera.Follow = cindyGO.transform;
        virtualCamera.m_Lens.OrthographicSize = 5f;

        scriptCindyMovement.footstepSounds = scriptCindyMovement.footstepOnGrass;
    }

    void FadeOutAfterCindyThoughtBlackScreen()
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        cindyGO.SetActive(true);
    }


    void GoToCindyThoughtWithRunning()
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[6]; // cindy stalked walking
            scriptAudioManager.gameplayAudioSource.Play();

            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 8; // 1;
            
            // scriptTransitionFunction.valueBlackOrYellow = 0; // 0 is yellow
            // scriptTransitionFunction.valueWhiteOrBlack = 255; // 255 is white 
            scriptTransitionFunction.characterThoughts = cindyThoughtWalking;
            // scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.thoughtText.gameObject.SetActive(false);
            scriptTransitionFunction.textWithOutline.gameObject.SetActive(true);
            // scriptTransitionFunction.textWithOutline.transform.position = new Vector2(0, 200f);
            scriptTransitionFunction.textWithOutline.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200f);
            // scriptTransitionFunction.textWithOutline.color = new Color(255, 255, 0, 0);
            // scriptTransitionFunction.textWithOutline.color = new Color(254, 240, 138, 0);
            scriptTransitionFunction.textWithOutline.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;

            scriptTransitionFunction.FadeInCinematicBarTransition();
        }
    }

    void CindyThoughtWithRunning()
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
    }

    void FadeInToHiddenBehindTree()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        scriptAudioManager.gameplayAudioSource.Stop();    
        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;

        cindyGO.SetActive(false);
        scriptCindyMovement.canMove = false;
        // scriptCindyMovement.intAutoMovement = 0;

        stalkerGO.SetActive(true);
        stalkerGO.GetComponent<PlayerMovement>().canMove = true;
        stalkerGO.transform.position = cindyGO.transform.position + new Vector3(-stalkerDistanceAway, 0, 0);
    }

    void FadeOutToHiddenBehindTree()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;

        scriptTransitionFunction.blackscreenFadeOut = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void CindyDialogueAfterStalkerAway()
    {
        if (stalkerGO.transform.position.x > cindyGO.transform.position.x + stalkerDistanceAway)
        {
            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[4];
            scriptAudioManager.ambientAudioSource.Play();

            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;

            scriptTransitionFunction.characterDialogue = cindyDialogueBehindTree;
            scriptTransitionFunction.DefaultTriggerMechanism();

            scriptTransitionFunction.FadeOutCinematicBarTransition();
            
            stalkerGO.SetActive(false);
            stalkerGO.GetComponent<PlayerMovement>().canMove = false;
        }
    }

    void StalkerWalkingGoToFadeIn()
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

        // stalkerGO.SetActive(false);
        // stalkerGO.GetComponent<PlayerMovement>().canMove = false;
    }

    void GoToFadeOutAfterStalker()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;

        cindyGO.SetActive(true);
        scriptCindyMovement.canMove = true;
        // scriptCindyMovement.intAutoMovement = 0;
        scriptCindyMovement.ChangeMovementType("Right", false);
        scriptCindyMovement.animatorCindy.speed = 1.5f;
        scriptCindyMovement.speedMovement = 5f;

        bgRiverBankGO.SetActive(false);
        canvasForRiverBankGO.SetActive(false);
        shadingRiverBankB2GO.SetActive(true);
        riverBankB2GO.SetActive(true);
    }

    void FadeOutAfterStalker()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;

        scriptTransitionFunction.blackscreenFadeOut = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void CindyRoaming()
    {
        if (!cindyMoved)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                cindyMoved = true;
                // scriptAudioManager.musicAudioSource.clip = scriptAudioManager.gameplayClips[8]; //Tension ending
                // scriptAudioManager.gameplayAudioSource.PlayOneShot(scriptAudioManager.gameplayAudioSource.clip); 
                // scriptAudioManager.musicAudioSource.Play();
                // scriptAudioManager.musicAudioSource.loop = false;
                audioSource.clip = scriptAudioManager.gameplayClips[8];
                audioSource.loop = false;
                audioSource.Play();
            }
        }
        else
        {
            roamingTimeCindy -= Time.deltaTime;
            if (roamingTimeCindy < 0)
            {
                scriptTransitionFunction.intFunctionNumbersNow++;
                scriptTransitionFunction.intTransitionNumbersNow = 3;
                scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
                exclamationMarkGO.SetActive(true);

                scriptCindyMovement.canMove = false;
                scriptCindyMovement.StopMovement();
                // scriptAudioManager.gameplayAudioSource.Stop();
                return;
            }
        }
    }

    void EndGameplayChapter0()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        // Stop All Audio
        scriptAudioManager.ambientAudioSource.Stop();

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    
        cindyGO.SetActive(false);
        StartCoroutine(GoToOutroVideoChapter0());
    }

    IEnumerator GoToOutroVideoChapter0()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        scriptPauseGameplay.SetVideoClip(videoClipOutro);
    }

    // void CheckIsPlayingOutro()
    // {
    //     StartCoroutine(GoToNextChapterToChapter1());
    // }

    // IEnumerator GoToNextChapterToChapter1()
    // {
    //     while (videoPlayer.isPlaying)
    //     {
    //         yield return null;
    //     }
    //     videoPlayer.Stop();
    //     Invoke("NextChapterToChapter1", 3f);
    // }

    // void NextChapterToChapter1()
    // {
    //     SceneManager.LoadScene("Chapter1");
    //     PlayerPrefs.SetString("ChapterNow", "Chapter1");
    // }
}
