using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private bool isTimerRunning = true;

    void Start()
    {
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
