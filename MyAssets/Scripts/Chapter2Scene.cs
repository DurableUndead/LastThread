using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Chapter2Scene : MonoBehaviour
{
    public List<System.Action> flowFunctionsChapter2 = new List<System.Action>();
    public List<System.Action> flowTransitionChapter2 = new List<System.Action>();
    [Header("Scripts")]
    public PlayerMovement scriptPlayerMovement;
    private TransitionFunction scriptTransitionFunction;
    private DialogueChapter2 dialogueChapter2;
    private PauseGameplay scriptPauseGameplay;
    private GuideScript scriptGuideScript;
    private AudioManager scriptAudioManager;
    [Header("GameObjects")]
    public GameObject fakeAlanGO;
    public GameObject realFakeAlanGO;
    public GameObject fakeCindyGO;
    public GameObject playerGO;
    public GameObject mirrorPlayerGO;
    public GameObject parentGO;
    public GameObject parentHomeGO;
    public GameObject limboGO;

    [Header("Limbo - Level 1")]
    private string[] dialogueLimbo1;
    private string[] dialogueLimbo2Her;
    private string[] dialogueLimbo3No;
    private string[] dialogueLimbo4Tomorrow;
    [Range(0.0f, 1.0f)] public float stepIntervalLimbo;
    public bool alanHasWokenUp = false;
    public Image limboInteractImage;
    public Sprite herSprite;
    public Sprite noSprite;
    public Sprite tomorrowSprite;
    public float maxDistanceToInteract = 5f;
    public AudioClip wakeupClip;

    [Header("After Limbo - Level 2")]
    bool canChangeImageScene = true;
    bool canFadeInOutImageScene = true;
    public Sprite wakeUpLikeChapter1;
    private string[] dialogueWakeUpAfterLimbo;
    public Sprite wakeUpAfterLimboSprite;
    private string[] dialogueBlackScreenAfterLimbo;
    private string[] thoughtsAfterWakeUp;
    public Sprite cindyHuggingAlanSprite;
    private string[] dialogueCindyHuggingAlan;
    private string[] cindyThoughtsYellowText;
    private string[] dialogueAlanCallMom;

    [Header("Home - Level 3")]
    public GameObject shadingVillageGO;
    private string[] dialogueAlanHome;
    private string[] alanLastThoughts;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    public int audioClipNow = 0;
    
    [Header("Credit")]
    public Image creditBetaImage;
    public VideoClip videoClipOutro;
    public VideoClip videoClipCredit;

    // Start is called before the first frame update
    void Start()
    {
        scriptPauseGameplay = GetComponent<PauseGameplay>();
        scriptAudioManager = GetComponent<AudioManager>();
        dialogueChapter2 = GetComponent<DialogueChapter2>();
        scriptTransitionFunction = GetComponent<TransitionFunction>();
        scriptGuideScript = GetComponent<GuideScript>();

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
        flowFunctionsChapter2.Add(SpaceToWakeUp); // 1
        flowFunctionsChapter2.Add(AlanApproachesFakeAlan); // 2
        flowFunctionsChapter2.Add(DialogueLimbo1); // 3
        flowFunctionsChapter2.Add(GoToDialogueLimbo2Her); // 4
        flowFunctionsChapter2.Add(DialogueLimbo2No); // 5
        flowFunctionsChapter2.Add(GoToDialogueLimbo3No); // 6
        flowFunctionsChapter2.Add(DialogueLimbo3No); // 7
        flowFunctionsChapter2.Add(GoToDialogueLimbo4Tomorrow); // 8
        flowFunctionsChapter2.Add(DialogueLimbo4Tomorrow); // 9
        flowFunctionsChapter2.Add(FadeOutAfterDialogue); // 10
        flowFunctionsChapter2.Add(WakeUpAfterLimbo); // 11
        flowFunctionsChapter2.Add(DialogueAfterWakeUp); // 12
        flowFunctionsChapter2.Add(FadeInBlackScreenAfterDialogueWakeUp); // 13
        flowFunctionsChapter2.Add(DialogueBlackScreenAfterLimbo); // 15 // 14
        flowFunctionsChapter2.Add(FadeOutBlackScreenToWhiteScreen); // 16 // 15
        flowFunctionsChapter2.Add(AlanThoughtsWhiteScreen); // 17 // 16
        flowFunctionsChapter2.Add(FadeOutWhiteGoToHugging); // 18 // 17
        flowFunctionsChapter2.Add(DialogueIsHugging); // 19 // 18
        flowFunctionsChapter2.Add(FadeInWhiteAfterHug); // 19 // 19
        flowFunctionsChapter2.Add(CindyThoughtsYellowText); // 20 // 20
        flowFunctionsChapter2.Add(BlackScreenAfterCindyThoughts); // 21 // 21
        flowFunctionsChapter2.Add(AfterAlanCallMom); // 21 // 22
        flowFunctionsChapter2.Add(FadeOutCallMom); // 22 // 23
        flowFunctionsChapter2.Add(GoToHome); // 23 // 24
        flowFunctionsChapter2.Add(DialogueToLastThoughts); // 24 // 25
        flowFunctionsChapter2.Add(LastThoughts); // 25 // 26
        flowFunctionsChapter2.Add(CreditGame); // 26 // 27

        scriptTransitionFunction.isChapter1 = false;
        // guideGO.SetActive(false);

        // OpeningGameplayChapter2();
        InitialAddDialogue();
        scriptGuideScript.iconStandOrWakeUp.gameObject.SetActive(false);
        scriptPlayerMovement.footstepSounds = scriptPlayerMovement.footstepLimbo;
        scriptPlayerMovement.stepInterval = stepIntervalLimbo;

        audioSource = scriptAudioManager.musicAudioSource;
        audioClip = scriptAudioManager.musicClips[audioClipNow];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptPauseGameplay.isPaused)
            return;

        if(scriptTransitionFunction.intFunctionNumbersNow != 0)
            flowFunctionsChapter2[scriptTransitionFunction.intFunctionNumbersNow]();

        if(scriptTransitionFunction.intTransitionNumbersNow != 0)
            flowTransitionChapter2[scriptTransitionFunction.intTransitionNumbersNow]();
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


    public void OpeningGameplayChapter2()
    {
        StartCoroutine(scriptGuideScript.IEFadeInGuideWakeUp());
        scriptPauseGameplay.StartInvokeESC();
        scriptTransitionFunction.intFunctionNumbersNow = 1;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
    }

    public void SpaceToWakeUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
            StartCoroutine(scriptGuideScript.IEFadeOutGuideWakeUp());
            scriptTransitionFunction.intFunctionNumbersNow++;
            mirrorPlayerGO.GetComponent<AnimRotationPlayer>().mirrorAlanRotation();
            StartCoroutine(PlayerRotationZ());

            // scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[audioClipNow];
            // scriptAudioManager.gameplayAudioSource.Play();

            // StartCoroutine(AudioWakeUpDone());
            scriptAudioManager.ambientAudioSource.PlayOneShot(scriptAudioManager.ambientClips[audioClipNow]);
        }
    }


    IEnumerator PlayerRotationZ()
    {
        Quaternion currentZRotation = playerGO.transform.rotation;
        float currentEulerZ = playerGO.transform.rotation.eulerAngles.z;
        while (currentZRotation.z > 0)
        {
            if (currentEulerZ > 60)
                currentEulerZ -= 50f * Time.deltaTime;
            else if (currentEulerZ > 30)
                currentEulerZ -= 40f * Time.deltaTime;
            else if (currentEulerZ > 0)
                currentEulerZ -= 30f * Time.deltaTime;
            currentZRotation = Quaternion.Euler(0, 0, currentEulerZ);
            playerGO.transform.rotation = currentZRotation;
            yield return null;
        }
        playerGO.transform.rotation = Quaternion.Euler(0, 0, 0);
        scriptPlayerMovement.canMove = true;
        alanHasWokenUp = true;
        scriptPlayerMovement.sideWalkCoreGO.SetActive(false);
        scriptPlayerMovement.idleSprite.SetActive(true);
    }

    // IEnumerator AudioWakeUpDone()
    // {
    //     while (scriptAudioManager.gameplayAudioSource.isPlaying)
    //     {
    //         yield return null;
    //     }
    //     scriptPlayerMovement.canMove = true;
    // }

    void AlanApproachesFakeAlan() // 1
    {
        if (!alanHasWokenUp)
            return;

        if (Vector3.Distance(playerGO.transform.position, fakeAlanGO.transform.position) <= maxDistanceToInteract)
        {
            scriptPlayerMovement.StopWalkingOrRunning();
            // scriptPlayerMovement.canMove = false;
            // scriptPlayerMovement.rb2D.velocity = Vector2.zero;
            // scriptPlayerMovement.animator.ResetTrigger("Walk");
            // scriptPlayerMovement.animator.SetTrigger("Idle");
            // scriptPlayerMovement.walkSprite.SetActive(false);
            // scriptPlayerMovement.idleSprite.SetActive(true);

            mirrorPlayerGO.GetComponent<PlayerMovement>().canMove = false;
            mirrorPlayerGO.GetComponent<PlayerMovement>().StopWalkingOrRunning();

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
        // guideGO.SetActive(true);
        // newGuideText.text = "Her";
        limboInteractImage.gameObject.SetActive(true);
        limboInteractImage.sprite = herSprite;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        scriptPlayerMovement.canMove = false;
    }

    void GoToDialogueLimbo2Her() // 3
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            limboInteractImage.gameObject.SetActive(false);
            scriptTransitionFunction.characterDialogue = dialogueLimbo2Her;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;

            audioClipNow++;
            audioClip = scriptAudioManager.musicClips[audioClipNow];
            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
        }
    }

    void DialogueLimbo2No() // 4
    {
        if (scriptTransitionFunction.isDialogue)
        {
            if(scriptTransitionFunction.intCharacterText == 2)
            {
                if (canChangeImageScene)
                {
                    canChangeImageScene = false;
                    scriptTransitionFunction.intTransitionNumbersNow = 3;
                    scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);
                    scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
                }
                if (!canFadeInOutImageScene)
                    return;
                if (scriptTransitionFunction.blackscreenFadeIn)
                {
                    scriptTransitionFunction.blackscreenFadeIn = false;
                    // fakeAlanGO.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                    realFakeAlanGO.SetActive(true);
                    fakeCindyGO.SetActive(false);

                    scriptTransitionFunction.intTransitionNumbersNow = 4;
                }
                if (scriptTransitionFunction.blackscreenFadeOut)
                {
                    scriptTransitionFunction.intTransitionNumbersNow = 2;
                    scriptTransitionFunction.blackscreenFadeOut = false;
                    canFadeInOutImageScene = false;
                }
            }
            return;
        }
        canChangeImageScene = true;
        canFadeInOutImageScene = true;

        limboInteractImage.gameObject.SetActive(true);
        limboInteractImage.sprite = noSprite;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        scriptPlayerMovement.canMove = false;
    }

    void GoToDialogueLimbo3No() // 5
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            limboInteractImage.gameObject.SetActive(false);
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

        limboInteractImage.gameObject.SetActive(true);
        limboInteractImage.sprite = tomorrowSprite;
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        scriptPlayerMovement.canMove = false;
    }

    void GoToDialogueLimbo4Tomorrow() // 7
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            limboInteractImage.gameObject.SetActive(false);
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
         

        scriptPlayerMovement.canMove = false;
        audioClip = scriptAudioManager.musicClips[1];
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));

        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[0];
        scriptAudioManager.gameplayAudioSource.Play();
        // fadeinWhite == long clip gameplays
        scriptTransitionFunction.fadeInWhiteScreen = scriptAudioManager.gameplayAudioSource.clip.length;
    }

    void FadeOutAfterDialogue() // 10
    {
        if (!scriptTransitionFunction.whiteScreenFadeIn)
            return;

        scriptTransitionFunction.fadeInWhiteScreen = scriptTransitionFunction.fadeOutWhiteScreen;
        scriptTransitionFunction.intFunctionNumbersNow = 0; // ++;
        scriptTransitionFunction.intTransitionNumbersNow = 0; // 8;
        scriptTransitionFunction.whiteScreenFadeIn = false;

        scriptTransitionFunction.imageScene.gameObject.SetActive(true);
        scriptTransitionFunction.imageScene.sprite = wakeUpLikeChapter1;
        scriptTransitionFunction.imageScene.color = new Color(255, 255, 255, 1);
        mirrorPlayerGO.SetActive(false);

        StartCoroutine(GoToWakeUpAfterLimbo());
    }

    IEnumerator GoToWakeUpAfterLimbo()
    {
        while (scriptAudioManager.gameplayAudioSource.isPlaying)
        {
            yield return null;
        }

        scriptTransitionFunction.intFunctionNumbersNow = 11;
        scriptTransitionFunction.intTransitionNumbersNow = 8;
    }

    void WakeUpAfterLimbo() // 11
    {
        if (scriptTransitionFunction.whiteScreenFadeOut)
        {
            scriptTransitionFunction.whiteScreenFadeOut = false;
            scriptTransitionFunction.characterDialogue = dialogueWakeUpAfterLimbo;
            scriptTransitionFunction.DefaultTriggerMechanism();
            scriptTransitionFunction.intFunctionNumbersNow++;
            scriptTransitionFunction.intTransitionNumbersNow = 2;
            // audioClip = scriptAudioManager.musicClips[1];

            audioClipNow++;
            audioClip = scriptAudioManager.musicClips[audioClipNow];
            StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
        }
    }

    void DialogueAfterWakeUp() // 11
    {
        if(scriptTransitionFunction.intCharacterText == 4)
        {
            if (canChangeImageScene)
            {
                StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
                canChangeImageScene = false;
                scriptTransitionFunction.intTransitionNumbersNow = 3;
                scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);
                scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
            }
            if (!canFadeInOutImageScene)
                return;
            if (scriptTransitionFunction.blackscreenFadeIn)
            {
                scriptTransitionFunction.blackscreenFadeIn = false;
                scriptTransitionFunction.imageScene.sprite = wakeUpAfterLimboSprite;
                scriptTransitionFunction.intTransitionNumbersNow = 4;
            }
            if (scriptTransitionFunction.blackscreenFadeOut)
            {
                scriptTransitionFunction.intTransitionNumbersNow = 2;
                scriptTransitionFunction.blackscreenFadeOut = false;
                canFadeInOutImageScene = false;
                // audioClip = scriptAudioManager.musicClips[2];

                audioClipNow++;
                audioClip = scriptAudioManager.musicClips[audioClipNow];
                StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
            }
        }

        if(scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 3;
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true); 
        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 0);

        //fade out audio
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
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

        scriptPlayerMovement.canMove = false;
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

        audioClipNow = 1;
        audioClip = scriptAudioManager.musicClips[audioClipNow];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
    }

    void DialogueIsHugging() // 17
    {
        // if (scriptTransitionFunction.intCharacterText == 1)
        // {
        //      // Go To Credit Game for Beta GamePlay
        //     scriptTransitionFunction.intFunctionNumbersNow = 27;
        //     scriptTransitionFunction.intTransitionNumbersNow = 0;
        //     scriptTransitionFunction.DefaultAfterDialogueTrigger();
        //     // StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
        //     return;
        // }

        if (scriptTransitionFunction.isDialogue)
            return;

        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 7;
        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true); 
        // scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true); 
        scriptPlayerMovement.canMove = false;
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
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
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

        scriptTransitionFunction.intFunctionNumbersNow=0;
        scriptTransitionFunction.intTransitionNumbersNow=0; // 2;
        scriptTransitionFunction.blackscreenFadeOut = false;

        // scriptTransitionFunction.characterDialogue = dialogueAlanCallMom;
        // scriptTransitionFunction.DefaultTriggerMechanism();

        // playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);

        // scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);
        // scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

        // scriptTransitionFunction.imageScene.gameObject.SetActive(false); 
        // scriptTransitionFunction.blackscreenImage2.gameObject.SetActive(false);

        // audioClipNow = 4;
        // audioClip = scriptAudioManager.musicClips[audioClipNow];
        // StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));

        //play audio call phone
        scriptAudioManager.gameplayAudioSource.clip = scriptAudioManager.gameplayClips[1];
        scriptAudioManager.gameplayAudioSource.Play();

        StartCoroutine(GoToAfterAlanCallMom());
    }

    IEnumerator GoToAfterAlanCallMom()
    {
        while (scriptAudioManager.gameplayAudioSource.isPlaying)
        {
            yield return null;
        }

        scriptTransitionFunction.intFunctionNumbersNow = 22;
        scriptTransitionFunction.intTransitionNumbersNow = 2;

        scriptTransitionFunction.characterDialogue = dialogueAlanCallMom;
        scriptTransitionFunction.DefaultTriggerMechanism();

        playerGO.transform.position = new Vector2(0, playerGO.transform.position.y);

        scriptTransitionFunction.blackscreenImage.color = new Color(0, 0, 0, 1);
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);

        scriptTransitionFunction.imageScene.gameObject.SetActive(false); 
        scriptTransitionFunction.blackscreenImage2.gameObject.SetActive(false);

        audioClipNow = 4;
        audioClip = scriptAudioManager.musicClips[audioClipNow];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
    }

    void AfterAlanCallMom()
    {
        if (scriptTransitionFunction.isDialogue) //22
            return;
        
        scriptTransitionFunction.intFunctionNumbersNow++;
        scriptTransitionFunction.intTransitionNumbersNow = 4;
        scriptPlayerMovement.canMove = false;
        parentHomeGO.SetActive(true);
        fakeAlanGO.SetActive(false);
        limboGO.SetActive(false);

        scriptPlayerMovement.footstepSounds = scriptPlayerMovement.footstepOnGrass;
        scriptPlayerMovement.stepInterval = 0.62f;
        scriptPlayerMovement.animator.speed = 0.9f;

        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
        shadingVillageGO.SetActive(true);
    }

    void FadeOutCallMom()
    {
        if (!scriptTransitionFunction.blackscreenFadeOut)
            return;

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
        scriptTransitionFunction.blackscreenFadeOut = false;
        scriptPlayerMovement.canMove = true;

        StartCoroutine(DelayGoToHome());
    }

    IEnumerator DelayGoToHome()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        scriptTransitionFunction.intFunctionNumbersNow = 24;
        scriptTransitionFunction.intTransitionNumbersNow = 0;
        audioClipNow++;
        audioClip = scriptAudioManager.musicClips[audioClipNow];
        StartCoroutine(scriptTransitionFunction.FadeInAudio(audioSource, audioClip, scriptAudioManager.volumeMusicNow));
    }

    void GoToHome()
    {
        if (Vector3.Distance(playerGO.transform.position, parentGO.transform.position) <= maxDistanceToInteract)
        {
            scriptPlayerMovement.canMove = false;
            scriptPlayerMovement.StopWalkingOrRunning();

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

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 7; //0;

        scriptPlayerMovement.StopWalkingOrRunning(mirrorDirection: true);
        
        Invoke("AlanLookedBack", 1f);
    }

    void AlanLookedBack()
    {
        scriptTransitionFunction.intFunctionNumbersNow = 26;
        scriptTransitionFunction.intTransitionNumbersNow = 7;
        scriptTransitionFunction.imageWhiteScreen.gameObject.SetActive(true);
        scriptTransitionFunction.whiteScreenFadeIn = false;
        scriptTransitionFunction.imageWhiteScreen.color = new Color(255, 255, 255, 0);
        shadingVillageGO.SetActive(false);
        StartCoroutine(scriptTransitionFunction.FadeOutAudio(audioSource, scriptAudioManager.volumeMusicNow));
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

    void CreditGame() // 27
    {
        if (scriptTransitionFunction.isThought)
            return;
        
        scriptTransitionFunction.blackscreenImage.gameObject.SetActive(true);
        StartCoroutine(scriptTransitionFunction.IEFadeInBlackscreenTransition());
        // Invoke("EnableCredit", scriptTransitionFunction.fadeInBlackscreen);

        // PlayerPrefs.DeleteKey("ChapterNow");

        scriptTransitionFunction.intFunctionNumbersNow = 0;
        scriptTransitionFunction.intTransitionNumbersNow = 0;

        Invoke("GoToOutroVideoChapter2", 1f);
    }

    void GoToOutroVideoChapter2()
    {
        scriptPauseGameplay.SetVideoClip(videoClipOutro);
    }

    // void EnableCredit()
    // {
    //     StartCoroutine(StartCredit());
    // }

    // IEnumerator StartCredit()
    // {
    //     creditBetaImage.gameObject.SetActive(true);
    //     creditBetaImage.color = new Color(255, 255, 255, 0);
    //     while (creditBetaImage.color.a < 1)
    //     {
    //         creditBetaImage.color = new Color(255, 255, 255, creditBetaImage.color.a + 0.5f * Time.deltaTime);
    //         yield return null;
    //     }
    // }
}
