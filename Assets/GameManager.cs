using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    void Start()
    {
        stop();
    }

    void Update()
    {
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
}
