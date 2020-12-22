using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    Color lerpedColor = Color.red;
    Text title;
    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lerpedColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
        title.color = lerpedColor;
    }
}
