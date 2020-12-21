﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    public float pSpeed;
    public float growScale;
    public float moveHorizontal;
    public float moveVertical;

    public float blobCooldown = 2.0f;
    public float cooldownTimer = 0.0f;

    public GameObject blobPrefab;
    public uint activeBlobID = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer) return;

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * pSpeed * Time.deltaTime);

        if(cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0.0f;
        }

        // place blob with mouseDown
        if (Input.GetMouseButtonDown(0) && cooldownTimer == 0.0f)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            SpawnBlob(Camera.main.ScreenToWorldPoint(mousePos), GetComponent<NetworkIdentity>().netId);
        }
        // release mouse to deactivate blob
        if (Input.GetMouseButtonUp(0) && activeBlobID != 0)
        {
            cooldownTimer = blobCooldown;
            DeactivateBlob(activeBlobID);
            activeBlobID = 0;
        }
    }

    // this is called on the server
    [Command]
    private void SpawnBlob(Vector3 mousePos, uint ownerPlayerID)
    {
        GameObject newBlob = Instantiate(blobPrefab, mousePos, Quaternion.identity);
        newBlob.GetComponent<BlobHaviour>().owner = this;
        NetworkServer.Spawn(newBlob);
        OnBlobSpawned(newBlob.GetComponent<NetworkIdentity>().netId, ownerPlayerID);
    }

    [ClientRpc]
    private void OnBlobSpawned(uint newBlobID, uint ownerPlayerID)
    {
        NetworkIdentity targetNetID;
        bool success = NetworkIdentity.spawned.TryGetValue(ownerPlayerID, out targetNetID);

        targetNetID.GetComponent<PlayerControl>().activeBlobID = newBlobID;
    }

    [Command]
    public void DeactivateBlob(uint targetBlobID)
    {
        RpcOnBlobDeactivate(targetBlobID);
    }

    [ClientRpc]
    void RpcOnBlobDeactivate(uint targetBlobID)
    {
        NetworkIdentity targetNetID;
        bool success = NetworkIdentity.spawned.TryGetValue(targetBlobID, out targetNetID);

        BlobHaviour targetBlob = targetNetID.GetComponent<BlobHaviour>();
        targetBlob.DeActivate();
    }

    public void Kill(PlayerControl killer)
    {
        // who killed this player?
        if (killer == this)
        {
            Debug.Log(name + " killed themselves!");
        }
        else
        {
            string killerName = killer.name;
            Debug.Log(name + " killed by " + killerName + "!");
        }

        // commence End of Game
        if (activeBlobID != 0)
        {
            cooldownTimer = blobCooldown;
            DeactivateBlob(activeBlobID);
            activeBlobID = 0;
        }
    }
}

