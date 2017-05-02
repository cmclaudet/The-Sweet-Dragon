using UnityEngine;
using System.Collections;
/*Makes background scroll infinitely
  grid to scroll speed ratio defines the ratio between grid move speed and background scroll speed so that they look like they're going at the same speed
 */
public class scrolling : MonoBehaviour {

    private float scrollSpeed;
    private float tileSizeZ;

    private Vector3 startPosition;

    void Start ()
    {
        startPosition = transform.position;
        scrollSpeed = GridConstants.speed;
        tileSizeZ = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y*2f;
    }

    void FixedUpdate ()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition - Vector3.up * newPosition;
    }
}
