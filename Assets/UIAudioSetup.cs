using UnityEngine;
using UnityEngine.UI;

public class UIAudioSetup : MonoBehaviour
{
    void Start()
    {
        // find all active UI Buttons in the loaded scene, unsorted (faster)
        var buttons = Object.FindObjectsByType<Button>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(() => AudioManager.Instance.PlayClick());
        }
    }
}
