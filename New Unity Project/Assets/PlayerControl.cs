using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float pSpeed;
    public float growScale;
    public float moveHorizontal;
    public float moveVertical;
    public GameObject blob;
    public GameObject rock;


    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * pSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.Space)) { DropBlob(); }
        if (Input.GetMouseButton(0))
        {
            if (!blob.activeSelf)
            {
                

                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

                blob.transform.localScale = Vector3.one;
                blob.transform.position = Camera.main.ScreenToWorldPoint(mousePos); ;
                blob.SetActive(true);
            }
            if (blob.transform.localScale.x < 5) { blob.transform.localScale += blob.transform.localScale * growScale * Time.deltaTime; }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (blob.activeSelf)
            {
                Instantiate(rock, blob.transform.position, blob.transform.rotation);
                rock.transform.localScale = blob.transform.localScale;
                blob.SetActive(false);
            }
        }
    }


    void DropBlob()
    {
        if (!blob.activeSelf) 
        {
            blob.transform.localScale = Vector3.one;
            blob.transform.position = transform.position;
            blob.SetActive(true); 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
    }
}

