using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Contains data needed to spawn sweets and rocks at random lane including sweet types and images and grid data.
  Calculates spawn period from desired move speed so that one sweet/rock is spawned on each grid row.
  Spawns one row of grid objects at the spawn period and creates a sweet/rock (with probability defined in rock spawn chance) childed to one of the grid objects in this row
  Calculates initial y position of new grid objects based on grid dimensions
  Passes grid data to sweets and rocks so they are able to snap to other grid objects
  Removes old grid objects that have passed off the screen
 */
public class spawnSweets : MonoBehaviour {
	public allSweetInformation sweetParams;	//contains sweet data
	public GameObject transformInfo;	//contains grid data
	public Transform sweetPrefab;
	public Transform rockPrefab;
	private float rockSpawnChance;
	private int laneNumber;
	private float spawnPeriod;
	private float gridHeightWorld;	//height of one grid cell in world space
	private float gridWidthWorld;	//width of one grid cell in world space
	private float initialYPos;		//initial y position of all spawns
	// Use this for initialization
	void Start () {
		laneNumber = transformInfo.GetComponent<levelData>().gridSize.x;
		gridHeightWorld = transformInfo.GetComponent<levelData>().gridHeightWorld;
		gridWidthWorld = transformInfo.GetComponent<levelData>().gridWidthWorld;
		rockSpawnChance = transformInfo.GetComponent<levelData>().rockSpawnChance;
		initialYPos = getInitialYPos();
		spawnPeriod = getSpawnPeriod();
		StartCoroutine(spawnCountDown());
	}

	IEnumerator spawnAndDestroyGridPoints() {
		for (;;) {
			spawnNewGridPoints();
			removeOldGridPoints();
			yield return new WaitForSeconds(spawnPeriod);
		}
	}

	//add one frame of wait time so that move grid down script can find all grid point objects
	IEnumerator spawnCountDown() {
		yield return null;
		StartCoroutine(spawnAndDestroyGridPoints());
	}

//spawns a number of grid point objects on a row equal to the lane number which is equal to x grid dimension
//adds new grid object to list of grid point objects in moveGridDown so new objects are also moved down
	void spawnNewGridPoints() {
		float[] xGridPoints = transformInfo.GetComponent<levelData>().xGridCoords;
		GameObject[] newGridPointObjects = new GameObject[xGridPoints.Length];
		for (int i = 0; i < xGridPoints.Length; i++) {
			GameObject newGridObject = new GameObject();
			newGridObject.transform.position = new Vector3(xGridPoints[i], initialYPos, 0);
//			transformInfo.GetComponent<moveGridDown>().gridPointObjects.Add(newGridObject);
			newGridPointObjects[i] = newGridObject;
		}
		spawnNewObject(newGridPointObjects);
	}

//creates a rock or sweet at one of the new grid point objects
	void spawnNewObject(GameObject[] newGridPointObjects) {
		Transform newObject;
		if (Random.Range(0, 1f) < rockSpawnChance) {
			newObject = Instantiate(rockPrefab);
		} else {
			newObject = Instantiate(sweetPrefab);
			setupSweetData(newObject);
		}
		setupGridSnappingData(newObject);
		setObjectToGridPoint(newGridPointObjects, newObject);
	}

//generates sweet data and passes it to new sweet
	void setupSweetData(Transform sweet) {
		sweetData thisSweetData = new sweetData(sweetParams.sweetTypeNames.Length, sweetParams.numberOfStages);
		sweet.GetComponent<sweetAttributes>().thisSweetData = thisSweetData;
		sweet.GetComponent<sweetAttributes>().thisSweetTypeStageImages = sweetParams.allImages[thisSweetData.type].stageImages;
	}

//passes grid data to new object so snapping is possible
	void setupGridSnappingData(Transform laneObject) {
//		laneObject.GetComponent<snapToGrid>().gridPointObjects = transformInfo.GetComponent<moveGridDown>().gridPointObjects;
//		laneObject.GetComponent<snapToGrid>().laneNumber = laneNumber;
//		laneObject.GetComponent<snapToGrid>().gridSizeWorld = new Vector2(gridWidthWorld, gridHeightWorld);
	}

//set new object as child of one of the grid point objects so it moves down with the grid
	void setObjectToGridPoint(GameObject[] gridPointObjects, Transform laneObject) {
		int lane = Random.Range(0, laneNumber);
		laneObject.transform.SetParent(gridPointObjects[lane].transform);
		laneObject.transform.localPosition = Vector3.zero;
	}

//removes first row in list of grid point objects as these will be off the screen
	void removeOldGridPoints() {
		for (int i = 0; i < laneNumber; i++) {
//			Destroy(transformInfo.GetComponent<moveGridDown>().gridPointObjects[i]);
//			transformInfo.GetComponent<moveGridDown>().gridPointObjects.RemoveAt(i);
		}
	}

	float getInitialYPos() {
		float screenHeightWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;
		float initialYPos = screenHeightWorld + gridHeightWorld/2;
		return initialYPos;
	}

	float getSpawnPeriod() {
		float spawnPer = gridHeightWorld/transformInfo.GetComponent<levelData>().gridMoveSpeed;
		return spawnPer;
	}
}
