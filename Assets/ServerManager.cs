using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void addScore(int id, int amount)
    {
        score[id] += amount;
    }
}
