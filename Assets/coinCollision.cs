using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
    }
}
