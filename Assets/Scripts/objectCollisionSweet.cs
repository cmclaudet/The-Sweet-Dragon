using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollisionSweet : MonoBehaviour {

	public void collide(Transform other) {
		if (other.gameObject.CompareTag("rock")) {
			OnRockCollision(other);
		} else {
			OnSweetCollision(other);
		}
	}
	void OnRockCollision(Transform otherRock) {
		Destroy(gameObject);
	}

	void OnSweetCollision(Transform otherSweet) {
		if (otherSweet.GetComponent<sweetAttributes>().thisSweetData.type == GetComponent<sweetAttributes>().thisSweetData.type) {
			mergeSweets(otherSweet, transform);
		} else {
			Destroy(otherSweet.gameObject);
		}
	}

	void mergeSweets(Transform sweet1, Transform sweet2) {
		int newStage = sweet1.GetComponent<sweetAttributes>().thisSweetData.stage + sweet2.GetComponent<sweetAttributes>().thisSweetData.stage;
		sweet1.GetComponent<sweetAttributes>().setNewStage(newStage);
		Destroy(sweet2.gameObject);
	}
}
