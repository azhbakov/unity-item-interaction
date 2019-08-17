using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (DistanceJoint2D))]
public class CarrierScript : MonoBehaviour {

	Collider2D cldr;
	Rigidbody2D rgbd;
	DistanceJoint2D seat;
	// add passenger num check
	// forbide passenger movement

	// Use this for initialization
	void Start () {
		cldr = GetComponent<Collider2D> ();
		cldr.isTrigger = true;

		rgbd = GetComponent<Rigidbody2D> ();
		rgbd.gravityScale = 0;

		seat = GetComponent<DistanceJoint2D> ();
		seat.enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//print("OnTriggerEnter2D");
		PlayerController player = other.GetComponent<PlayerController> ();
		if (player != null) {
			player.AllowBoard (this);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		//print("OnTriggerExit2D");
		PlayerController player = other.GetComponent<PlayerController> ();
		if (player != null) {
			player.DisallowBoard (this);
		}
	}

	public bool acceptPassenger (Rigidbody2D passenger) { // called by passenger
		seat.connectedBody = passenger;
		seat.enabled = true;
		return true;
	}

	public void releasePassenger (Rigidbody2D passenger) { // called by passenger
		seat.connectedBody = null;
		seat.enabled = false;
	}
}
