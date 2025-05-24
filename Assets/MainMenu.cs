using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public AudioSource menuMusic;

    void Start() {
        menuMusic.loop = true;
        menuMusic.Play();
    }

    public void PlayGame(int sceneIndex) {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
