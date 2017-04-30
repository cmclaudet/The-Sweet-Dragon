using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSweets : MonoBehaviour {
	public allSweetInformation sweetParams;
	public GameObject transformInfo;
	public Transform sweetPrefab;
	public Transform rockPrefab;
	private float rockSpawnChance;
	private int laneNumber;
	private float spawnFrequency;
	private float gridHeightWorld;
	private float gridWidthWorld;
	private float initialYPos;
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.GetComponent<levelData>().gridSize.x;
		gridHeightWorld = transformInfo.GetComponent<levelData>().gridHeightWorld;
		gridWidthWorld = transformInfo.GetComponent<levelData>().gridWidthWorld;
		rockSpawnChance = transformInfo.GetComponent<levelData>().rockSpawnChance;
		initialYPos = getInitialYPos();
		spawnFrequency = getSpawnFrequency();
		StartCoroutine(spawnCountDown());
	}

	IEnumerator spawnAndDestroyGridPoints() {
		for (;;) {
			spawnNewGridPoints();
			removeOldGridPoints();
			yield return new WaitForSeconds(spawnFrequency);
		}
	}

	IEnumerator spawnCountDown() {
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(spawnAndDestroyGridPoints());
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
		spawnNewObject(newGridPointObjects);
	}

	void spawnNewObject(GameObject[] newGridPointObjects) {
		Transform newObject;
		if (Random.Range(0, 1f) < rockSpawnChance) {
			newObject = Instantiate(rockPrefab);
		} else {
			newObject = Instantiate(sweetPrefab);
			setupSweetData(newObject);
		}
		setupGridSnappingData(newObject);
		setObjectToGridPoint(newGridPointObjects, newObject);
	}

	void setupSweetData(Transform sweet) {
		sweetData thisSweetData = new sweetData(sweetParams.sweetTypeNames.Length, sweetParams.numberOfStages);
		sweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		sweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetParams.allImages[thisSweetData.type].stageImages;
	}

	void setupGridSnappingData(Transform laneObject) {
		laneObject.GetComponent<snapToGrid>().gridPointObjects = transformInfo.GetComponent<moveGridDown>().gridPointObjects;
		laneObject.GetComponent<snapToGrid>().laneNumber = laneNumber;
		laneObject.GetComponent<snapToGrid>().gridSizeWorld = new Vector2(gridWidthWorld, gridHeightWorld);
	}

	void setObjectToGridPoint(GameObject[] gridPointObjects, Transform laneObject) {
		int lane = Random.Range(0, laneNumber);
		laneObject.transform.SetParent(gridPointObjects[lane].transform);
		laneObject.transform.localPosition = Vector3.zero;
	}

	void removeOldGridPoints() {
		for (int i = 0; i < laneNumber; i++) {
			Destroy(transformInfo.GetComponent<moveGridDown>().gridPointObjects[i]);
			transformInfo.GetComponent<moveGridDown>().gridPointObjects.RemoveAt(i);
		}
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
