using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public void restart()
    {
        ServerManager.instance.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        ServerManager.instance.restart();
    }
}
