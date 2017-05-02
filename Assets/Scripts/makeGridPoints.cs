using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeGridPoints : MonoBehaviour {
	private int laneNumber;
	public float rockSpawnChance;
	public Transform sweetPrefab;
	public Transform rockPrefab;
	[HideInInspector]public allSweetInformation sweetImageInfo;
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
		if (GetComponent<spawnObject>() != null) {
			setupSpawnObjectComponent();
			GetComponent<spawnObject>().spawnRowObject();
		}
	}

	void setupSpawnObjectComponent() {
		GetComponent<spawnObject>().rockPrefab = rockPrefab;
		GetComponent<spawnObject>().sweetPrefab = sweetPrefab;
		GetComponent<spawnObject>().rockSpawnChance = rockSpawnChance;
		GetComponent<spawnObject>().sweetImageInfo = sweetImageInfo;
	}
	

}
