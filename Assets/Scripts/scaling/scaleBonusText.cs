using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to score bonus text to make text scale up then scale down before disappearing
//An instance of custom class blowUpGeneral is created to find how object scales over time with a given velocity and acceleration
public class scaleBonusText : MonoBehaviour {
	public float timePresent;		//time text is present
	public float vel;
	public float acc;
	private float maxScale = 1;			//max value text will scale up to
	private float minScale = 0;		//text always starts with size of 0
	private bool needScaling;
	private blowUpGeneral scaleUp;

	// Use this for initialization
	void Start () {
		needScaling = true;
		GetComponent<Transform> ().localScale = new Vector3(minScale, minScale);
		scaleUp = new blowUpGeneral (vel, acc, minScale);
		StartCoroutine(scaleDown());		
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (needScaling) {
			scaleUp.updateVelocity ();
			scaleUp.updateScale ();
			GetComponent<Transform> ().localScale = new Vector3 (scaleUp.scale, scaleUp.scale);
		}

		if (scaleUp.scale >= maxScale) {
			needScaling = false;
		}

		//inactivate gamobject once it has become small enough
		if (scaleUp.scale < minScale) {
			Destroy(gameObject);
		}
	}

	IEnumerator scaleDown() {
		yield return new WaitForSeconds(timePresent);
		scaleUp.velocity = -vel;
		scaleUp.acceleration = acc;
		needScaling = true;
	}

}
