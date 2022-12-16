using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 5f;
    private float _timeInterval = 3.2f;
    private float _subtractedTimeIntervalPerRound = 0.15f;
    private float _minTimeInterval = 0.20f;
    private int _counter = 0;
    private int _timePerRound = 30;

    private ObjectPool _enemyPool;

    //public GameObject[] enemies;  CAN BE USED FOR MULTIPLE ENEMIES
    [SerializeField] private GameObject _enemyPrefab;

    private void Awake()
    {
        _enemyPool = GetComponent<ObjectPool>();
        _enemyPool.Initialize(_enemyPrefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(RoundCounter());  //can be commented
    }

    IEnumerator SpawnEnemy()  //spawns enemy based on radius around player
    {
        Vector2 spawnPos = FindObjectOfType<Player>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * _spawnRadius;
        
        //Instantiate(_enemyPrefab,spawnPos, Quaternion.identity);

        GameObject enemy = _enemyPool.GetObject(spawnPos);
        enemy.SetActive(true);
        yield return new WaitForSeconds(_timeInterval);
        StartCoroutine(SpawnEnemy());  //loops
    }

    IEnumerator RoundCounter()  // can be commented
    {
        _counter++;  //add counts per round
        
        Debug.Log($"Round: {_counter}");
        
        if (_timeInterval > _minTimeInterval) // prevents to make interval to 0
        {
            _timeInterval -= _subtractedTimeIntervalPerRound;  //subtracts interval time per round to make it harder
        }
        yield return new WaitForSeconds(_timePerRound);
        StartCoroutine(RoundCounter()); 
    }
}
