using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float pSpeed;
    public float growScale;
    public float moveHorizontal;
    public float moveVertical;

    public float blobCooldown = 2.0f;
    public float cooldownTimer = 0.0f;

    public Transform blobPrefab;
    Transform activeBlob;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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

            activeBlob = Instantiate(blobPrefab, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            activeBlob.GetComponent<BlobHaviour>().owner = this;
        }
        // release mouse to deactivate blob
        if (Input.GetMouseButtonUp(0))
        {
            if (activeBlob)
            {
                cooldownTimer = blobCooldown;
                activeBlob.GetComponent<BlobHaviour>().DeActivate();
                activeBlob = null;
            }
        }
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
        if (activeBlob)
        {
            activeBlob.GetComponent<BlobHaviour>().DeActivate();
            activeBlob = null;
        }

    }
}

