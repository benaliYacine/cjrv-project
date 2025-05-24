using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndGoal"))
        {
            // 1) Make sure the cursor is visible and unlocked
            Cursor.visible    = true;
            Cursor.lockState  = CursorLockMode.None;

            // 2) Now load your end screen (scene index 5)
            SceneManager.LoadScene(5);
        }
    }
}
