using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Moves all grid point objects down at a constant speed
public class moveGridDown : MonoBehaviour {
	private float moveSpeed;
	void Start() {
		moveSpeed = GridConstants.speed;
	}

	void FixedUpdate() {
		foreach (Transform gridRow in transform) {
			gridRow.transform.position = new Vector3(gridRow.transform.position.x, gridRow.transform.position.y - moveSpeed*Time.fixedDeltaTime);
		}
	}
}
