using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class NetworkManagerPlosinovecka : NetworkManager
{

    public Transform playerSpawn;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform spawnPos = playerSpawn.GetChild(numPlayers);
        GameObject player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        var playerController = player.GetComponent<PlayerController>();
        playerController.changeColor();
        playerController.playerID = numPlayers - 1;
        if (numPlayers == 2)
        {
            ServerManager.instance.initialize(numPlayers);
            GameManager.instance.play();
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Time.timeScale = 0f;
        base.OnServerDisconnect(conn);
    }
}
