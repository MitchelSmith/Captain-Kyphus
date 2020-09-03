using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathExplosion;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerKill = 10;
    [SerializeField] int hitsRemaining = 10;

    Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        hitsRemaining--;
        if (hitsRemaining <= 0)
        {
            KillEnemy();
            scoreboard.ScoreHit(scorePerKill);
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }
}
