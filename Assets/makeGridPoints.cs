using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeGridPoints : MonoBehaviour {
	private int laneNumber;
	public float rockSpawnChance;
	public Transform sweetPrefab;
	public Transform rockPrefab;
	// Use this for initialization
	void Start () {
		laneNumber = GridConstants.x;

		float gridCellWidthScreen = Screen.width/(float)laneNumber;
		for (int i = 0; i < laneNumber; i++) {
			float gridScreenXPos = (i+0.5f)*gridCellWidthScreen;
			float gridWorldXPos = Camera.main.ScreenToWorldPoint(new Vector3(gridScreenXPos, 0)).x;
			GameObject newGridPoint = new GameObject();
			newGridPoint.transform.SetParent(transform);
			newGridPoint.transform.localPosition = new Vector3(gridWorldXPos, 0);
		}
		spawnRowObject();
	}
	
	void spawnRowObject() {
		Transform newObject;
		if (Random.Range(0,1f) < rockSpawnChance) {
			newObject = Instantiate(rockPrefab);
		} else {
			newObject = Instantiate(sweetPrefab);
		}
		setObjectLane(newObject);
	}

	void setObjectLane(Transform laneObject) {
		int objectLane = Random.Range(0, laneNumber);
		foreach (Transform gridPoint in transform) {
			if (gridPoint.GetSiblingIndex() == objectLane) {
				laneObject.SetParent(gridPoint);
				break;
			}
		}
	}
}
