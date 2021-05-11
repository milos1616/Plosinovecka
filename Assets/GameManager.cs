using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Canvas canvas;
    public GameObject deathScreen;
    public float speed;
    public float acceleration = 0.01f;
    public float maxSpeed = 50f;
    public GameObject VictoryScreen;
    public Text VictoryScreenScore;
    public GameObject LoseScreen;
    public Text LoseScreenScore;
    public GameObject ground;
    public bool gameRunning = true;
    void Start()
    {
        stop();
    }

    void Update()
    {
        if (isClientOnly && Camera.main.transform.position != new Vector3(0, 0, -1))
        {
            Camera.main.transform.position = new Vector3(0, 0, -1);
        }

        //platform move speed increase over time
        if (speed <= maxSpeed) speed += (Time.deltaTime * acceleration);
    }

    public void play()
    {
        gameRunning = true;
    }

    public void stop()
    {
        gameRunning = false;
    }

    public void restartServer()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = NetworkManagerPlosinovecka.instance.playerSpawn.GetChild(i).position;
            players[i].GetComponent<PlayerController>().resetValues(NetworkManagerPlosinovecka.instance.playerSpawn.GetChild(i).position);
            players[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        foreach (var platforms in GameObject.FindGameObjectsWithTag("Enviroment"))
        {
            Destroy(platforms);
        }
        foreach (var coins in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coins);
        }
        var Ground = Instantiate(ground, new Vector3(-19, -216, 0), Quaternion.identity);
        NetworkServer.Spawn(Ground);
        PlatformGeneration.instance.lastPlatform = Ground;
        ServerManager.instance.resetScore();
        laterStart();
    }

    public void restartClient()
    {
        play();
        GameManager.instance.LoseScreen.SetActive(false);
        GameManager.instance.VictoryScreen.SetActive(false);
        laterStart();
    }
    
    [ServerCallback]
    public void laterStart()
    {
        GameManager.instance.LoseScreen.SetActive(false);
        GameManager.instance.VictoryScreen.SetActive(false);
        play();
    }
}

