using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class AIChase : MonoBehaviour
{

    private Player _player;

    [SerializeField] private float _speed;
    
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    public void Chase() //follow player while facing target
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.position =
            Vector2.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
