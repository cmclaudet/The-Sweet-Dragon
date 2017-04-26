using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelData : MonoBehaviour {
	public Grid gridSize;
	public float spawnMoveSpeed;

	[System.Serializable]
	public struct Grid {
		public int x;
		public int y;

		public Grid(int width, int height) {
			x = width;
			y = height;
		}
	}

	void OnValidate() {
		if (gridSize.x % 2 == 0) {
			Debug.LogWarning("Grid width must be odd.");
			gridSize.x += 1;
		}
		if (gridSize.x < 0) {
			Debug.LogWarning("Grid width must be positive.");
			gridSize.x *= -1;
		}
		if (gridSize.y < 0) {
			Debug.LogWarning("Grid length must be positive.");
			gridSize.y *= -1;
		}
		if (spawnMoveSpeed < 0) {
			Debug.LogWarning("Move speed must be greater than 0.");
			spawnMoveSpeed *= -1;
		}
	}

}
