using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Contains image data for all sweets. 
  Number of sweet types can be changed by changing total sweet type names. Number of sweet stages can be changed by changing number of stages.
  Images for sweet types and stages must be dragged into inspector
 */
public class allSweetInformation : MonoBehaviour {
	public string[] sweetTypeNames;
	public int numberOfStages;
	public imagesOfOneType[] allImages;

//Cannot make length of sweet image types different to sweet type names
	void OnValidate() {
		if (allImages.Length != sweetTypeNames.Length) {
			Debug.LogWarning("Image list length must be equal to sweet type names length.");
         	System.Array.Resize(ref allImages, sweetTypeNames.Length);
		}

//Cannot make total sweet stage images for one sweet type different to number of stages
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


