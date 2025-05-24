using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Tooltip("Drag your TimerText UI Text here")]
    public TMP_Text timerText;

    private float elapsed = 0f;
    private bool running = true;

    void Start()
    {
        elapsed = 0f;
        running = true;
        UpdateDisplay();
    }

    void Update()
    {
        if (!running) return;

        elapsed += Time.deltaTime;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int minutes = (int)(elapsed / 60f);
        int seconds = (int)(elapsed % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary> Stop the timer. </summary>
    public void StopTimer()
    {
        running = false;
    }

    /// <summary> Reset to 0:00 and restart. </summary>
    public void ResetTimer()
    {
        elapsed = 0f;
        running = true;
        UpdateDisplay();
    }
}
