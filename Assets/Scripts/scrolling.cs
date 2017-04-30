using UnityEngine;
using System.Collections;
/*Makes background scroll infinitely
  grid to scroll speed ratio defines the ratio between grid move speed and background scroll speed so that they look like they're going at the same speed
 */
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
