using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour {

    public moveGridDown gridMoveInfo;
    private float scrollSpeed;
    private float gridScrollSpeedRatio;

    void Start() {
        gridScrollSpeedRatio = 1f/0.09f;
        setScrollSpeed();
        StartCoroutine(resetSpeed());
    }

    // Make texture on quad repeat itself for infinitely scrolling background
    void FixedUpdate()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

    IEnumerator resetSpeed() {
        for (;;) {
            yield return null;
            setScrollSpeed();
        }
    }

    void setScrollSpeed() {
        float gridMoveSpeed = gridMoveInfo.moveSpeed;
   //     Debug.Log(gridMoveSpeed);
        scrollSpeed = gridMoveSpeed/gridScrollSpeedRatio;
    }
}
