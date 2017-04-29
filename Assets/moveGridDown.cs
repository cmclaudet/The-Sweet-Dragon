using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGridDown : MonoBehaviour {
	[HideInInspector]public float moveSpeed;
	[HideInInspector]public List<GameObject> gridPointObjects;
	// Update is called once per frame
	void Start() {
		GameObject[] gridObjects = GameObject.FindGameObjectsWithTag("gridPoint");
		foreach (GameObject gridObject in gridObjects) {
			gridPointObjects.Add(gridObject);
		}
	}

	void FixedUpdate () {
		foreach (GameObject gridPointObject in gridPointObjects) {
			gridPointObject.transform.position = new Vector3(gridPointObject.transform.position.x, gridPointObject.transform.position.y - moveSpeed*Time.fixedDeltaTime);
		}
	}
}
