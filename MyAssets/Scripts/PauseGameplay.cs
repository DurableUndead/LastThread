using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameplay : MonoBehaviour
{
    public Color defaultColor;
    // [Header("Scripts")]
    // public OpeningGameplay openingGameplay;

    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;
    public bool isPaused = false;
    [Header ("Menu Panel")]
    public GameObject menuPanel;
    public Text continueText;
    public Text settingsText;
    public Text exitText;
    public Text volumeText;
    public Text backText;
    [Header("Settings Menu")]
    public GameObject settingsPanel;
    public AudioSource audioSource;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioSource.volume = volumeSlider.value;
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

    void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsButton()
    {

    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
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
}
