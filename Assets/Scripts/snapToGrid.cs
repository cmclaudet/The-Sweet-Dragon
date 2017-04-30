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
				}
			}
		}
	}

	void checkOtherObjects() {
		dragOnHold[] objectsOnGridPoint = transform.parent.gameObject.GetComponentsInChildren<dragOnHold>();
		if (objectsOnGridPoint.Length > 1) {
			if (gameObject.CompareTag("sweet")) {
				checkOnSweetCollision(objectsOnGridPoint[0].gameObject);
			} else {
				if (objectsOnGridPoint[0].CompareTag("sweet")) {
					Destroy(objectsOnGridPoint[0].gameObject);
				} else {
					Destroy(gameObject);
				}
			}
		}
	}

	void checkOnSweetCollision(GameObject otherObject) {
		if (otherObject.CompareTag("sweet")) {
			if (otherObject.GetComponent<sweetAttributes>().thisSweetData.type == GetComponent<sweetAttributes>().thisSweetData.type) {
				mergeSweets(otherObject.transform, gameObject.transform);
			} else {
				Destroy(gameObject);
			}
		} else {
			Destroy(gameObject);
		}
	}

	void mergeSweets(Transform sweet1, Transform sweet2) {
		int newStage = sweet1.GetComponent<sweetAttributes>().thisSweetData.stage + sweet2.GetComponent<sweetAttributes>().thisSweetData.stage;
		sweet1.GetComponent<sweetAttributes>().setNewStage(newStage);
		Destroy(sweet2.gameObject);
	}

}
