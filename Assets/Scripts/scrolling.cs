using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour {

    public float speed = 0.4f;

    // Make texture on quad repeat itself for infinitely scrolling background
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
