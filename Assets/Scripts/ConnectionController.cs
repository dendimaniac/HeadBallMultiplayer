using UnityEngine;
using Bolt.Matchmaking;
using System;
using UdpKit;

public class ConnectionController : Bolt.GlobalEventListener
{
    public void StartServer()
    {
        if (BoltNetwork.IsRunning)
        {
            BoltLauncher.Shutdown();
        }
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        if (BoltNetwork.IsRunning)
        {
            BoltLauncher.Shutdown();
        }
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (!BoltNetwork.IsServer) return;
        
        var matchName = Guid.NewGuid().ToString();

        BoltMatchmaking.CreateSession(matchName, null, "Game");
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

        foreach (var session in sessionList)
        {
            var photonSession = session.Value;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
