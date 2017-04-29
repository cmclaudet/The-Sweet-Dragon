using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweetAttributes : MonoBehaviour {
	[HideInInspector]public sweetData thisSweetData;
	[HideInInspector]public Sprite[] thisSweetTypeStageImages;
	// Use this for initialization
	void Start () {
		setNewImage();
	}

	void setNewImage() {
		GetComponent<SpriteRenderer>().sprite = thisSweetTypeStageImages[thisSweetData.stage - 1];
	}

	public void setNewStage(int newStage) {
		thisSweetData.stage = newStage;
		setNewImage();
	}

}
