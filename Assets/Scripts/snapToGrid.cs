using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Finds nearest grid point object and sets it as its parent so that the object snaps to the grid
  If grid point object already has a child collision logic is executed depending on the nature of this object
 */
public class snapToGrid : MonoBehaviour {
	private GameObject grid;
	private Vector2 gridSizeWorld;	//size of one grid cell in world space

	void Start() {
		gridSizeWorld = GridConstants.gridSizeWorld;
		grid = ((transform.parent).parent).parent.gameObject;
	}
	public void snapObject() {
		bool snapped = false;
		foreach (Transform gridRow in grid.transform) {
			//if object is between the left and right side of a grid point object and the top and bottom bounds of the grid point object it snaps towards it
			//calculated using the position of the grid point and size of a grid cell in world space
			if (transform.position.y > gridRow.transform.position.y - gridSizeWorld.y/2 && transform.position.y < gridRow.transform.position.y + gridSizeWorld.y/2 ) {
				foreach (Transform gridPoint in gridRow) {
					if (transform.position.x > gridPoint.transform.position.x - gridSizeWorld.x/2 && transform.position.x < gridPoint.transform.position.x + gridSizeWorld.x/2) {
						setObjectToGridPoint(gridPoint);
						checkOtherObjects();
						snapped = true;
						break;
					}
				}
				break;
			}
		}

		//if object does not fall within bounds of any grid cell find nearest grid point and attach object anyway
		if (!snapped) {
			Transform nearestGridRow = findNearestGridRow();
			Transform nearestGridPoint = findNearestGridPoint(nearestGridRow);
			setObjectToGridPoint(nearestGridPoint);
			checkOtherObjects();
			snapped = true;
		}
	}

	void setObjectToGridPoint(Transform gridPointObject) {
		transform.SetParent(gridPointObject.transform);
		transform.localPosition = Vector3.zero;
	}

//if there are other objects with dragOnHold script childed to the grid point object collision logic is executed
	void checkOtherObjects() {
		dragOnHold[] objectsOnGridPoint = transform.parent.gameObject.GetComponentsInChildren<dragOnHold>();
		if (objectsOnGridPoint.Length > 1) {
			if (gameObject.CompareTag("sweet")) {
				GetComponent<objectCollisionSweet>().collide(objectsOnGridPoint[0].transform);	//if object is a sweet this logic is necessary
			} else {
				GetComponent<objectCollisionRock>().collide(objectsOnGridPoint[0].transform);	//if object is a rock this logic is necessary
			}
		}
	}

	Transform findNearestGridRow() {
		float shortestYDifference = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;
		Transform nearestGridRow = grid.transform.GetChild(0);
		foreach (Transform gridRow in grid.transform) {
			float yDiff = Mathf.Abs(gridRow.position.y - transform.position.y);
			if (yDiff < shortestYDifference) {
				shortestYDifference = yDiff;
				nearestGridRow = gridRow;
			}
		}
		return nearestGridRow;
	}

	Transform findNearestGridPoint(Transform gridRow) {
		float shortestXDifference = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
		Transform nearestGridPoint = gridRow.GetChild(0);
		foreach (Transform gridPoint in gridRow) {
			float xDiff = Mathf.Abs(gridPoint.position.x - transform.position.x);
			if (xDiff < shortestXDifference) {
				shortestXDifference = xDiff;
				nearestGridPoint = gridPoint;
			}
		}
		return nearestGridPoint;
	}
}
