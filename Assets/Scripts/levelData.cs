using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelData : MonoBehaviour {
	public Grid gridSize;
	public float spawnMoveSpeed;

	public float[] xGridCoords{get; private set;}
	public float[] yGridCoords{get; private set;}

	[System.Serializable]
	public struct Grid {
		public int x;
		public int y;

		public Grid(int width, int height) {
			x = width;
			y = height;
		}
	}

	void Awake() {
		setXGridCoords();
		setYGridCoords();
	}

	void setXGridCoords() {
		float gridWidthScreen = Screen.width/gridSize.x;
		xGridCoords = new float[gridSize.x + 1];
		for (int i = 0; i <= gridSize.x; i++) {
			float gridScreenXPos = i*gridWidthScreen;
			xGridCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(gridScreenXPos, 0)).x;
		}
	}

	void setYGridCoords() {
		float gridHeightScreen = Screen.height/(gridSize.y + 1);
		yGridCoords = new float[gridSize.y + 1];
		for (int i = 0; i <= gridSize.y; i++) {
			float gridScreenYPos = Screen.height - i*gridHeightScreen;
			yGridCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(0, gridScreenYPos)).y;
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
