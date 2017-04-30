using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollisionRock : MonoBehaviour {
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
		Destroy(otherSweet.gameObject);
	}
}
