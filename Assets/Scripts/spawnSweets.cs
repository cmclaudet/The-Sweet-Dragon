using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSweets : MonoBehaviour {
	public allSweetInformation sweetParams;
	public levelData transformInfo;
	public Transform sweetPrefab;
	private int laneNumber;
	private float moveSpeed;
	private float spawnFrequency;
	private float gridHeightWorld;
	private float initialYPos;
	private List<Vector2> gridPoints = new List<Vector2>();
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.gridSize.x;
		moveSpeed = transformInfo.spawnMoveSpeed;
		setGridPoints();
		gridHeightWorld = getGridHeightWorld();
		initialYPos = getInitialYPos();
		spawnFrequency = getSpawnFrequency();
		StartCoroutine(spawnAndDestroySweets());
	}

	void setGridPoints() {
		for (int i = 0; i < transformInfo.xGridCoords.Length; i++) {
			for (int j = 0; j < transformInfo.yGridCoords.Length; j++) {
				gridPoints.Add(new Vector2(transformInfo.xGridCoords[i], transformInfo.yGridCoords[j]));
			}
		}
	}

	IEnumerator spawnAndDestroySweets() {
		for (;;) {
			spawnNewGridPoints();
			spawnNewSweet();
			yield return new WaitForSeconds(spawnFrequency);
		}
	}

	void spawnNewGridPoints() {
		foreach (float xPoint in transformInfo.xGridCoords) {
			gridPoints.Add(new Vector2(xPoint, initialYPos));
		}
	}

	void spawnNewSweet() {
		Transform newSweet = Instantiate(sweetPrefab);
		sweetData thisSweetData = new sweetData(sweetParams.sweetTypeNames.Length, sweetParams.numberOfStages);
		newSweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		newSweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetParams.allImages[thisSweetData.type].stageImages;
		newSweet.GetComponent<snapToGrid>().gridXPos = transformInfo.xGridCoords;
		newSweet.GetComponent<snapToGrid>().gridYPos = transformInfo.yGridCoords;
		newSweet.GetComponent<snapToGrid>().spawnFrequency = spawnFrequency;
		setSweetInitialLocation(newSweet);
		setSweetSpeed(newSweet);
	}

	void setSweetInitialLocation(Transform sweet) {
		int sweetLane = Random.Range(0,laneNumber);
		float lanePixelWidth = (Screen.width)/(float)laneNumber;
		float sweetXPixelPos = ((float)sweetLane + 0.5f)*lanePixelWidth;
		float initialXPos = Camera.main.ScreenToWorldPoint(new Vector3(sweetXPixelPos, 0)).x;
		sweet.position = new Vector3 (initialXPos, initialYPos, 0);
	}

	void setSweetSpeed(Transform sweet) {
		sweet.GetComponent<moveDown>().speed = moveSpeed;
	}

	float getGridHeightWorld() {
		int gridHeight = transformInfo.gridSize.y;
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
		float spawnFreq = gridHeightWorld/moveSpeed;
		return spawnFreq;
	}
}
