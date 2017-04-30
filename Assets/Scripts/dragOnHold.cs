using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*detects touch input on object and follows player finger on dragging
  if touch phase ends object snapping to grid logic is executed in separate script
  Does not allow dragging when time scale == 0 as this means the player is on gameover
 */
public class dragOnHold : MonoBehaviour {
	private bool dragging;
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (Time.timeScale != 0) {	//so that player cannot drag anything on game over screen
				detectInitialTouch();
				dragObject();
			}
		}
	}

	void detectInitialTouch() {
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            if(hitInfo)	//if player finger hits something
            {
				if (hitInfo.transform.gameObject == gameObject) {
					transform.parent = null;
                	dragging = true;
				}
            }
        }
	}

	void dragObject() {
		if (dragging) {
			Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = new Vector3 (fingerPos.x, fingerPos.y, transform.position.z);
			if (Input.GetTouch(0).phase == TouchPhase.Ended || Time.timeScale == 0) {
				dragging = false;
				GetComponent<snapToGrid>().snapObject();
			}
		}
	}
}
