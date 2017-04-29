using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragOnHold : MonoBehaviour {
	private bool dragging;
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			detectInitialTouch();
			dragObject();
		}
	}

	void detectInitialTouch() {
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            if(hitInfo)
            {
				if (hitInfo.transform.gameObject == gameObject) {
                	dragging = true;
				}
            }
        }
	}

	void dragObject() {
		if (dragging) {
			Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = new Vector3 (fingerPos.x, fingerPos.y, transform.position.z);
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				dragging = false;
//				GetComponent<snapToGrid>().snapPosition();
			}
		}
	}
}
