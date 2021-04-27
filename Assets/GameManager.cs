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
    void Start()
    {
        stop();
    }

    void Update()
    {
        if(isClientOnly && Camera.main.transform.position != new Vector3(0,0,-1))
        {
            Camera.main.transform.position = new Vector3(0, 0, -1);
        }

        //platform move speed increase over time
        if (speed <= maxSpeed) speed += (Time.deltaTime * acceleration);
    }

    public void play()
    {
        Time.timeScale = 1f;
    }

    public void stop()
    {
        Time.timeScale = 0f;
    }

    public void restartServer()
    {
        Time.timeScale = 1f;
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = NetworkManagerPlosinovecka.instance.playerSpawn.GetChild(i).position;
            players[i].GetComponent<PlayerController>().resetValues();
        }
        foreach (var platforms in GameObject.FindGameObjectsWithTag("Enviroment"))
        {
            Destroy(platforms);
        }
        foreach(var coins in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coins);
        }
        var Ground = Instantiate(ground, new Vector3(-19, -216, 0), Quaternion.identity);
        NetworkServer.Spawn(Ground);
        PlatformGeneration.instance.lastPlatform = Ground;
        ServerManager.instance.resetScore();
        GameManager.instance.LoseScreen.SetActive(false);
        GameManager.instance.VictoryScreen.SetActive(false);
    }

    public void restartClient()
    {
        Time.timeScale = 1f;
        GameManager.instance.LoseScreen.SetActive(false);
        GameManager.instance.VictoryScreen.SetActive(false);
    }
}
