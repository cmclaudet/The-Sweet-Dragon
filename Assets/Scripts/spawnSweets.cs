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
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.laneNumber;
		moveSpeed = transformInfo.spawnMoveSpeed;
		spawnFrequency = transformInfo.spawnFrequency;
		StartCoroutine(spawnAndDestroySweets());
	}

	IEnumerator spawnAndDestroySweets() {
		for (;;) {
			spawnNewSweet();
			yield return new WaitForSeconds(spawnFrequency);
		}
	}

	void spawnNewSweet() {
		Transform newSweet = Instantiate(sweetPrefab);
		sweetData thisSweetData = new sweetData(sweetParams.sweetTypeNames.Length, sweetParams.numberOfStages);
		newSweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		newSweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetParams.allImages[thisSweetData.type].stageImages;
		setSweetInitialLocation(newSweet);
	}

	void setSweetInitialLocation(Transform sweet) {
		int sweetLane = Random.Range(0,laneNumber);
		float lanePixelWidth = (Screen.width)/(float)laneNumber;
		float sweetXPixelPos = ((float)sweetLane + 0.5f)*lanePixelWidth;
		float sweetYPixelPos = Screen.height;
		Vector3 initialPosition = Camera.main.ScreenToWorldPoint(new Vector3(sweetXPixelPos, sweetYPixelPos));
		sweet.position = initialPosition;
	}
}
