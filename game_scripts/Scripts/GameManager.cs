using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathCanvas;

    void Start()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("Death Canvas not assigned in GameManager!");
        }
    }

    public void PlayerDied()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
