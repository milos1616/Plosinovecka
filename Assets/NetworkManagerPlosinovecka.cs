using UnityEngine;

namespace Mirror.Examples.Pong
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerPlosinovecka : NetworkManager
    {

        public Transform players;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? players.GetChild(0) : players.GetChild(1);
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation, players);
            NetworkServer.Spawn(player);
            NetworkServer.AddPlayerForConnection(conn, player);

            if (numPlayers == 2)
            {
                //game starts
                Time.timeScale = 1f;
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
            Time.timeScale = 0f;
        }
    }
}
