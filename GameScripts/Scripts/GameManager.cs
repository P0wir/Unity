using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathCanvas;
    public GameObject mainMenuCanvas; 
    public GameObject winCanvas;

    private int bossKillCount = 0;
    public int bossesToWin = 3;
    private bool isMenuActive = false; 

    void Start()
    {
        if (deathCanvas != null) deathCanvas.SetActive(false);
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(false);
        if (winCanvas != null) winCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenu();
        }
    }

    public void PlayerDied()
    {
        if (deathCanvas != null) deathCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    private void ToggleMainMenu()
    {
        if (mainMenuCanvas != null)
        {
            isMenuActive = !isMenuActive;
            mainMenuCanvas.SetActive(isMenuActive);
            Time.timeScale = isMenuActive ? 0f : 1f;
        }
    }

    public void BossKilled()
    {
        bossKillCount++;
        if (bossKillCount >= bossesToWin)
        {
            YouWin();
        }
    }

    private void YouWin()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
        Debug.Log("You Win!");
    }
}
