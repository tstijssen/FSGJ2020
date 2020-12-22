using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobHaviour : MonoBehaviour
{
    public float growSpeed;
    public float maxSize;
    public bool isActive;
    public PlayerControl owner;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Blob activated");
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if(transform.localScale.x < maxSize)
            {
                transform.localScale += transform.localScale * growSpeed * Time.deltaTime;
            }
            else
            {
                DeActivate();
                //owner.DeactivateBlob();
            }
        }
    }

    public void DeActivate()
    {
        isActive = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // change texture
        GetComponent<SpriteRenderer>().color = Color.black;

        Debug.Log("Blob set inactive");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isActive && other.TryGetComponent(out PlayerControl otherPlayer))
        {
            otherPlayer.Kill(owner);
        }
    }
}
