using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject smallDiePEffect;
    private void OnCollisionEnter2D(Collision2D col) 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player _player = GameObject.FindObjectOfType<Player>();
        if (col.gameObject.tag.Equals("Player")) {
            GameObject particle = Instantiate(smallDiePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 3);
            _player.ReduceHealth(1);
        }
        else if (col.gameObject.tag.Equals("Bullet"))
        {
            GameObject particle = Instantiate(smallDiePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 1);
        }
        Destroy(gameObject);
    }
}
