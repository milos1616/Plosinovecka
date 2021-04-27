using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{

    #region Singleton

    public static ServerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    
    //MA BYT POUZE NA SERVERU !!!!! **DULEZITE** SILENE
    public int[] score;
    public void initialize(int playerCount)
    {
        score = new int[playerCount];
        for(int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
        }
    }
    
    public void addScore(PlayerController player, int amount, Text scoreText)
    {
        int id = player.playerID;
        score[id] += amount;
        player.updateScoreText(score[id]);
    }
    
    public void resetScore()
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
        }
    }

    [ServerCallback]
    public void restart()
    {
        GameManager.instance.restartServer();
    }
}
