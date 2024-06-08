using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseGameplay : MonoBehaviour
{
    [Header("Video Outro Gameplay")]
    public VideoPlayer videoPlayer;
    public VideoClip creditVideoClip;
    public RawImage rawImageVideo;
    public bool goToNextChapter = true;

    [Header("Another Settings")]
    public Color defaultColor;
    public Color targetColor;
    public bool isPaused = false;
    public float timerDisableESC = 10f;
    [Header("Scripts")]
    public AudioManager scriptAudioManager;
    public TransitionFunction scriptTransitionFunction;
    public PuzzleManager scriptPuzzleManager;
    [Header("GameObject Menu")]
    public GameObject pauseMenuGO;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject iconPauseESC;
    
    [Header ("Menu Panel")]
    public Text continueText;
    public Text restartChapterText;
    public Text settingsText;
    public Text mainMenuText;
    public Text backText;

    [Header("Audio Gameplay Settings")]    
    // public Text gameplayText;
    public Image fillAreaGameplaySlider;
    public Image handleAreaGameplaySlider;
    public Slider gameplaySlider;
    public AudioSource gameplaySource;  

    [Header("Music Settings")]
    // public Text musicText;
    public Image fillAreaMusicSlider;
    public Image handleAreaMusicSlider;
    public Slider musicSlider;
    public AudioSource musicSource;

    [Header("Ambient Settings")]
    // public Text ambientText;
    public Image fillAreaAmbientSlider;
    public Image handleAreaAmbientSlider;
    public Slider ambientSlider;
    public AudioSource ambientSource;

    [Header("UI Audio Settings")]
    // public Text UIText;
    public Image fillAreaUISlider;
    public Image handleAreaUISlider;
    public Slider UISlider;
    public AudioSource UISource;
    

    // Start is called before the first frame update
    void Start()
    {
        rawImageVideo.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
        videoPlayer.loopPointReached += SkipOrStopVideo;

        pauseMenuGO.SetActive(false);
        gameplaySlider.value = PlayerPrefs.GetFloat("GameplayVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolume");
        UISlider.value = PlayerPrefs.GetFloat("UIVolume");
        
        gameplaySource.volume = gameplaySlider.value;
        musicSource.volume = musicSlider.value;
        ambientSource.volume = ambientSlider.value;
        UISource.volume = UISlider.value;

        scriptAudioManager.volumeGameplayNow = gameplaySlider.value;
        scriptAudioManager.volumeMusicNow = musicSlider.value;
        scriptAudioManager.volumeAmbientNow = ambientSlider.value;
        scriptAudioManager.volumeUINow = UISlider.value;
    }

    void Update()
    {
        if (!isPaused && videoPlayer.isPlaying)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                SkipOrStopVideo(videoPlayer);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scriptAudioManager.PlayButtonPressedUI();
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void SetVideoClip(VideoClip clip)
    {
        rawImageVideo.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);

        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void SkipOrStopVideo(VideoPlayer vp)
    {
        if (creditVideoClip != null)
        {
            SetVideoClip(creditVideoClip);
            creditVideoClip = null;
            return;
        }

        rawImageVideo.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
        videoPlayer.Stop();
        if (goToNextChapter)
        {
            string getSceneName = SceneManager.GetActiveScene().name;
            if (getSceneName == "Chapter0")
            {
                PlayerPrefs.SetString("ChapterNow", "Chapter1");
                SceneManager.LoadScene("Chapter1");
            }
            else if (getSceneName == "Chapter1")
            {
                PlayerPrefs.SetString("ChapterNow", "Chapter2");
                SceneManager.LoadScene("Chapter2");
            }
            else if (getSceneName == "Chapter2")
            {
                SceneManager.LoadScene("MainMenu");
                PlayerPrefs.DeleteKey("ChapterNow");
            }
        }
    }
    
    public void SpeedUpTimeHack()
    {
        Time.timeScale = 2f;
        ResumeGame(speed: true);
    }

    public void StartInvokeESC()
    {
        Invoke("DisabledEscIcon", timerDisableESC);
    }

    void DisabledEscIcon()
    {
        iconPauseESC.SetActive(false);
        timerDisableESC = 0;
    }

    void PauseGame()
    {
        if (scriptPuzzleManager != null)
            scriptPuzzleManager.CannotDragObject();
        pauseMenuGO.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        iconPauseESC.SetActive(false);
        scriptAudioManager.musicAudioSource.Pause();
        // StartCoroutine(WaitMusicFadeInOut());
    }

    void ResumeGame(bool speed = false)
    {
        if (scriptPuzzleManager != null)
            scriptPuzzleManager.CanDragObject();
        pauseMenuGO.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        if (!speed)
            Time.timeScale = 1f;
        isPaused = false;
        scriptAudioManager.musicAudioSource.UnPause();

        if (timerDisableESC == 0)
            return;
        iconPauseESC.SetActive(true);
    }

    public void ResumeButton()
    {
        scriptAudioManager.PlayButtonPressedUI();
        ResumeGame();
    }

    public void RestartChapter()
    {
        goToNextChapter = false;
        SkipOrStopVideo(videoPlayer);
        scriptAudioManager.PlayButtonPressedUI();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        scriptAudioManager.PlayButtonPressedUI();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void AdjustGameplayVolume()
    {
        gameplaySource.volume = gameplaySlider.value;
        PlayerPrefs.SetFloat("GameplayVolume", gameplaySlider.value);
        scriptAudioManager.volumeGameplayNow = gameplaySlider.value;
    }

    public void AdjustMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        scriptAudioManager.volumeMusicNow = musicSlider.value;
    }

    public void AdjustAmbientVolume()
    {
        ambientSource.volume = ambientSlider.value;
        PlayerPrefs.SetFloat("AmbientVolume", ambientSlider.value);
        scriptAudioManager.volumeAmbientNow = ambientSlider.value;
    }

    public void AdjustUIVolume()
    {
        UISource.volume = UISlider.value;
        PlayerPrefs.SetFloat("UIVolume", UISlider.value);
        scriptAudioManager.volumeUINow = UISlider.value;
    }

    public void OpenCloseSettings()
    {
        scriptAudioManager.PlayButtonPressedUI();
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }


    public void MouseEnterContinueBtn()
    {   
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        scriptAudioManager.PlayButtonHoverSoundUI();
        continueText.color = Color.white;
    }

    public void MouseExitContinueBtn()
    {
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
        continueText.color = defaultColor;
    }
    public void MouseEnterRestartChapterBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        restartChapterText.color = Color.white;
    }

    public void MouseExitRestartChapterBtn()
    {
        restartChapterText.color = defaultColor;
    }
    public void MouseEnterSettingsBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        settingsText.color = Color.white;
    }

    public void MouseExitSettingsBtn()
    {
        settingsText.color = defaultColor;
    }

    public void MouseEnterExitBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        mainMenuText.color = Color.white;
    }

    public void MouseExitExitBtn()
    {
        mainMenuText.color = defaultColor;
    }

    public void MouseEnterBackBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        backText.color = Color.white;
    }

    public void MouseExitBackBtn()
    {
        backText.color = defaultColor;
    }

    public void MouseEnterGameplayBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        // gameplayText.color = Color.yellow;
        fillAreaGameplaySlider.color = Color.yellow;
        handleAreaGameplaySlider.color = Color.yellow;
    }
    public void MouseExitGameplayBtn()
    {
        // gameplayText.color = defaultColor;
        fillAreaGameplaySlider.color = defaultColor;
        handleAreaGameplaySlider.color = defaultColor;
    }
    public void MouseEnterMusicBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        // musicText.color = Color.yellow;
        fillAreaMusicSlider.color = Color.yellow;
        handleAreaMusicSlider.color = Color.yellow;
    }

    public void MouseExitMusicBtn()
    {
        // musicText.color = defaultColor;
        fillAreaMusicSlider.color = defaultColor;
        handleAreaMusicSlider.color = defaultColor;
    }
    public void MouseEnterAmbientBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        // ambientText.color = Color.yellow;
        fillAreaAmbientSlider.color = Color.yellow;
        handleAreaAmbientSlider.color = Color.yellow;
    }
    public void MouseExitAmbientBtn()
    {
        // ambientText.color = defaultColor;
        fillAreaAmbientSlider.color = defaultColor;
        handleAreaAmbientSlider.color = defaultColor;
    }
    public void MouseEnterUIBtn()
    {
        scriptAudioManager.PlayButtonHoverSoundUI();
        // UIText.color = Color.yellow;
        fillAreaUISlider.color = Color.yellow;
        handleAreaUISlider.color = Color.yellow;
    }
    public void MouseExitUIBtn()
    {
        // UIText.color = defaultColor;
        fillAreaUISlider.color = defaultColor;
        handleAreaUISlider.color = defaultColor;
    }
}
