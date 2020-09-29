using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    private const float CAMERA_DISTANCE_TRESHOLD = 200f;
    public GameObject lastPlatform;
    public GameObject platformPrefab;
    public Canvas canvas;

    void Update()
    {
        if(lastPlatform.transform.position.y - this.transform.position.y <= CAMERA_DISTANCE_TRESHOLD)
        {
            var lastPlatformPosition = lastPlatform.transform.position;
            var xSpawnPos = lastPlatformPosition.x + Random.Range(-lastPlatformPosition.x, canvas.pixelRect.width - lastPlatformPosition.x);
            Vector3 vector = new Vector3(xSpawnPos, lastPlatformPosition.y + 50, 1);
            lastPlatform = Instantiate(platformPrefab, vector, this.transform.rotation, canvas.transform);
        }
    }
}
