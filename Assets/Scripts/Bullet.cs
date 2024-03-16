using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]float timeToDestroy;
    float timer;
    void Start()
    {
        
    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=timeToDestroy)
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
    }
}
