using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkRoomPlayer : NetworkRoomPlayer
{
    [Header("Player Info")]
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerType;

    public Sprite[] typeSprites;

    public override void OnStartClient()
    {
        base.OnStartClient();
        playerType = index;
        playerName = "Player " + (index + 1);

        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("playerType", playerType);
    }
}
