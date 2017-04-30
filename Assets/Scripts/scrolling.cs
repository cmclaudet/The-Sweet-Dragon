using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour {

    public levelData thisLevelData;
    private float scrollSpeed;

    void Start() {
        float gridMoveSpeed = thisLevelData.gridMoveSpeed;
        float gridScrollSpeedRatio = 1f/0.091f;
        scrollSpeed = gridMoveSpeed/gridScrollSpeedRatio;
    }

    // Make texture on quad repeat itself for infinitely scrolling background
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
