using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private EntityInputsManager inputManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private TimerAndCoin timerScript;
    [SerializeField] private GameObject pauseMenuUI, playIcon, pauseIcon, settingMenu, joystick, skillButton;

    private bool gameIsPaused = true;
    private bool optionsOpen = false;

    public bool IsPaused { get => !gameIsPaused; }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused) Resume();
        else Pause();
    }

    void Pause()
    {
        pauseIcon.SetActive(false);
        skillButton.SetActive(false);
        joystick.SetActive(false);
        playIcon.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        timerScript.Pause();
        musicManager.Stop(1);
    }

    void Resume()
    {
        playIcon.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingMenu.SetActive(false);
        pauseIcon.SetActive(true);
        Time.timeScale = 1f;
        timerScript.Continue();
        musicManager.Play(1);
        inputManager.SetUIControllers();
    }

    public void ReStart()
    {
        timerScript.Restart();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackMenu()
    {
        Time.timeScale = 1f;
        musicManager.Stop(1);
        LevelLoader.LoadLevel("Menu");
    }

    public void OpenCloseOptions()
    {
        optionsOpen = !optionsOpen;
        settingMenu.SetActive(optionsOpen);
    }
}
