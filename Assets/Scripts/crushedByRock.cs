using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crushedByRock : MonoBehaviour {
	public GameObject gameOverUI;
	public Transform splatter;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("rock")) {
			Destroy(gameObject);
			Time.timeScale = 0;
			gameOverUI.SetActive(true);
			Instantiate(splatter);
		}
	}
}
