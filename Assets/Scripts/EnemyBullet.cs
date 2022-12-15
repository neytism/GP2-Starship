using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject diePEffect;
    private void OnCollisionEnter2D(Collision2D col) 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player _player = GameObject.FindObjectOfType<Player>();
        if (col.gameObject.tag.Equals("Player")) {
            GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 3);
            _player.ReduceHealth(1);
        }
        Destroy(gameObject);
    }
}
