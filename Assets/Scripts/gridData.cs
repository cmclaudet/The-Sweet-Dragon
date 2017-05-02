using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Contains data for the level grid
  Grid dimensions are defined in the inspector
  Generates grid point objects at centre of each cell based on these dimensions and screen size
 */
public class gridData : MonoBehaviour {
	public Grid gridSize;	//number of grid cells for both x and y dimensions
    public Transform gridRow;
	public float[] yGridRowCoords{get; private set;}	//coordinates in world space of y positions of grid cell centres
	public float gridHeightWorld{get; private set;}	//height of one grid cell in world space


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
		setYGridRowCoords();
		makeGridRows();
	}

	void setYGridRowCoords() {
		float gridHeightScreen = Screen.height/gridSize.y;
		float initialYPos = (gridSize.y + 0.5f)*gridHeightScreen;
		yGridRowCoords = new float[gridSize.y + 2];		//size is greater so that when old grid point objects are removed they are off of the screen
		for (int i = 0; i <= gridSize.y + 1; i++) {
			float gridScreenYPos = (-1.5f+i)*gridHeightScreen;
			yGridRowCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(0, gridScreenYPos)).y;
		}
	}

	void makeGridRows() {
		for (int i = 0; i < yGridRowCoords.Length; i++) {
			Transform newGridRow = Instantiate(gridRow);
            newGridRow.position = new Vector3(0, yGridRowCoords[i]);
			newGridRow.GetComponent<makeGridPoints>().laneNumber = gridSize.x;
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
	}

}
