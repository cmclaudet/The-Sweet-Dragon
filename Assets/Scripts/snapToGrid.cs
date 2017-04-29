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
		
		float yPosWithoutTime = 0;
		for (int i = 1; i < gridYPos.Length; i++) {
			if (transform.position.y > gridYPos[i]) {
				yPosWithoutTime = gridYPos[i] + gridDimensions.y/2;
				break;
			}
		}
		//Debug.Log(yPosWithoutTime);

		float timeSinceGridPointPass = Time.timeSinceLevelLoad%spawnFrequency;
		float snapPointDistanceFromGrid = gridDimensions.y*timeSinceGridPointPass/spawnFrequency;

		if (snapPointDistanceFromGrid < gridDimensions.y/2) {
			float snapPosY = yPosWithoutTime - snapPointDistanceFromGrid;
			if (transform.position.y < snapPosY + gridDimensions.y/2) {
				transform.position = new Vector3(transform.position.x, snapPosY, transform.position.z);
			} else {
				transform.position = new Vector3(transform.position.x, snapPosY + gridDimensions.y, transform.position.z);
			}
		} else {
			float snapPosY = yPosWithoutTime + gridDimensions.y - snapPointDistanceFromGrid;
			if (transform.position.y > snapPosY - gridDimensions.y/2) {
				transform.position = new Vector3(transform.position.x, snapPosY, transform.position.z);
			} else {
				transform.position = new Vector3(transform.position.x, snapPosY - gridDimensions.y, transform.position.z);
			}
		}
	}
}
