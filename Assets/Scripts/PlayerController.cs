using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class PlayerController : MonoBehaviour
{
    private static bool _isPauseOrDead;

    //for movements
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    public Vector2 _moveDirection;

    //for aiming
    private Vector2 _mousePosition; 
    
    //for weapon
    private GameObject _weapon;
    private Transform _firePoint;
    private float _nextFire = 0f;

    public Transform FirePoint => _firePoint;

    private void Awake()
    {
        _isPauseOrDead = false;
    }

    private void Start()
    {
        //gets initial position from save
        transform.position = PlayerManager.Instance.Position;
        
        WeaponHolder _weaponHolder = GetComponent<WeaponHolder>();
        _weapon = _weaponHolder.SelectWeaponType(PlayerManager.Instance.GetSelectedCharacter());
        
        Debug.Log($"Selected on loading game scene: {PlayerManager.Instance.GetSelectedCharacter()}");
    }

    // Update is called once per frame
    void Update()
    {
        _firePoint = _weapon.GetComponent<Transform>();
        if (!_isPauseOrDead)
        {
            //movement
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            _moveDirection = new Vector2(moveX, moveY);
            
            //mouse pointer position
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //fire
            if (Input.GetMouseButton(0) && Time.time > _nextFire)
            {
                AudioManager.Instance.PlayOnce(AudioManager.Sounds.PlayerShoot);
                _nextFire = Time.time + _weapon.GetComponent<Weapon>().FireRate;
                _weapon.GetComponent<Weapon>().FireWeapon();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isPauseOrDead)
        {
            //movement
            _rb.AddForce(new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed));
        
            //aim
            Vector2 aimDirection = _mousePosition - _rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;  //faces towards target
            _rb.rotation = aimAngle;
        }
        
    }

    public static bool IsPauseOrDead // to avoid movements if dead or paused
    {
        get => _isPauseOrDead;
        set => _isPauseOrDead = value;
    }

    
}
