using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobHaviour : MonoBehaviour
{
    public float growScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.localScale.x < 5) { transform.localScale += transform.localScale * growScale * Time.deltaTime; }
        //else { gameObject.SetActive(false); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Hit");

    }
}
