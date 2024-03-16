using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        Transform hitTransform=collision.transform;
        Debug.Log("Pehla");
        if(hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            hitTransform.GetComponent<PlayerHealthBar>().Damage(10);
        }
        Destroy(gameObject);
    }
}
