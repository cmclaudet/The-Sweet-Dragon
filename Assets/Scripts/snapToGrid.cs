using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour {
	[HideInInspector]public float[] gridXPos;
	[HideInInspector]public float[] gridYPos;
	[HideInInspector]public float spawnFrequency;
	private Vector2 gridDimensions;

	void Start() {
		gridDimensions = new Vector2(gridXPos[1] - gridXPos[0], gridYPos[0] - gridYPos[1]);
	}
	
	public void snapPosition() {
		snapXPos();
		snapYPos();
	}

	void snapXPos() {
		for (int i = 1; i < gridXPos.Length; i++) {
			if (transform.position.x < gridXPos[i]) {
				float newXPos = gridXPos[i] - gridDimensions.x/2;
				transform.position = new Vector3 (newXPos, transform.position.y, transform.position.z);
				break;
			}
		}
	}

	void snapYPos() {

	}
}
