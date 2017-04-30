using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Finds nearest grid point object and sets it as its parent so that the object snaps to the grid
  If grid point object already has a child collision logic is executed depending on the nature of this object
 */
public class snapToGrid : MonoBehaviour {
	[HideInInspector]public List<GameObject> gridPointObjects;
	[HideInInspector]public int laneNumber;
	[HideInInspector]public Vector2 gridSizeWorld;	//size of one grid cell in world space
	public void snapObject() {
		for (int i = 0; i < gridPointObjects.Count; i++) {
			//if object is between the left and right side of a grid point object and the top and bottom bounds of the grid point object it snaps towards it
			//calculated using the position of the grid point and size of a grid cell in world space
			if (transform.position.y > gridPointObjects[i].transform.position.y - gridSizeWorld.y/2 && transform.position.y < gridPointObjects[i].transform.position.y + gridSizeWorld.y/2 ) {
				if (transform.position.x > gridPointObjects[i].transform.position.x - gridSizeWorld.x/2 && transform.position.x < gridPointObjects[i].transform.position.x + gridSizeWorld.x/2) {
					transform.SetParent(gridPointObjects[i].transform);
					transform.localPosition = Vector3.zero;
					checkOtherObjects();
					break;
				}
			}
		}
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
}
