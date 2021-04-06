using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                        GameManager.instance.VictoryScreen.SetActive(true);
                        GameManager.instance.VictoryScreenScore.text = controller.score.ToString();
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
                        GameManager.instance.LoseScreen.SetActive(true);
                        GameManager.instance.LoseScreenScore.text = controller.score.ToString();
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
