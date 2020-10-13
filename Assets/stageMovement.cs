using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + Time.deltaTime*speed);
    }
}
