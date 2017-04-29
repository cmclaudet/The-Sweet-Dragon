using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour {
	[HideInInspector]public List<GameObject> gridPointObjects;
	public void snapSweet() {
		foreach (GameObject gridPointObject in gridPointObjects) {
			if (transform.position.y < gridPointObject.transform.position.y) {
				if (transform.position.x < gridPointObject.transform.position.x) {
					transform.SetParent(gridPointObject.transform);
				}
			}
		}
	}

}
