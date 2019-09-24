using UnityEngine;

[BoltGlobalBehaviour(BoltScenes.Game)]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        // randomize a position
        var spawnPosition = new Vector2(Random.Range(-2f, 2f), -2f);

        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.Player, spawnPosition, Quaternion.identity);
    }
}