using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDown : MonoBehaviour {
	public float speed{get;set;}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += Vector3.down*speed*Time.fixedDeltaTime;
	}
}
