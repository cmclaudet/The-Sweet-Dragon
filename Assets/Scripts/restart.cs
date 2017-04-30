using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	public void restartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("main");
	}
}
