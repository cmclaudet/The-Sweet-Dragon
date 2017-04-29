using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates grid with set set in the inspector
public class levelData : MonoBehaviour {
	public Grid gridSize;
	public float gridMoveSpeed;
	public GameObject grid;

	public float[] xGridCoords{get; private set;}
	public float[] yGridCoords{get; private set;}

	[HideInInspector]public List<Vector2> gridPoints;

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
		GetComponent<moveGridDown>().moveSpeed = gridMoveSpeed;
		setGridPoints();
	}

	void setXGridCoords() {
		float gridWidthScreen = Screen.width/gridSize.x;
		xGridCoords = new float[gridSize.x];
		for (int i = 0; i < gridSize.x; i++) {
			float gridScreenXPos = (i+0.5f)*gridWidthScreen;
			xGridCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(gridScreenXPos, 0)).x;
		}
	}

	void setYGridCoords() {
		float gridHeightScreen = Screen.height/gridSize.y;
		yGridCoords = new float[gridSize.y];
		for (int i = 0; i < gridSize.y; i++) {
			float gridScreenYPos = (0.5f+i)*gridHeightScreen;
			yGridCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(0, gridScreenYPos)).y;
		}
	}

	void setGridPoints() {
		for (int i = 0; i < xGridCoords.Length; i++) {
			for (int j = 0; j < yGridCoords.Length; j++) {
				Vector2 newGridPoint = new Vector2(xGridCoords[i], yGridCoords[j]);
				gridPoints.Add(newGridPoint);
				createNewGridObject(newGridPoint);
			}
		}
	}

	void createNewGridObject(Vector2 objectPos) {
		GameObject gridPoint = new GameObject();
		gridPoint.transform.position = new Vector3(objectPos.x, objectPos.y, 0);
		//gridPoint.transform.SetParent(grid.transform);
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
		if (gridMoveSpeed < 0) {
			Debug.LogWarning("Move speed must be greater than 0.");
			gridMoveSpeed *= -1;
		}
	}

}
