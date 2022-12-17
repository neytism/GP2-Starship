using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject smallDiePEffect;

    private void Awake()
    {
    }

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
            //GameObject particle = Instantiate(smallDiePEffect, transform.position, Quaternion.identity);
            //Destroy(particle, 3);
            //GameObject particle = _particlePool.GetObject(smallDiePEffect, transform.position);
            GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
            particle.SetActive(true);
            _player.ReduceHealth(1);
        }
        else if (col.gameObject.tag.Equals("Bullet") || col.gameObject.tag.Equals("ExplosionRadius"))
        {
            AudioManager.Instance.PlayOnce(AudioManager.Sounds.MiniExplosion);
            //GameObject particle = _particlePool.GetObject(smallDiePEffect, transform.position);
            GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
            particle.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    
    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(5);
        //GameObject particle = _particlePool.GetObject(smallDiePEffect,transform.position);
        GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
        particle.SetActive(true);
        bullet.SetActive(false);
    }
}
