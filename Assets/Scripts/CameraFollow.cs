using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

public class CameraFollow : MonoBehaviour
{
    //for tracking player
    public Transform target;
    public float smoothSpeed;
    public float shakeDuration;
    private Vector3 tempPos;
    public Vector3 minVal, maxVal;
    
    //for camera shake
    private Vector3 _initialPos;
    public float shakeRadius;

    private void OnEnable()
    {
        Enemy.enemyKill += CameraShake;
    }

    private void OnDisable()
    {
        Enemy.enemyKill -= CameraShake;
    }

    void FixedUpdate()
    {
        //follow player
        if (!target)
        {
            return;
        }
        
        tempPos = transform.position;
        tempPos.x = target.position.x;
        tempPos.y = target.position.y;
        
        //sets camera bounds, camera stops at certain position
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(tempPos.x,minVal.x,maxVal.x),
            Mathf.Clamp(tempPos.y,minVal.y,maxVal.y),
            Mathf.Clamp(tempPos.z,minVal.z,maxVal.z)
        );
        
        //adds delay effect on following player
        Vector3 smoothedPos = Vector3.Lerp(transform.position, boundPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
        
    }

    private void CameraShake()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        _initialPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            Vector2 shakePos = _initialPos;
            shakePos += Random.insideUnitCircle.normalized * shakeRadius;
            transform.position = new Vector3(shakePos.x,shakePos.y,_initialPos.z);
            yield return null;
        }

    }
}
