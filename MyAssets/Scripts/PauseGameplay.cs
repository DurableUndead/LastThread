using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameplay : MonoBehaviour
{
    public Color defaultColor;
    public Color targetColor;
    public bool isPaused = false;
    public float timerDisableESC = 10f;
    [Header("Scripts")]
    public AudioManager scriptAudioManager;
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
    public Text gameplayText;
    public Image fillAreaGameplaySlider;
    public Image handleAreaGameplaySlider;
    public Slider gameplaySlider;
    public AudioSource gameplaySource;  

    [Header("Music Settings")]
    public Text musicText;
    public Image fillAreaMusicSlider;
    public Image handleAreaMusicSlider;
    public Slider musicSlider;
    public AudioSource musicSource;

    [Header("Ambient Settings")]
    public Text ambientText;
    public Image fillAreaAmbientSlider;
    public Image handleAreaAmbientSlider;
    public Slider ambientSlider;
    public AudioSource ambientSource;

    [Header("UI Audio Settings")]
    public Text UIText;
    public Image fillAreaUISlider;
    public Image handleAreaUISlider;
    public Slider UISlider;
    public AudioSource UISource;
    

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuGO.SetActive(false);
        gameplaySlider.value = PlayerPrefs.GetFloat("GameplayVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolume");
        UISlider.value = PlayerPrefs.GetFloat("UIVolume");
        
        gameplaySource.volume = gameplaySlider.value;
        musicSource.volume = musicSlider.value;
        ambientSource.volume = ambientSlider.value;
        UISource.volume = UISlider.value;

        scriptAudioManager.volumeGameplay = gameplaySlider.value;
        scriptAudioManager.volumeMusicNow = musicSlider.value;
        scriptAudioManager.volumeAmbientNow = ambientSlider.value;
        scriptAudioManager.volumeUINow = UISlider.value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        pauseMenuGO.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        iconPauseESC.SetActive(false);
    }

    void ResumeGame()
    {
        pauseMenuGO.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (timerDisableESC == 0)
            return;
        iconPauseESC.SetActive(true);
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void RestartChapter()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void AdjustGameplayVolume()
    {
        gameplaySource.volume = gameplaySlider.value;
        PlayerPrefs.SetFloat("GameplayVolume", gameplaySlider.value);
        scriptAudioManager.volumeGameplay = gameplaySlider.value;
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
        continueText.color = Color.white;
    }

    public void MouseExitContinueBtn()
    {
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
        continueText.color = defaultColor;
    }
    public void MouseEnterRestartChapterBtn()
    {
        restartChapterText.color = Color.white;
    }

    public void MouseExitRestartChapterBtn()
    {
        restartChapterText.color = defaultColor;
    }
    public void MouseEnterSettingsBtn()
    {
        settingsText.color = Color.white;
    }

    public void MouseExitSettingsBtn()
    {
        settingsText.color = defaultColor;
    }

    public void MouseEnterExitBtn()
    {
        mainMenuText.color = Color.white;
    }

    public void MouseExitExitBtn()
    {
        mainMenuText.color = defaultColor;
    }

    public void MouseEnterBackBtn()
    {
        backText.color = Color.white;
    }

    public void MouseExitBackBtn()
    {
        backText.color = defaultColor;
    }

    public void MouseEnterGameplayBtn()
    {
        gameplayText.color = Color.yellow;
        fillAreaGameplaySlider.color = Color.yellow;
        handleAreaGameplaySlider.color = Color.yellow;
    }
    public void MouseExitGameplayBtn()
    {
        gameplayText.color = defaultColor;
        fillAreaGameplaySlider.color = defaultColor;
        handleAreaGameplaySlider.color = defaultColor;
    }
    public void MouseEnterMusicBtn()
    {
        musicText.color = Color.yellow;
        fillAreaMusicSlider.color = Color.yellow;
        handleAreaMusicSlider.color = Color.yellow;
    }

    public void MouseExitMusicBtn()
    {
        musicText.color = defaultColor;
        fillAreaMusicSlider.color = defaultColor;
        handleAreaMusicSlider.color = defaultColor;
    }
    public void MouseEnterAmbientBtn()
    {
        ambientText.color = Color.yellow;
        fillAreaAmbientSlider.color = Color.yellow;
        handleAreaAmbientSlider.color = Color.yellow;
    }
    public void MouseExitAmbientBtn()
    {
        ambientText.color = defaultColor;
        fillAreaAmbientSlider.color = defaultColor;
        handleAreaAmbientSlider.color = defaultColor;
    }
    public void MouseEnterUIBtn()
    {
        UIText.color = Color.yellow;
        fillAreaUISlider.color = Color.yellow;
        handleAreaUISlider.color = Color.yellow;
    }
    public void MouseExitUIBtn()
    {
        UIText.color = defaultColor;
        fillAreaUISlider.color = defaultColor;
        handleAreaUISlider.color = defaultColor;
    }
}
