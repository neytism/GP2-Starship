using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class Weapon : MonoBehaviour
{
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireForce = 20f;
    [SerializeField] private float _fireRate = .1f;
    

    private ObjectPool _bulletPool;
    [SerializeField] private int _bulletPoolCount = 30;
    
    private PlayerController _player;
    
    public float FireRate => _fireRate;
    
    private void Awake()
    {
        _bulletPool = GetComponent<ObjectPool>();
        _player = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        _bulletPool.Initialize(_bulletPrefab,_bulletPoolCount); // size depends on fore rate of player
    }
    
    
    public void FireWeapon()
    {
        if (this.GameObject().name.Equals("SpreadGun"))
        {
            
            for (int i = 0; i < 5; i++)
            {

                GameObject bullet = _bulletPool.CreateObject();
                
                switch (i)
                {
                    case 0:
                        bullet.GetComponent<Rigidbody2D>().AddForce(_player.FirePoint.up * _fireForce,ForceMode2D.Impulse);
                        break;
                    case 1:
                        bullet.GetComponent<Rigidbody2D>().AddForce(RotateTowardsUp(_player.FirePoint.up, -20) * _fireForce,ForceMode2D.Impulse);
                        break;
                    case 2:
                        bullet.GetComponent<Rigidbody2D>().AddForce(RotateTowardsUp(_player.FirePoint.up, -10) * _fireForce,ForceMode2D.Impulse);
                        break;
                    case 3:
                        bullet.GetComponent<Rigidbody2D>().AddForce(RotateTowardsUp(_player.FirePoint.up, 10) * _fireForce,ForceMode2D.Impulse);
                        break;
                    case 4:
                        bullet.GetComponent<Rigidbody2D>().AddForce(RotateTowardsUp(_player.FirePoint.up, 20) * _fireForce,ForceMode2D.Impulse);
                        break;
                }
                StartCoroutine(BulletLife(bullet));
            }
        } else 
        {
            //get object from object pool instead of instantiating directly
            GameObject bullet = _bulletPool.CreateObject();
            bullet.GetComponent<Rigidbody2D>().AddForce(_player.FirePoint.up * _fireForce,ForceMode2D.Impulse);
            StartCoroutine(BulletLife(bullet));
            
        }
        
    }
    
    Vector3 RotateTowardsUp(Vector3 start, float angle) //rotates vector3.up
    {
        // if you know start will always be normalized, can skip this step
        start.Normalize();

        Vector3 axis = Vector3.Cross(start, Vector3.up);

        // handle case where start is colinear with up
        if (axis == Vector3.zero) axis = Vector3.right;

        return Quaternion.AngleAxis(angle, axis) * start;
    }
    

    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        bullet.SetActive(false);
    }
}
