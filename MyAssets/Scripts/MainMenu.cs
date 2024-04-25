using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header ("Default Color Text")]
    public Color defaultColor;

    [Header ("Menu Text")]
    public Text continueText;
    public Text newGameText;
    public Text settingsText;
    public Text exitText;
    public Text volumeText;
    public Text backText;
    
    [Header("Game Object")]
    public GameObject menuPanel;
    public GameObject settingsPanel;

    [Header("Volume Settings")]
    public Slider volumeSlider;
    public AudioSource audioSource;

    // [Header ("Mouse Pointer")]
    // public Texture2D cursorTexture;
    // public CursorMode cursorMode = CursorMode.Auto;
    // public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            audioSource.volume = volumeSlider.value;
        }
        else
        {
            volumeSlider.value = 1;
            audioSource.volume = volumeSlider.value;
        }
    }

    //Button Continue
    public void ContinueGame()
    {
        //Load Scene
        //nanti disini akan menggunakan save data yang telah di load
    }

    //Button Start New Game
    public void StartNewGame()
    {
        //Load Scene Gameplay
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        SceneManager.LoadScene("Chapter1");
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
        continueText.color = Color.white;
    }

    public void MouseExitContinueBtn()
    {
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
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

    public void MouseEnterExitBtn()
    {
        exitText.color = Color.white;
    }

    public void MouseExitExitBtn()
    {
        exitText.color = defaultColor;
    }

    public void openSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void closeSettings()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void MouseEnterVolumeBtn()
    {
        volumeText.color = Color.white;
    }

    public void MouseExitVolumeBtn()
    {
        volumeText.color = defaultColor;
    }

    public void MouseEnterBackBtn()
    {
        backText.color = Color.white;
    }

    public void MouseExitBackBtn()
    {
        backText.color = defaultColor;
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
