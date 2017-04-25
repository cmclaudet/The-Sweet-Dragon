using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sweetData : MonoBehaviour {
	private sweetInfo sweet = new sweetInfo();
	public imageList[] allImages;

	void OnValidate() {
		if (allImages.Length != sweet.numberOfTypes()) {
			Debug.LogWarning("Don't change the 'ints' field's array size!");
         	System.Array.Resize(ref allImages, sweet.numberOfTypes());
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class sweetInfo {
	public enum types{RED, BLUE, YELLOW, GREEN};
	public int numberOfTypes() {
		return System.Enum.GetValues(typeof(types)).Length;
	}
	public int type{get; private set;}
	private int _stage;
	public int stage{
		get{
			return _stage;
		} set {
			if (value > 2) {
				Debug.Log("ERROR: Stage is too high!");
			} else {
				_stage = value;
			}
		}
	}
	public int sweetValue() {
		return _stage*2;
	}

	public sweetInfo() {
		System.Random typeNumber = new System.Random();
		type = typeNumber.Next(0, numberOfTypes());
		_stage = 0;
	}


}

[System.Serializable]
public class imageList{
	public Image[] images;
}