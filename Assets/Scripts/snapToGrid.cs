using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour {
	[HideInInspector]public List<GameObject> gridPointObjects;
	[HideInInspector]public int laneNumber;
	[HideInInspector]public Vector2 gridSizeWorld;
	public void snapSweet() {
		for (int i = 0; i < gridPointObjects.Count; i++) {
			if (transform.position.y > gridPointObjects[i].transform.position.y - gridSizeWorld.y/2 && transform.position.y < gridPointObjects[i].transform.position.y + gridSizeWorld.y/2 ) {
				if (transform.position.x > gridPointObjects[i].transform.position.x - gridSizeWorld.x/2 && transform.position.x < gridPointObjects[i].transform.position.x + gridSizeWorld.x/2) {
					transform.SetParent(gridPointObjects[i].transform);
					transform.localPosition = Vector3.zero;
					checkOtherSweets();
				}
			}
		}
	}

	void checkOtherSweets() {
		sweetAttributes[] sweetsOnGridPoint = transform.parent.gameObject.GetComponentsInChildren<sweetAttributes>();
		if (sweetsOnGridPoint.Length > 1) {
			if (sweetsOnGridPoint[0].CompareTag("sweet") && gameObject.CompareTag("sweet")) {
				if (sweetsOnGridPoint[0].GetComponent<sweetAttributes>().thisSweetData.type == GetComponent<sweetAttributes>().thisSweetData.type) {
					mergeSweets(sweetsOnGridPoint[0].transform, gameObject.transform);
				} else {
					Destroy(transform.gameObject);
				}
			} else if (sweetsOnGridPoint[0].CompareTag("sweet") && gameObject.CompareTag("rock")) {
				Destroy(sweetsOnGridPoint[0].gameObject);
			} else if (sweetsOnGridPoint[0].CompareTag("rock") && gameObject.CompareTag("sweet")) {
				Destroy(gameObject);
			} else {
				Destroy(gameObject);
			}
		}
	}

	void mergeSweets(Transform sweet1, Transform sweet2) {
		int newStage = sweet1.GetComponent<sweetAttributes>().thisSweetData.stage + sweet2.GetComponent<sweetAttributes>().thisSweetData.stage;
		sweet1.GetComponent<sweetAttributes>().setNewStage(newStage);
		Destroy(sweet2.gameObject);
	}

}
