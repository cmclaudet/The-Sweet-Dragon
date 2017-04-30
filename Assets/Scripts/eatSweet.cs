using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eatSweet : MonoBehaviour {
	private int score;
	public Text scoreText;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("sweet")) {
			int sweetValue = other.GetComponent<sweetAttributes>().thisSweetData.sweetValue();
			Destroy(other.gameObject);
			score += sweetValue;
			rewriteScore();
		}
	}

	void rewriteScore() {
		scoreText.text = "Score: " + score.ToString();
	}
}
