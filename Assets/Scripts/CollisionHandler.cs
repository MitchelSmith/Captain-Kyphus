using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX Prefab on Player")] [SerializeField] GameObject deathExplosion;

    void OnTriggerEnter(Collider collider)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("HandleDeathSequence");
        deathExplosion.SetActive(true);
        Invoke("RestartLevel", levelLoadDelay);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
