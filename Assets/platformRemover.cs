using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRemover : MonoBehaviour
{
    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            Destroy(collision.collider.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var P in players)
            {
                if (P != collision.collider.gameObject)
                {
                    P.GetComponent<PlayerController>().victory();
                }
                else
                {
                    P.GetComponent<PlayerController>().defeat();
                }
            }
            Destroy(collision.collider.gameObject);
            GameManager.instance.stop();
        }
    }
}
