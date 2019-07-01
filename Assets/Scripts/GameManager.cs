using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string PLAYER_ID_PREF = "Player ";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void registerPlayer(string netId, Player player)
    {
        string playerId = PLAYER_ID_PREF + netId;
        players.Add(playerId, player);
        player.transform.name = "Player " + netId;
    }

    public static void unregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player getPlayer(string playerId)
    {
        return players[playerId];
    }

    public static void playerLose(string playerId)
    {

        foreach (KeyValuePair<string, Player> entry in players)
        {
            if (entry.Key.Equals(playerId))
            {
                entry.Value.RpcLose();
            }

        }

        foreach (KeyValuePair<string, Player> entry in players)
        {
            if (!entry.Key.Equals(playerId))
            {
                entry.Value.RpcWin();
            }
        }
    }

}
