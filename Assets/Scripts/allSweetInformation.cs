using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allSweetInformation : MonoBehaviour {
	public string[] sweetTypeNames;
	public int numberOfStages;
	public imagesOfOneType[] allImages;

	void OnValidate() {
		if (allImages.Length != sweetTypeNames.Length) {
			Debug.LogWarning("Image list length must be equal to sweet type names length.");
         	System.Array.Resize(ref allImages, sweetTypeNames.Length);
		}

		foreach (imagesOfOneType oneTypeImages in allImages) {
			if (oneTypeImages.stageImages.Length != numberOfStages) {
				Debug.LogWarning("Image stage list length must be equal to the number of stages.");
         		System.Array.Resize(ref oneTypeImages.stageImages, numberOfStages);
			}
		}
	}
	
}

[System.Serializable]
public class imagesOfOneType{
	public Sprite[] stageImages;
}


