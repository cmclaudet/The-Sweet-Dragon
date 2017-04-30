using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//contains data for one sweet
[System.Serializable]
public class sweetData {
	public int type{get; private set;}	//which kind of sweet this is. Number corresponds to order of sweet image data set in inspector.
	private int _stage;	//what stage the sweet is at
	public int stage{
		get{
			return _stage;
		} set {
			if (value > _numberOfStages) {		//ensures sweet does not have an impossible stage
				_stage = _numberOfStages;
			} else {
				_stage = value;
			}
		}
	}
	private int _numberOfStages;
	public int sweetValue() {
		if (this.atMaxStage()) {		//if sweet is at max stage it is worth double. Encourages players to get their sweets to max stage.
			return _stage*2;
		} else {
			return _stage;
		}	
	}

	public bool atMaxStage() {
		if (_stage == _numberOfStages) {
			return true;
		} else {
			return false;
		}
	}

//randomizes which type the sweet is at sweet initialization
	public sweetData(int numberOfSweetTypes, int totalStages) {
		System.Random typeNumber = new System.Random();
		type = typeNumber.Next(0, numberOfSweetTypes);
		_stage = 1;
		_numberOfStages = totalStages;
	}


}

