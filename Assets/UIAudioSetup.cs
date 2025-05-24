using UnityEngine;
using UnityEngine.UI;

public class UIAudioSetup : MonoBehaviour
{
    void Start()
    {
        // find all UI buttons in the loaded scene
        foreach (var btn in FindObjectsOfType<Button>())
        {
            btn.onClick.AddListener(() => AudioManager.Instance.PlayClick());
        }
    }
}
