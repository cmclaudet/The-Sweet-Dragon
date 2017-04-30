using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*If caterpillar is squashed by rock time is stopped and game over UI appears
  Splatter sprite is also displayed.
 */
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
