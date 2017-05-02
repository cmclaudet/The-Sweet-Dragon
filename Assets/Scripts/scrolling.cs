using UnityEngine;
using System.Collections;
/*Makes background scroll infinitely
  grid to scroll speed ratio defines the ratio between grid move speed and background scroll speed so that they look like they're going at the same speed
 */
public class scrolling : MonoBehaviour {

    private float scrollSpeed;

    private Vector3 startPosition;

    void Start ()
    {
        startPosition = transform.position;
        scrollSpeed = GridConstants.speed;
    }

    void FixedUpdate ()
    {
        scrollSpeed = GridConstants.speed;
        transform.position -= Vector3.up * Time.fixedDeltaTime*scrollSpeed;
        if (transform.position.y <= 0) {
            transform.position = startPosition;
        }
    }
}
