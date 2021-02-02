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
    }
}
