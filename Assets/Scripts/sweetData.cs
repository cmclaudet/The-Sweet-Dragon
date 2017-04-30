﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sweetData {
	public int type{get; private set;}
	private int _stage;
	public int stage{
		get{
			return _stage;
		} set {
			if (value > _numberOfStages) {
				_stage = _numberOfStages;
			} else {
				_stage = value;
			}
		}
	}
	private int _numberOfStages;
	public int sweetValue() {
		return _stage;
	}

	public sweetData(int numberOfSweetTypes, int totalStages) {
		System.Random typeNumber = new System.Random();
		type = typeNumber.Next(0, numberOfSweetTypes);
		_stage = 1;
		_numberOfStages = totalStages;
	}


}

