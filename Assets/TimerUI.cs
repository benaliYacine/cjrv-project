using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUI : MonoBehaviour
{
    [Tooltip("Drag your TimerText UI Text here")]
    public TMP_Text timerText;

    private float elapsed = 0f;
    private bool running = true;
    private bool isScene5 = false;

    void Start()
    {
        // Check if we're in scene 5
        isScene5 = SceneManager.GetActiveScene().buildIndex == 5;
        
        if (isScene5)
        {
            // In scene 5, display the stored time
            timerText.text = TimerData.finalTime.ToString("F2");
            running = false;
        }
        else
        {
            // In other scenes, start the timer normally
            elapsed = 0f;
            running = true;
            UpdateDisplay();
        }
    }

    void Update()
    {
        if (!running) return;

        elapsed += Time.deltaTime;
        UpdateDisplay();

        // Store the current time in case we need to switch scenes
        TimerData.finalTime = elapsed;
    }

    private void UpdateDisplay()
    {
        if (isScene5) return; // Don't update display in scene 5
        
        int minutes = (int)(elapsed / 60f);
        int seconds = (int)(elapsed % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary> Stop the timer and store the final time. </summary>
    public void StopTimer()
    {
        running = false;
        TimerData.finalTime = elapsed;
    }

    /// <summary> Reset to 0:00 and restart. </summary>
    public void ResetTimer()
    {
        if (isScene5) return; // Don't reset in scene 5
        
        elapsed = 0f;
        running = true;
        UpdateDisplay();
    }

    public float GetElapsedTime()
    {
        return elapsed;
    }
}

public static class TimerData
{
    public static float finalTime;
}

