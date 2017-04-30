using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour {
	[HideInInspector]public List<GameObject> gridPointObjects;
	[HideInInspector]public int laneNumber;
	[HideInInspector]public Vector2 gridSizeWorld;
	public void snapObject() {
		for (int i = 0; i < gridPointObjects.Count; i++) {
			if (transform.position.y > gridPointObjects[i].transform.position.y - gridSizeWorld.y/2 && transform.position.y < gridPointObjects[i].transform.position.y + gridSizeWorld.y/2 ) {
				if (transform.position.x > gridPointObjects[i].transform.position.x - gridSizeWorld.x/2 && transform.position.x < gridPointObjects[i].transform.position.x + gridSizeWorld.x/2) {
					transform.SetParent(gridPointObjects[i].transform);
					transform.localPosition = Vector3.zero;
					checkOtherObjects();
					break;
				}
			}
		}
	}

	void checkOtherObjects() {
		dragOnHold[] objectsOnGridPoint = transform.parent.gameObject.GetComponentsInChildren<dragOnHold>();
		if (objectsOnGridPoint.Length > 1) {
			if (gameObject.CompareTag("sweet")) {
				GetComponent<objectCollisionSweet>().collide(objectsOnGridPoint[0].transform);
			} else {
				GetComponent<objectCollisionRock>().collide(objectsOnGridPoint[0].transform);
			}
		}
	}
}
