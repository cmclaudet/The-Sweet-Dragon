using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour {
	[HideInInspector]public float rockSpawnChance;
	[HideInInspector]public Transform rockPrefab;
	[HideInInspector]public Transform sweetPrefab;
	[HideInInspector]public allSweetInformation sweetImageInfo;
	// Use this for initialization
	
	public void spawnRowObject() {
		Transform newObject;
		if (Random.Range(0,1f) < rockSpawnChance) {
			newObject = Instantiate(rockPrefab);
		} else {
			newObject = Instantiate(sweetPrefab);
			setupSweetData(newObject);
		}
		setObjectLane(newObject);
	}

	void setupSweetData(Transform sweet) {
		sweetData thisSweetData = new sweetData(sweetImageInfo.sweetTypeNames.Length, sweetImageInfo.numberOfStages);
		sweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		sweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetImageInfo.allImages[thisSweetData.type].stageImages;
	}


	void setObjectLane(Transform laneObject) {
		int objectLane = Random.Range(0, GridConstants.x);
		foreach (Transform gridPoint in transform) {
			if (gridPoint.GetSiblingIndex() == objectLane) {
				laneObject.SetParent(gridPoint);
				laneObject.localPosition = Vector3.zero;
				break;
			}
		}
	}
}
