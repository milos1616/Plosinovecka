using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
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
    private bool stunned = false;
    int jumpRemain = 2;
    float stunTimer = 0f;
    public float stunTime = 2f;
    private bool right = true;
    public int gunDistance = 7;
    public float fireRate = 1f;
    private bool canFire = false;
    private float fireTimer = 0f;
    public int playerID;
    public int score = 0;
    

    private void Start()
    {
        canvas = GameManager.instance.canvas;
        deathScreen = GameManager.instance.deathScreen;

        if (isClientOnly)
        {
            GameManager.instance.play();
        }
    }

    [Command]
    private void fire(Vector2 bulletPosition, Vector2 bulletForce)
    {
        GameObject bulletObject = Instantiate(bullet, bulletPosition, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletForce);
        NetworkServer.Spawn(bulletObject);
    }
    void Update()
    {
        if (isLocalPlayer)
        {

            if (!stunned)
            {

                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

                if (Input.GetKeyDown(buttonFire) && canFire)
                {
                    var gunPosition = transform.GetChild(0).position;
                    Vector2 bulletPosition;
                    Vector2 bulletForce;
                    if (right)
                    {
                        bulletPosition = new Vector2(gunPosition.x + 10, gunPosition.y);
                        bulletForce = new Vector2(shotPower, bulletPosition.y);
                    }
                    else
                    {
                        bulletPosition = new Vector2(gunPosition.x - 10, gunPosition.y);
                        bulletForce = new Vector2(-shotPower, bulletPosition.y);
                    }
                    fire(bulletPosition, bulletForce);
                    canFire = false;
                    fireTimer = 0f;
                }
                if (Input.GetKey(buttonLeft))
                {
                    rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
                    right = false;
                    transform.GetChild(0).transform.position = new Vector2(transform.position.x - gunDistance, transform.position.y);
                }
                if (Input.GetKey(buttonRight))
                {
                    rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
                    right = true;
                    transform.GetChild(0).transform.position = new Vector2(transform.position.x + gunDistance, transform.position.y);

                }
                if (Input.GetKeyDown(buttonJump))
                {
                    if (jumpRemain > 0)
                    {
                        rb.AddForce(new Vector2(0, jumpPower));
                        jumpRemain--;
                    }
                }

                Camera cam = Camera.main;
                float height = 2f * cam.orthographicSize;
                float width = height * cam.aspect;
                if (transform.position.x > cam.transform.position.x + width / 2)
                {
                    transform.position = new Vector2(cam.transform.position.x - width / 2, transform.position.y);
                }
                if (transform.position.x < cam.transform.position.x - width / 2)
                {
                    transform.position = new Vector2(cam.transform.position.x + width / 2, transform.position.y);
                }
            }

            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime)
            {
                stunned = false;
            }

            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                canFire = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            Vector2 hit = collision.contacts[0].normal;
            float angle = Vector2.Angle(hit, Vector2.down);
            if (angle > 170 && angle < 190)
            {
                jumpRemain = 2;
            }
        }
    }

    [ClientRpc]
    public void onHit()
    {
        stunned = true;
        stunTimer = 0f;
    }

    [ClientRpc]
    public void changeColor()
    {
        if (isLocalPlayer)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    [ClientRpc]
    public void updateScoreText(int score)
    {
        this.score = score;
        GetComponentInChildren<Text>().text = score.ToString();
    }

    [ClientRpc]
    public void victory()
    {
        if (!isClientOnly) return;
        Debug.Log("vyhrals noumo");
        GameManager.instance.VictoryScreen.SetActive(true);
        GameManager.instance.VictoryScreenScore.text = score.ToString();
        GameManager.instance.stop();
    }

    [ClientRpc]
    public void defeat()
    {
        if (!isClientOnly) return;
        Debug.Log("prohrals noumo");
        GameManager.instance.LoseScreen.SetActive(true);
        GameManager.instance.LoseScreenScore.text = score.ToString();
        GameManager.instance.stop();
    }
}
