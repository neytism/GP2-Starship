using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class Enemy : MonoBehaviour
{
    private Player _player;
    [SerializeField] private float _damage = 1;
    public GameObject diePEffect;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private Transform _enemyFirePoint;
    [SerializeField] private float _enemyFireForce;
    private float timeBtwShot;
    [SerializeField] private float startTimeBtwShots;

    [SerializeField] private AudioClip _hitSound;
    
    [SerializeField] private bool _canFire = true;
    
    
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        timeBtwShot = startTimeBtwShots;
    }

    private void Update()
    {
        if (_canFire)
        {
            EnemyFire();
        }
       
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("Bullet")) 
        {
            //deploys particle before destroying object
            //particle system can be converted to pooling system if time possible
            AudioSource.PlayClipAtPoint(_hitSound, gameObject.transform.position);
            GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 3);
            

            if (col.gameObject.tag.Equals("Player")) {
               _player.ReduceHealth(_damage);
           }
           _player.AddKillCount();
            Destroy(gameObject);
        }
    }

    public void EnemyFire()
    {
        if (timeBtwShot <= 0)
        {
            GameObject bullet = Instantiate(_enemyBullet, _enemyFirePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(_enemyFirePoint.up * _enemyFireForce,ForceMode2D.Impulse);
            timeBtwShot = startTimeBtwShots;
            StartCoroutine(BulletLife(bullet));
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
    
    
    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }
}
