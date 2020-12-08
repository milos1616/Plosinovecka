﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    private const float CAMERA_DISTANCE_TRESHOLD = 200f;
    public float platformDistanceY = 100f;
    public GameObject lastPlatform;
    public GameObject platformPrefab;
    public Canvas canvas;
    public GameObject parent;

    void Update()
    {
        if(lastPlatform.transform.position.y - this.transform.position.y <= CAMERA_DISTANCE_TRESHOLD)
        {
            var lastPlatformPosition = lastPlatform.transform.position;
            var xSpawnPos = lastPlatformPosition.x + Random.Range(-lastPlatformPosition.x, Camera.main.pixelWidth - lastPlatformPosition.x);
            if (xSpawnPos > lastPlatformPosition.x + 400) xSpawnPos = lastPlatformPosition.x + 300; 
            if (xSpawnPos < lastPlatformPosition.x - 400) xSpawnPos = lastPlatformPosition.x - 300; 
            Vector3 vector = new Vector3(xSpawnPos, lastPlatformPosition.y + platformDistanceY, 1);
            lastPlatform = Instantiate(platformPrefab, vector, this.transform.rotation, parent.transform);
        }
    }
}
