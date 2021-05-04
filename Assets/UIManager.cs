using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void restart()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            var playerController = obj.GetComponent<PlayerController>();
            if (playerController.isLocalPlayer)
            {
                playerController.restartCommand();
            }
        }

    }
}
