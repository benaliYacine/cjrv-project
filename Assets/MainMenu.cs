using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int sceneIndex){
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
