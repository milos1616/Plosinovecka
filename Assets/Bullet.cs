using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController)
            {
                playerController.onHit();
            }
        }
        Destroy(this.gameObject);
    }
}
