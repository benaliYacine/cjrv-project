using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapFollow : MonoBehaviour
{
    [Tooltip("Tag of your player object")]
    public string playerTag = "Player";

    [Tooltip("Height above the player")]
    public float height = 100f;

    private Transform target;

    void Awake()
    {
        TryFindPlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindPlayer();
    }

    void LateUpdate()
    {
        if (target == null) return;
        var pos = target.position;
        transform.position = new Vector3(pos.x, height, pos.z);
    }

    void TryFindPlayer()
    {
        var go = GameObject.FindGameObjectWithTag(playerTag);
        if (go) target = go.transform;
    }
}
