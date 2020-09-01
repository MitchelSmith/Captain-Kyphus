using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathExplosion;
    [SerializeField] Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    void OnParticleCollision(GameObject other) 
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
