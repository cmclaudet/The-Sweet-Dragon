using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNewGridRows : MonoBehaviour {
	[HideInInspector]public Transform gridRowPrefab;
	[HideInInspector]public allSweetInformation sweetImageInfo;
	private float moveSpeed;
	private int gridYSize;
	private float spawnPeriod;
	private float initialYPos;
	private float gridHeightScreen;
	// Use this for initialization
	void Start () {
		moveSpeed = GridConstants.speed;
		gridYSize = GridConstants.y;
		gridHeightScreen = Screen.height/gridYSize;
		initialYPos = setInitialYPos();
		spawnPeriod = setSpawnPeriod();
		StartCoroutine(spawnAndDestroyGridRows());
	}
	
	IEnumerator spawnAndDestroyGridRows() {
		for (;;) {
			spawnNewGridRow();
			destroyOldGridRow();
			yield return new WaitForSeconds(spawnPeriod);
		}
	}

	float setInitialYPos() {
		float initialYPos = Camera.main.ScreenToWorldPoint(new Vector3(0, (gridYSize + 0.5f)*gridHeightScreen)).y;
		return initialYPos;
	}

	float setSpawnPeriod() {
		float spawnPeriod = GridConstants.gridSizeWorld.y/moveSpeed;
		return spawnPeriod;
	}

	void spawnNewGridRow() {
		Transform newGridRow = Instantiate(gridRowPrefab);
		newGridRow.GetComponent<makeGridPoints>().sweetImageInfo = sweetImageInfo;
		newGridRow.gameObject.AddComponent<spawnObject>();
		newGridRow.SetParent(transform);
		newGridRow.localPosition = new Vector3(0, initialYPos);
	}

	void destroyOldGridRow() {
		Destroy(transform.GetChild(0).gameObject);
	}
}
