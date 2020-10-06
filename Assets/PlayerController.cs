using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpPower = 700;
    public float moveSpeed = 100;
    public Canvas canvas;
    
    int jumpRemain = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpRemain > 0)
            {
                rb.AddForce(new Vector2(0, jumpPower));
                jumpRemain--;
            }
        }
        if (transform.position.x > canvas.pixelRect.width)
        {
            transform.position = new Vector2(-canvas.pixelRect.width, transform.position.y);
        }
        if (transform.position.x < -canvas.pixelRect.width)
        {
            transform.position = new Vector2(canvas.pixelRect.width, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enviroment")
        {
            jumpRemain = 2;
        }
    }
}
