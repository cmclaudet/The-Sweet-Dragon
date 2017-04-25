using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelData : MonoBehaviour {
	public float spawnMoveSpeed;
	public int laneNumber;
	public float spawnFrequency;

	void OnValidate() {
		if (laneNumber % 2 == 0) {
			Debug.LogWarning("Lane number must be odd.");
			laneNumber -= 1;
		}
		if (laneNumber < 0) {
			Debug.LogWarning("Lane number must be positive.");
			laneNumber *= -1;
		}
		if (spawnFrequency < 0) {
			Debug.LogWarning("Frequency of spawn must be greater than 0.");
			spawnFrequency *= -1;
		}
		if (spawnMoveSpeed < 0) {
			Debug.LogWarning("Move speed must be greater than 0.");
			spawnMoveSpeed *= -1;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
