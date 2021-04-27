using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageMovement : NetworkBehaviour
{
    void Update()
    {
        if (isServer && GameManager.instance.gameRunning)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Time.deltaTime * GameManager.instance.speed);
        }
        

    }
}
