using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCollision : MonoBehaviour
{
    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ServerManager.instance.addScore(collision.gameObject.GetComponent<PlayerController>(), 1, collision.gameObject.GetComponentInChildren<Text>());
            Destroy(this.gameObject);
        }
    }
}
