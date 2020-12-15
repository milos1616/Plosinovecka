using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageMovement : MonoBehaviour
{
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Time.deltaTime*GameManager.instance.speed);

    }
}
