using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUI : MonoBehaviour
{
    [Tooltip("Drag your TimerText TMP component here")]
    public TMP_Text timerText;

    private float elapsed = 0f;
    private bool running = false;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        // Start your timer when the first gameplay scene comes up
        // (Or call ResetTimer() manually from your GameManager)
        running = true;
        elapsed = 0f;
        UpdateDisplay();
    }

    void Update()
    {
        if (!running) return;

        elapsed += Time.deltaTime;
        TimerData.finalTime = elapsed;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int m = (int)(elapsed / 60f);
        int s = (int)(elapsed % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", m, s);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndRunning")  
        {
            // stop and show the stored final time
            running = false;
            timerText.text = FormatTime(TimerData.finalTime);
            // optional: disable this script so nothing else runs
            enabled = false;
        }
        else
        {
            // fresh start
            running = true;
            elapsed = 0f;
            UpdateDisplay();
        }
        // otherwise (e.g. intermediate levels) you can choose to keep counting
    }

    private string FormatTime(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        return string.Format("{0:00}:{1:00}", m, s);
    }
}

public static class TimerData
{
    public static float finalTime;
}
