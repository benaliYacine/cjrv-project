using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    void Awake()
    {
        if (!SceneManager.GetSceneByName("UI").isLoaded)
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }
}
