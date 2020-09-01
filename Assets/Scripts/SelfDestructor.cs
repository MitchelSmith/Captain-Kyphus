using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [Tooltip("Seconds")] [SerializeField] float destructionDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructionDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
