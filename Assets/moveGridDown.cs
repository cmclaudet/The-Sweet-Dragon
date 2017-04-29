using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGridDown : MonoBehaviour {
	public GameObject grid;
	[HideInInspector]public float moveSpeed;
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Transform gridPointObject in grid.transform) {
			gridPointObject.position = new Vector2(gridPointObject.position.x, gridPointObject.position.y * moveSpeed * Time.fixedDeltaTime);
		}
	}
}
