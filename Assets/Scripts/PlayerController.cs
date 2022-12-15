using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class PlayerController : MonoBehaviour
{
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

    private PlayerManager _playerManager;

    [SerializeField] private AudioSource _shootSound;
    
    
    public Transform FirePoint => _firePoint;

    private void Awake()
    {
        _playerManager = GameObject.FindObjectOfType<PlayerManager>();
        _weapon = _playerManager.SelectWeaponType(_playerManager.selectedCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        _firePoint = _weapon.GetComponent<Transform>();
        
        //movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector2(moveX, moveY);
        
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //fire
        if (Input.GetMouseButton(0) && Time.time > _nextFire)
        {
            _shootSound.Play();
            _nextFire = Time.time + _weapon.GetComponent<Weapon>().FireRate;
            _weapon.GetComponent<Weapon>().FireWeapon();
        }
    }

    private void FixedUpdate()
    {
        //player full stop 
        //_rb.velocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
        
        //player drift
        _rb.AddForce(new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed));

        Vector2 aimDirection = _mousePosition - _rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;  //faces towards target
        _rb.rotation = aimAngle;
    }

    

    
}
