using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragOnHold : MonoBehaviour {
	private bool dragging;
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
            if(hitInfo)
            {
				if (hitInfo.transform.gameObject == gameObject) {
                	dragging = true;
                	// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				}
            }
        }

		if (dragging) {
			Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = new Vector3 (fingerPos.x, fingerPos.y, transform.position.z);
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				dragging = false;
			}
		}
	}
	}
}
