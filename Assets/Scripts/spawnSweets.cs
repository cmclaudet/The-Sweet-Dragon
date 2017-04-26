﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSweets : MonoBehaviour {
	public allSweetInformation sweetParams;
	public levelData transformInfo;
	public Transform sweetPrefab;
	private int laneNumber;
	private float moveSpeed;
	private float spawnFrequency;
	private float initialYPos;
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.laneNumber;
		moveSpeed = transformInfo.spawnMoveSpeed;
		spawnFrequency = transformInfo.spawnFrequency;
		initialYPos = getInitialYPos();
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

	float getInitialYPos() {
		float screenHeightWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;
		float initialYPos = screenHeightWorld + sweetPrefab.GetComponent<CircleCollider2D>().radius;
		return initialYPos;
	}
}
