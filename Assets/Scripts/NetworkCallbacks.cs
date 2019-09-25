using UnityEngine;

[BoltGlobalBehaviour(BoltScenes.Game)]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        // randomize a position
        var playerSpawnPosition = new Vector2(Random.Range(-2f, 2f), -2f);
        var ballSpawnPosition = new Vector2(0, 1f);

        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.Player, playerSpawnPosition, Quaternion.identity);
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(BoltPrefabs.Ball, ballSpawnPosition, Quaternion.identity);
        }
    }
}