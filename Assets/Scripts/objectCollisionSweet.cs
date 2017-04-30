using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Checks which object the sweet has collided with and executes appropriate logic
 
public class objectCollisionSweet : MonoBehaviour {

	public void collide(Transform other) {
		if (other.gameObject.CompareTag("rock")) {
			OnRockCollision(other);
		} else {
			OnSweetCollision(other);
		}
	}
	//if the other object is a rock the sweet is destroyed by the rock
	void OnRockCollision(Transform otherRock) {
		Destroy(gameObject);
	}

//if the other object is a sweet must check if they are the same type. If so the sweets merge. If not the sweet is destroyed.
	void OnSweetCollision(Transform otherSweet) {
		if (otherSweet.GetComponent<sweetAttributes>().thisSweetData.type == GetComponent<sweetAttributes>().thisSweetData.type) {
			mergeSweets(otherSweet, transform);
		} else {
			Destroy(otherSweet.gameObject);
		}
	}

//when sweets merge new stage is set for one of them and the other is destroyed
	void mergeSweets(Transform sweet1, Transform sweet2) {
		int newStage = sweet1.GetComponent<sweetAttributes>().thisSweetData.stage + sweet2.GetComponent<sweetAttributes>().thisSweetData.stage;
		sweet1.GetComponent<sweetAttributes>().setNewStage(newStage);
		Destroy(sweet2.gameObject);
	}
}
