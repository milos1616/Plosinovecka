﻿using System.Collections;
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

    void Start()
    {
        Time.timeScale = 0f;
    }

}
