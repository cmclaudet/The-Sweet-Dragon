using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Contains data for the level grid
  Grid dimensions are defined in the inspector
  Generates grid point objects at centre of each cell based on these dimensions and screen size
 */
public class gridData : MonoBehaviour {
	public Grid gridSize;	//number of grid cells for both x and y dimensions
    public float moveSpeed;
    public Transform gridRow;
    public allSweetInformation sweetImageInfo;
	public float[] yGridRowCoords{get; private set;}	//coordinates in world space of y positions of grid cell centres


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
        setupGridConstants();
		setYGridRowCoords();
		makeGridRows();
        GetComponent<spawnNewGridRows>().gridRowPrefab = gridRow;
        GetComponent<spawnNewGridRows>().sweetImageInfo = sweetImageInfo;
	}

    void setupGridConstants() {
        GridConstants.x = gridSize.x;
        GridConstants.y = gridSize.y;
        GridConstants.gridSizeWorld = setGridSizeWorld();
        GridConstants.speed = moveSpeed;
    }

	void setYGridRowCoords() {
		float gridHeightScreen = Screen.height/gridSize.y;
		yGridRowCoords = new float[gridSize.y + 2];		//size is greater so that when old grid point objects are removed they are off of the screen
		for (int i = 0; i <= gridSize.y + 1; i++) {
			float gridScreenYPos = (-1.5f+i)*gridHeightScreen;
			yGridRowCoords[i] = Camera.main.ScreenToWorldPoint(new Vector3(0, gridScreenYPos)).y;
		}
	}

    Vector2 setGridSizeWorld() {
        float gridHeightScreen = Screen.height/gridSize.y;
        float gridYPos0 = Camera.main.ScreenToWorldPoint(new Vector3(0, (gridSize.y + 0.5f)*gridHeightScreen)).y;
        float gridYPos1 = Camera.main.ScreenToWorldPoint(new Vector3(0, (gridSize.y - 0.5f)*gridHeightScreen)).y;
        float gridHeightWorld = gridYPos0 - gridYPos1;

        float gridWidthScreen = Screen.width/gridSize.x;
        float gridXPos0 = Camera.main.ScreenToWorldPoint(new Vector3((gridSize.x + 0.5f)*gridWidthScreen, 0)).x;
        float gridXPos1 = Camera.main.ScreenToWorldPoint(new Vector3((gridSize.x - 0.5f)*gridWidthScreen, 0)).x;
        float gridWidthWorld = gridXPos0 - gridXPos1;
        return new Vector2(gridWidthWorld,gridHeightWorld);
    }

	void makeGridRows() {
		for (int i = 0; i < yGridRowCoords.Length; i++) {
			Transform newGridRow = Instantiate(gridRow);
            newGridRow.GetComponent<makeGridPoints>().sweetImageInfo = sweetImageInfo;
            newGridRow.SetParent(transform);
            newGridRow.localPosition = new Vector3(0, yGridRowCoords[i]);
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
