using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    public PlayerMovement scriptPlayerMovement;
    public Vector3 playerPositionSpawn;

    [Header("Scripts")]
    public TransitionFunction scriptTransitionFunction;
    public DialogueChapter0 scriptDialogueChapter0;
    public OpeningGameplay openingGameplay;
    public GuideScript scriptGuide;
    private PauseGameplay scriptPauseGameplay;
    public AudioManager scriptAudioManager;
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
    public Text textBridge;

    //Cindy
    [Header("Cindy Scene")]
    private string[] cindyThoughtBlackScene;
    private string[] cindyThoughtWalking;
    private string[] cindyDialogueBehindTree;
    public float stalkerDistanceAway = 15f;
    public bool cindyMoved = false;
    public float roamingTimeCindy = 10f;
    public GameObject exclamationMarkGO;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volumeAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptPauseGameplay = GetComponent<PauseGameplay>();
        scriptAudioManager = GetComponent<AudioManager>();

        blackscreenImage.gameObject.SetActive(true);

        flowFunctionsChapter0.Add(NothingHappend); // 0
        flowFunctionsChapter0.Add(AlanThougtsBlackScreen); // 1
        flowFunctionsChapter0.Add(GoToDialogueWithBoss); // 2
        flowFunctionsChapter0.Add(GoToFadeOutBlackScreen); // 3
        flowFunctionsChapter0.Add(FadeInTitleGameAndGuideAD); // 4
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

        playerPositionSpawn = playerGO.transform.position;

        InitialAddDialogue();

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
        scriptTransitionFunction.isChapter1 = false;

        audioSource = scriptAudioManager.musicAudioSource;
        audioClip = scriptAudioManager.musicClips[0];
        volumeAudio = scriptAudioManager.volumeMusicNow;
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
        scriptAudioManager.musicAudioSource.clip = scriptAudioManager.gameplayClips[0];
        scriptAudioManager.musicAudioSource.Play();

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
        scriptAudioManager.ambientAudioSource.Stop();
 
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        scriptPauseGameplay.StartInvokeESC();
    }

    void FadeInTitleGameAndGuideAD()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;
        scriptTransitionFunction.blackscreenFadeOut = false;
        openingGameplay.enabled = true;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    void AutoWalkAndThoughts()
    {
        if (playerGO.transform.position.x > playerPositionSpawn.x + maxDistanceThoughts)
        {
            scriptPlayerMovement.isMoving = false;
            scriptPlayerMovement.isStepPlaying = false;

            scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[1];
            scriptAudioManager.ambientAudioSource.Play();

            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, volumeAudio));

            scriptPlayerMovement.intAutoMovement = 1;
            scriptTransitionFunction.characterThoughts = thoughts1;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;

            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;
        }
    }

    void FadeInBlackScreenAfterThought1()
    {
        if (scriptTransitionFunction.isThought)
            return;

        string status = "Pause";
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, volumeAudio, status));

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
        scriptPlayerMovement.speedMovement = 1f;
        playerGO.GetComponent<PlayerMovement>().animator.speed = 0.7f;
    }

    void GoToAlanThought2()
    {
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, volumeAudio));
        
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
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, volumeAudio, status));
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
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
            textBridge.gameObject.SetActive(true);
            scriptTransitionFunction.thoughtText = textBridge;
            scriptTransitionFunction.targetTextForTransition = textBridge;
            scriptTransitionFunction.characterThoughts = alanThoughtsBridge;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;

            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 7;
        }
    }

    void EndThoughtToJump()
    {
        if (scriptTransitionFunction.intCharacterText >= scriptTransitionFunction.characterThoughts.Length - 1)
        {
            scriptAudioManager.ambientAudioSource.Stop();
            scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[3]; //last space
            scriptAudioManager.gameplayAudioSource.Play();
        }
        if (scriptTransitionFunction.isThought)
            return;

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
    }
    void DelayGoToCindyScene()
    {
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[5]; // cindy stalked walking
        scriptAudioManager.gameplayAudioSource.Play();

        scriptAudioManager.ambientAudioSource.clip = scriptAudioManager.ambientClips[3];
        scriptAudioManager.ambientAudioSource.Play();

        scriptTransitionFunction.thoughtText = scriptTransitionFunction.middleText;
        scriptTransitionFunction.targetTextForTransition = scriptTransitionFunction.middleText;
        scriptTransitionFunction.characterThoughts = cindyThoughtBlackScene;
        scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
        scriptTransitionFunction.isThought = true;

        scriptTransitionFunction.intFunctionNumbersNow = 17;
        scriptTransitionFunction.intTransitionNumbersNow = 1;

        cindyGO.SetActive(true);
        cindyGO.GetComponent<PlayerMovement>().canMove = true;
        cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 1;
        cindyGO.transform.position = playerPositionSpawn;

        bridgeGO.SetActive(false);
        roadGO.SetActive(true);
        playerGO.SetActive(false);
        // change target virtualCamera to cindy
        virtualCamera.Follow = cindyGO.transform;
        virtualCamera.m_Lens.OrthographicSize = 5f;
    }

    void FadeOutAfterCindyThoughtBlackScreen()
    {
        if (scriptTransitionFunction.isThought)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
    }


    void GoToCindyThoughtWithRunning()
    {
        if (scriptTransitionFunction.blackscreenFadeOut)
        {
            scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[6]; // cindy stalked walking
            scriptAudioManager.gameplayAudioSource.Play();

            scriptTransitionFunction.blackscreenFadeOut = false;
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 1;
            

            scriptTransitionFunction.characterThoughts = cindyThoughtWalking;
            scriptTransitionFunction.thoughtText.text = scriptTransitionFunction.characterThoughts[scriptTransitionFunction.intCharacterText];
            scriptTransitionFunction.isThought = true;
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
        cindyGO.GetComponent<PlayerMovement>().canMove = false;
        cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 0;

        stalkerGO.SetActive(true);
        stalkerGO.GetComponent<PlayerMovement>().canMove = true;
        stalkerGO.GetComponent<PlayerMovement>().intAutoMovement = 1;
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
        }
    }

    void StalkerWalkingGoToFadeIn()
    {
        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

        stalkerGO.SetActive(false);
        stalkerGO.GetComponent<PlayerMovement>().canMove = false;
        stalkerGO.GetComponent<PlayerMovement>().intAutoMovement = 0;
    }

    void GoToFadeOutAfterStalker()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;

        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;

        cindyGO.SetActive(true);
        cindyGO.GetComponent<PlayerMovement>().canMove = true;
        cindyGO.GetComponent<PlayerMovement>().intAutoMovement = 0;
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
                scriptAudioManager.musicAudioSource.clip = scriptAudioManager.gameplayClips[8]; //Tension ending
                // scriptAudioManager.gameplayAudioSource.PlayOneShot(scriptAudioManager.gameplayAudioSource.clip); 
                scriptAudioManager.musicAudioSource.Play();
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

                cindyGO.GetComponent<PlayerMovement>().canMove = false;
                return;
            }
        }
    }

    void EndGameplayChapter0()
    {
        if (!scriptTransitionFunction.blackscreenFadeIn)
            return;
        scriptTransitionFunction.blackscreenFadeIn = false;
        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    
        cindyGO.SetActive(false);
        Invoke("NextChapterToChapter1", 3f);
    }

    void NextChapterToChapter1()
    {
        SceneManager.LoadScene("Chapter1");
        PlayerPrefs.SetString("ChapterNow", "Chapter1");
    }
}
