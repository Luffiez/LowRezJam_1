using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PauseScreen : MonoBehaviour
{
    public AudioClip pauseClip;
    public VolumeProfile volume;
    public GameObject pauseWindow;
    public GameObject helpScreen;
    public GameObject statsWindow;

    bool isPaused = false;
    private VolumeComponent colorAdjustment = null;
    SoundManager soundManager;

    void Start()
    {
        colorAdjustment = volume.components[2];
        FindObjectOfType<PlayerController>().PauseEvent.AddListener(TogglePauseScreen);
        soundManager = SoundManager.instance;
    }

    public void TogglePauseScreen()
    {
        isPaused = !isPaused;

        if (isPaused)
            Pause();
        else
            Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Pause()
    {
        soundManager.PlaySfx(pauseClip);
        soundManager.PauseBgm();
        colorAdjustment.active = true;
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        statsWindow.SetActive(false);
    }

    void Resume()
    {
        soundManager.ResumeBgm();
        colorAdjustment.active = false;
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        statsWindow.SetActive(true);
        helpScreen.SetActive(false);
    }
}
