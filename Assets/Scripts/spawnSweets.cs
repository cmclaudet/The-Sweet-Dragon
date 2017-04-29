using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSweets : MonoBehaviour {
	public allSweetInformation sweetParams;
	public GameObject transformInfo;
	public Transform sweetPrefab;
	private int laneNumber;
	private float spawnFrequency;
	private float gridHeightWorld;
	private float initialYPos;
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.GetComponent<levelData>().gridSize.x;
		gridHeightWorld = getGridHeightWorld();
		initialYPos = getInitialYPos();
		spawnFrequency = getSpawnFrequency();
		StartCoroutine(spawnCountDown());
	}

	IEnumerator spawnAndDestroySweets() {
		for (;;) {
			spawnNewGridPoints();	
			yield return new WaitForSeconds(spawnFrequency);
		}
	}

	IEnumerator spawnCountDown() {
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(spawnAndDestroySweets());
	}

	void spawnNewGridPoints() {
		float[] xGridPoints = transformInfo.GetComponent<levelData>().xGridCoords;
		GameObject[] newGridPointObjects = new GameObject[xGridPoints.Length];
		for (int i = 0; i < xGridPoints.Length; i++) {
			GameObject newGridObject = new GameObject();
			newGridObject.transform.position = new Vector3(xGridPoints[i], initialYPos, 0);
			newGridObject.gameObject.tag = "gridPoint";
			transformInfo.GetComponent<moveGridDown>().gridPointObjects.Add(newGridObject);
			newGridPointObjects[i] = newGridObject;
		}
		spawnNewSweet(newGridPointObjects);
	}

	void spawnNewSweet(GameObject[] newGridPointObjects) {
		Transform newSweet = Instantiate(sweetPrefab);
		sweetData thisSweetData = new sweetData(sweetParams.sweetTypeNames.Length, sweetParams.numberOfStages);
		newSweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		newSweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetParams.allImages[thisSweetData.type].stageImages;
		newSweet.GetComponent<snapToGrid>().gridPointObjects = transformInfo.GetComponent<moveGridDown>().gridPointObjects;
		
		int lane = Random.Range(0, laneNumber);
		newSweet.transform.SetParent(newGridPointObjects[lane].transform);
		newSweet.transform.localPosition = Vector3.zero;
	}


	float getGridHeightWorld() {
		int gridHeight = transformInfo.GetComponent<levelData>().gridSize.y;
		float gridHeightPixel = Screen.height/(float)(gridHeight + 1);
		float gridHeightWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2 + gridHeightPixel)).y;
		return gridHeightWorld;
	}

	float getInitialYPos() {
		float screenHeightWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;
		float initialYPos = screenHeightWorld + gridHeightWorld/2;
		return initialYPos;
	}

	float getSpawnFrequency() {
		float spawnFreq = gridHeightWorld/transformInfo.GetComponent<levelData>().gridMoveSpeed;
		return spawnFreq;
	}
}
