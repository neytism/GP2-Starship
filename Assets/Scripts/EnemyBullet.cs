using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject smallDiePEffect;

    private void OnEnable()
    {
        StartCoroutine(BulletLife(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        gameObject.SetActive(false);
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
            AudioManager.Instance.PlayOnce(AudioManager.Sounds.MiniExplosion);
            GameObject particle = Instantiate(smallDiePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 1);
        }
        gameObject.SetActive(false);
    }
    
    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(5);
        bullet.SetActive(false);
    }
}
