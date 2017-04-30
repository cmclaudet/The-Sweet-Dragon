using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//contains data of this sweet
public class sweetAttributes : MonoBehaviour {
	[HideInInspector]public sweetData thisSweetData;
	[HideInInspector]public Sprite[] thisSweetTypeStageImages;	//contains image data for all stages of this sweet type
	// Use this for initialization
	void Start () {
		setNewImage();
	}

//resets image based on new stage
	void setNewImage() {
		GetComponent<SpriteRenderer>().sprite = thisSweetTypeStageImages[thisSweetData.stage - 1];
	}

//when sweet changes stage image is automatically updated
	public void setNewStage(int newStage) {
		thisSweetData.stage = newStage;
		setNewImage();
	}

}
