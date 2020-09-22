using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpPower = 700;
    public float moveSpeed = 100;
    
    int jumpRemain = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 position = this.transform.position;
            Vector2 direction = new Vector2(position.x - 1, position.y);
            this.transform.position = Vector2.MoveTowards(position, direction, Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 position = this.transform.position;
            Vector2 direction = new Vector2(position.x + 1, position.y);
            this.transform.position = Vector2.MoveTowards(position, direction, Time.deltaTime * moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpRemain > 0)
            {
                rb.AddForce(new Vector2(0, jumpPower));
                jumpRemain--;
            }
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
