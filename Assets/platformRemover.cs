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
                var controller = P.GetComponent<PlayerController>();
                if (P != collision.collider.gameObject)
                {
                    if(controller.isLocalPlayer)
                    {
                        Debug.Log("Vyhrals noumo");
                    }
                    else
                    {
                        controller.victory();
                    }
                }
                else
                {
                    if (controller.isLocalPlayer)
                    {
                        Debug.Log("Prohrals noumo");
                    }
                    else
                    {
                        controller.defeat();
                    }
                }
            }
            //Destroy(collision.collider.gameObject);
            GameManager.instance.stop();
        }
    }
}
