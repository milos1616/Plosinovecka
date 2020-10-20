﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpPower = 700;
    public float moveSpeed = 100;
    public Canvas canvas;
    public GameObject deathScreen;
    public KeyCode buttonJump;
    public KeyCode buttonLeft;
    public KeyCode buttonRight;
    public KeyCode buttonFire;
    public GameObject bullet;
    public float shotPower = 100;

    int jumpRemain = 2;

    void Update()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(buttonFire))
        {
            GameObject bulletObject = Instantiate(bullet, transform.GetChild(0));
            bulletObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(shotPower, bulletObject.transform.position.y));
        }
        if(Input.GetKeyDown(buttonLeft))
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(buttonRight))
        {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        if (Input.GetKeyDown(buttonJump))
        {
            if (jumpRemain > 0)
            {
                rb.AddForce(new Vector2(0, jumpPower));
                jumpRemain--;
            }
        }
        if (transform.position.x > canvas.pixelRect.width)
        {
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x < 0)
        {
            transform.position = new Vector2(canvas.pixelRect.width, transform.position.y);
        }
    
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            Vector2 hit = collision.contacts[0].normal;
            float angle = Vector2.Angle(hit, Vector2.down);
            if (Mathf.Approximately(angle, 180))
            {
                jumpRemain = 2;
            }          
        }
        else if (collision.gameObject.tag == "Death")
        {
            Time.timeScale = 0f;
            deathScreen.SetActive(true);
        }  
    }


}
