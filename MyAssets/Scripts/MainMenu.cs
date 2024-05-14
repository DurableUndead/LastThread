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

    [Header ("Text in Menu")]
    public Button continueBtn;
    public Text continueText;
    public Text newGameText;
    public Text settingsText;
    public Text creditsText;
    public Text exitText;

    [Header ("Text in Settings")]
    public Text musicText;
    public Text SFXText;
    public Text brightnessText;
    public Text backSettingsText;
    public Text backCreditsText;
    
    [Header("Game Object")]
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Audio Settings")]
    public Image FillAreaMusicSlider;
    public Image FillAreaSFXSlider;
    public Image FillBrightnessSFXSlider;
    public Image HandleAreaMusicSlider;
    public Image HandleAreaSFXSlider;
    public Image HandleBrightnessSFXSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider brightnessSlider;
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Brightness Settings")]
    public static float brightnessValue = 1f;

    // [Header ("Mouse Pointer")]
    // public Texture2D cursorTexture;
    // public CursorMode cursorMode = CursorMode.Auto;
    // public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        if (PlayerPrefs.HasKey("ChapterNow"))
        {
            continueText.color = newGameText.color;
            continueBtn.interactable = true;
            textCanColored = true;
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

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SFXSource.volume = SFXSlider.value;
        }
        else
        {
            SFXSlider.value = 1;
            SFXSource.volume = SFXSlider.value;
        }

        // if (PlayerPrefs.HasKey("Brightness"))
        // {
        //     brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
        // }
        // else
        // {
        //     brightnessSlider.value = 1;
        // }
    }

    //Button Continue
    public void ContinueGame()
    {
        //Load Scene
        //nanti disini akan menggunakan save data yang telah di load
        string chapterNow = PlayerPrefs.GetString("ChapterNow");
        SceneManager.LoadScene(chapterNow);
    }

    //Button Start New Game
    public void StartNewGame()
    {
        //Load Scene Gameplay
        
        PlayerPrefs.DeleteKey("ChapterNow");
        PlayerPrefs.SetString("ChapterNow", "Chapter0");
        SceneManager.LoadScene("Chapter0");
    }


    public void OpenCloseSettings()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);

            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
            // PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        }
        else
        {
            settingsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    public void OpenCloseCredits()
    {
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

    public void AdjustMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    public void AdjustSFXVolume()
    {
        SFXSource.volume = SFXSlider.value;
    }
    public void AdjustBrightness()
    {
        brightnessValue = brightnessSlider.value;
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue, 1);
    }

    //Button Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }
    




    //Merubah warna color text menu jika di hover
    public void MouseEnterContinueBtn()
    {   
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
        newGameText.color = Color.white;
    }

    public void MouseExitNewGameBtn()
    {
        newGameText.color = defaultColor;
    }

    public void MouseEnterSettingsBtn()
    {
        settingsText.color = Color.white;
    }

    public void MouseExitSettingsBtn()
    {
        settingsText.color = defaultColor;
    }

    public void MouseEnterCreditsBtn()
    {
        creditsText.color = Color.white;
    }

    public void MouseExitCreditsBtn()
    {
        creditsText.color = defaultColor;
    }

    public void MouseEnterExitBtn()
    {
        exitText.color = Color.white;
    }

    public void MouseExitExitBtn()
    {
        exitText.color = defaultColor;
    }
    
    public void MouseEnterMusicBtn()
    {
        musicText.color = Color.yellow;
        FillAreaMusicSlider.color = Color.yellow;
        HandleAreaMusicSlider.color = Color.yellow;
    }

    public void MouseExitMusicBtn()
    {
        musicText.color = defaultColor;
        FillAreaMusicSlider.color = defaultColor;
        HandleAreaMusicSlider.color = defaultColor;
    }

    public void MouseEnterSFXBtn()
    {
        SFXText.color = Color.yellow;
        FillAreaSFXSlider.color = Color.yellow;
        HandleAreaSFXSlider.color = Color.yellow;
    }

    public void MouseExitSFXBtn()
    {
        SFXText.color = defaultColor;
        FillAreaSFXSlider.color = defaultColor;
        HandleAreaSFXSlider.color = defaultColor;
    }

    public void MouseEnterBrightnessBtn()
    {
        brightnessText.color = Color.yellow;
        FillBrightnessSFXSlider.color = Color.yellow;
        HandleBrightnessSFXSlider.color = Color.yellow;
    }

    public void MouseExitBrightnessBtn()
    {
        brightnessText.color = defaultColor;
        FillBrightnessSFXSlider.color = defaultColor;
        HandleBrightnessSFXSlider.color = defaultColor;
    }
    public void MouseEnterBackBtn()
    {
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
        if (creditsPanel.activeSelf)
        {
            backCreditsText.color = defaultColor;
        }
        else if (settingsPanel.activeSelf)
        {
            backSettingsText.color = defaultColor;
        }
    }
}
