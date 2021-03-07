using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionScreen;
    public GameObject pauseScreen;
    public GameObject gameoverScreen;
    public Transition transition;

    public int highScore = 0;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioMixer audioMixer;

    bool isPaused = false;
    float timeScale;

    static GameController instance = null;

    public static GameController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        
    }


    void Start()
    {
        //titleScreen.SetActive(true);


         highScore = PlayerPrefs.GetInt("HighScore", 0);

        //PlayerPrefs.SetInt("HighScore", highScore);
        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0));

        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0));
        audioMixer.GetFloat("MasterVolume", out float volume);
        masterSlider.value = volume;

        audioMixer.GetFloat("MusicVolume", out float mvolume);
        musicSlider.value = mvolume;

        audioMixer.GetFloat("SFXVolume", out float svolume);
        sfxSlider.value = svolume;
        //Debug.Log(highScore);
    }

    public void SetHighScore()
    {
        highScore = Character.TotalScore;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnLoadGameScene(string scene)
    {
        //titleScreen.SetActive(false);
        //gameoverScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(LoadGameScene(scene));
    }

    IEnumerator LoadGameScene(string sceneName)
    {
        transition.StartTransition(Color.black, 1);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }

    public void OnLoadMenuScene(string scene)
    {
       // titleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameoverScreen.SetActive(false);
        SceneManager.LoadScene(scene);
        Time.timeScale = timeScale;

        StartCoroutine(LoadMenuScene(scene));
    }

    IEnumerator LoadMenuScene(string sceneName)
    {
        transition.StartTransition(Color.black, 1);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }


    public void OnQuit()
    {
        Application.Quit();
    }


    public void OnTitleScreen()
    {
        titleScreen.SetActive(true);
        optionScreen.SetActive(false);
        gameoverScreen.SetActive(false);

    }

    public void OnOptionScreen()
    {
        titleScreen.SetActive(false);
        optionScreen.SetActive(true);
    }
    public void OnGameOverScreen()
    {
        gameoverScreen.SetActive(true);
        timeScale = Time.timeScale;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void OnPauseScreen()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = timeScale;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }

    public void OnMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }
}
