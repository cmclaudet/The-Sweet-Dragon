using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Increments player score and displays bonus text when caterpillar eats a sweet
  On eating a sweet at a maximum stage "max sweet!" bonus message is displayed
  Score is rewritten each time a sweet is eaten.
 */
public class eatSweet : MonoBehaviour {
	private int score;
	public Text scoreText;
	public Transform bonusText;
	public Transform maxSweetText;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("sweet")) {
			writeBonusText(other.GetComponent<sweetAttributes>().thisSweetData);
			rewriteScore();
			Destroy(other.gameObject);	//destroy sweet on eating
		}
	}

	void rewriteScore() {
		scoreText.text = "Score: " + score.ToString();
	}

	void writeBonusText(sweetData sweetInfo) {
		Transform newBonusText = Instantiate(bonusText);
		if (sweetInfo.atMaxStage()) {
			Instantiate(maxSweetText);
		}
		int sweetValue = sweetInfo.sweetValue();
		newBonusText.GetComponent<TextMesh>().text = "+" + sweetValue.ToString();
		score += sweetValue;
	}
}
