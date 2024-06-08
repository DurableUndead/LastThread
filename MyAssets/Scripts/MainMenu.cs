using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private bool textCanColored = false;
    [Header ("Default Color Text")]
    public Color defaultColor;
    public Color targetColor;

    [Header ("Text in Menu")]
    public Button continueBtn;
    public Text continueText;
    public Text newGameText;
    public Text settingsText;
    public Text creditsText;
    public Text exitText;

    [Header ("Text in Settings")]
    public Text backSettingsText;
    public Text backCreditsText;
    
    [Header("Game Object")]
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject listChapterPanel;

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
    [Header("UI Audio Clips")]
    public AudioClip[] UIClips;
    public AudioSource UIAudioSource;
    [Header("Play BGM")]
    public AudioClip[] musicClips;
    void Start()
    {
        if (PlayerPrefs.HasKey("ChapterNow"))
        {
            continueText.color = newGameText.color;
            continueBtn.interactable = true;
            textCanColored = true;
        }


        if (PlayerPrefs.HasKey("GameplayVolume"))
        {
            gameplaySlider.value = PlayerPrefs.GetFloat("GameplayVolume");
            gameplaySource.volume = gameplaySlider.value;
        }
        else
        {
            gameplaySlider.value = 1;
            gameplaySource.volume = gameplaySlider.value;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicSource.volume = musicSlider.value;
        }
        else
        {
            musicSlider.value = 1;
            musicSource.volume = musicSlider.value;
        }

        if (PlayerPrefs.HasKey("AmbientVolume"))
        {
            ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolume");
            ambientSource.volume = ambientSlider.value;
        }
        else
        {
            ambientSlider.value = 1;
            ambientSource.volume = ambientSlider.value;
        }

        if (PlayerPrefs.HasKey("UIVolume"))
        {
            UISlider.value = PlayerPrefs.GetFloat("UIVolume");
            UISource.volume = UISlider.value;
        }
        else
        {
            UISlider.value = 1;
            UISource.volume = UISlider.value;
        }
        musicSource.clip = musicClips[0];
        musicSource.Play();
    }

    #region Button MainMenu & Settings
    public void ContinueGame()
    {
        //Load Scene
        //nanti disini akan menggunakan save data yang telah di load
        PlayButtonPressedUI();
        string chapterNow = PlayerPrefs.GetString("ChapterNow");
        SceneManager.LoadScene(chapterNow);
    }

    //Button Start New Game
    public void StartNewGame()
    {
        //Load Scene Gameplay
        PlayButtonPressedUI();
        PlayerPrefs.DeleteKey("ChapterNow");
        PlayerPrefs.SetString("ChapterNow", "Chapter0");
        SceneManager.LoadScene("Chapter0");
    }


    public void OpenCloseSettings()
    {
        PlayButtonPressedUI();
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);

            PlayerPrefs.SetFloat("GameplayVolume", gameplaySlider.value);
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("AmbientVolume", ambientSlider.value);
            PlayerPrefs.SetFloat("UIVolume", UISlider.value);
        }
        else
        {
            settingsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    public void OpenCloseCredits()
    {
        PlayButtonPressedUI();
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
        {
            creditsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    public void OpenListChapters()
    {
        PlayButtonPressedUI();
        if (listChapterPanel.activeSelf)
        {
            listChapterPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
        else
        {
            listChapterPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    public void GoToChapter0()
    {
        PlayButtonPressedUI();
        PlayerPrefs.DeleteKey("ChapterNow");
        PlayerPrefs.SetString("ChapterNow", "Chapter0");
        SceneManager.LoadScene("Chapter0");
    }

    public void GoToChapter1()
    {
        PlayButtonPressedUI();
        PlayerPrefs.DeleteKey("ChapterNow");
        PlayerPrefs.SetString("ChapterNow", "Chapter1");
        SceneManager.LoadScene("Chapter1");
    }

    public void GoToChapter2()
    {
        PlayButtonPressedUI();
        PlayerPrefs.DeleteKey("ChapterNow");
        PlayerPrefs.SetString("ChapterNow", "Chapter2");
        SceneManager.LoadScene("Chapter2");
    }
    #endregion

    #region Slider Volume
    public void AdjustGameplayVolume()
    {
        gameplaySource.volume = gameplaySlider.value;
    }
    public void AdjustMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    public void AdjustAmbientVolume()
    {
        ambientSource.volume = ambientSlider.value;
    }
    public void AdjustUIVolume()
    {
        UISource.volume = UISlider.value;
    }
    //Button Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    

    void PlayButtonPressedUI()
    {
        UIAudioSource.PlayOneShot(UIClips[0]);
    }

    void PlayButtonHoverSoundUI()
    {
        UIAudioSource.PlayOneShot(UIClips[1]);
    }


    #region Hover Button
    public void MouseEnterContinueBtn()
    {   
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        PlayButtonHoverSoundUI();
        if (textCanColored)
            continueText.color = Color.white;
    }

    public void MouseExitContinueBtn()
    {
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
        if (textCanColored)
            continueText.color = defaultColor;
    }

    public void MouseEnterNewGameBtn()
    {
        PlayButtonHoverSoundUI();
        newGameText.color = Color.white;
    }

    public void MouseExitNewGameBtn()
    {
        newGameText.color = defaultColor;
    }

    public void MouseEnterSettingsBtn()
    {
        PlayButtonHoverSoundUI();
        settingsText.color = Color.white;
    }

    public void MouseExitSettingsBtn()
    {
        settingsText.color = defaultColor;
    }

    public void MouseEnterCreditsBtn()
    {
        PlayButtonHoverSoundUI();
        creditsText.color = Color.white;
    }

    public void MouseExitCreditsBtn()
    {
        creditsText.color = defaultColor;
    }

    public void MouseEnterExitBtn()
    {
        PlayButtonHoverSoundUI();
        exitText.color = Color.white;
    }

    public void MouseExitExitBtn()
    {
        exitText.color = defaultColor;
    }
    

    public void MouseEnterGameplayBtn()
    {
        PlayButtonHoverSoundUI();
        // gameplayText.color = targetColor;
        fillAreaGameplaySlider.color = targetColor;
        handleAreaGameplaySlider.color = targetColor;
    }
    public void MouseExitGameplayBtn()
    {
        // gameplayText.color = defaultColor;
        fillAreaGameplaySlider.color = defaultColor;
        handleAreaGameplaySlider.color = defaultColor;
    }
    public void MouseEnterMusicBtn()
    {
        PlayButtonHoverSoundUI();
        // musicText.color = targetColor;
        fillAreaMusicSlider.color = targetColor;
        handleAreaMusicSlider.color = targetColor;
    }

    public void MouseExitMusicBtn()
    {
        // musicText.color = defaultColor;
        fillAreaMusicSlider.color = defaultColor;
        handleAreaMusicSlider.color = defaultColor;
    }
    public void MouseEnterAmbientBtn()
    {
        PlayButtonHoverSoundUI();
        // ambientText.color = targetColor;
        fillAreaAmbientSlider.color = targetColor;
        handleAreaAmbientSlider.color = targetColor;
    }
    public void MouseExitAmbientBtn()
    {
        // ambientText.color = defaultColor;
        fillAreaAmbientSlider.color = defaultColor;
        handleAreaAmbientSlider.color = defaultColor;
    }
    public void MouseEnterUIBtn()
    {
        PlayButtonHoverSoundUI();
        // UIText.color = targetColor;
        fillAreaUISlider.color = targetColor;
        handleAreaUISlider.color = targetColor;
    }
    public void MouseExitUIBtn()
    {
        // UIText.color = defaultColor;
        fillAreaUISlider.color = defaultColor;
        handleAreaUISlider.color = defaultColor;
    }

    public void MouseEnterBackBtn()
    {
        PlayButtonHoverSoundUI();
        if (creditsPanel.activeSelf)
        {
            backCreditsText.color = Color.white;
        }
        else if (settingsPanel.activeSelf)
        {
            backSettingsText.color = Color.white;
        }
    }

    public void MouseExitBackBtn()
    {
        PlayButtonHoverSoundUI();
        if (creditsPanel.activeSelf)
        {
            backCreditsText.color = defaultColor;
        }
        else if (settingsPanel.activeSelf)
        {
            backSettingsText.color = defaultColor;
        }
    }
    #endregion
}
