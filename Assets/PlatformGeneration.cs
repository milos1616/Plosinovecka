using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    private const float CAMERA_DISTANCE_TRESHOLD = 300f;
    public float platformDistanceY = 100f;
    public GameObject lastPlatform;
    public GameObject platformPrefab;
    public Canvas canvas;
    public GameObject parent;

    [ServerCallback]
    void LateUpdate()
    {
        if(Time.timeScale > 0)
        {
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            if (lastPlatform.transform.position.y - this.transform.position.y <= CAMERA_DISTANCE_TRESHOLD && NetworkServer.active)
            {
                var lastPlatformPosition = lastPlatform.transform.position;
                var xSpawnPos = lastPlatformPosition.x + Random.Range(-lastPlatformPosition.x, width - lastPlatformPosition.x);
                if (xSpawnPos > lastPlatformPosition.x + 400) xSpawnPos = lastPlatformPosition.x + 300;
                if (xSpawnPos < lastPlatformPosition.x - 400) xSpawnPos = lastPlatformPosition.x - 300;
                Vector3 vector = new Vector3(xSpawnPos, lastPlatformPosition.y + platformDistanceY, 1);
                lastPlatform = Instantiate(platformPrefab, vector, this.transform.rotation);
                NetworkServer.Spawn(lastPlatform);
            }
        }
    }
}