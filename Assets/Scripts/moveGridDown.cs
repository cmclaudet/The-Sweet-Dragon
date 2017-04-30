using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Moves all grid point objects down at a constant speed
public class moveGridDown : MonoBehaviour {
	[HideInInspector]public float moveSpeed;
	[HideInInspector]public List<GameObject> gridPointObjects;
	// Update is called once per frame
	void Start() {
		//initial grid points generated on awake of levelData are found by tag
		GameObject[] gridObjects = GameObject.FindGameObjectsWithTag("gridPoint");
		foreach (GameObject gridObject in gridObjects) {
			gridPointObjects.Add(gridObject);
		}
	}

	void FixedUpdate() {
		foreach (GameObject gridPointObject in gridPointObjects) {
			gridPointObject.transform.position = new Vector3(gridPointObject.transform.position.x, gridPointObject.transform.position.y - moveSpeed*Time.fixedDeltaTime);
		}
	}
}
