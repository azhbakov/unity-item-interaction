using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Item : WorldInteractable {

	public int size = 1;
	public Sprite previewSprite;
	PlayerController owner;
	State currentState;

	enum State {
		Ground, Pocket
	}
	//bool isPickedUp = false;


	// Use this for initialization
	override protected void Start () {
		if (previewSprite == null) {
			throw new UnityException ("No preview sprite set");
		}

		base.Start ();

		currentState = State.Ground;

		actionList.AddAction (SubmitPickUp, "Pick Up");
	}

	void SubmitPickUp (Item itemArgIsCheckedByPlayer = null) { // called by user via action list and PerformAction
		detector.SubmitPickup (this);
	}

	virtual public void PickupConfirmed (PlayerController player) {
		SetStatePocket (player);
		//print ("PickupConfirmed, changing state and disappearing");
	}

	void SubmitDropdown (Item itemArgIsCheckedByPlayer = null) { // called by user via action list and PerformAction
		detector.SubmitDropdown (this);
	}
	
	virtual public void DropdownConfirmed () {
		SetStateGround ();
	}

	virtual public void SetStateGround () {
		cldr.enabled = true;
		renderer.enabled = true;

		owner = null;
		transform.SetParent (null);

		Rigidbody2D rgbd = GetComponent<Rigidbody2D> (); // check if it exists
		rgbd.gravityScale = 1;

		if (currentState == State.Pocket)
			actionList.RemoveAction (SubmitDropdown); 
		actionList.AddAction (SubmitPickUp, "Pick Up");

		currentState = State.Ground;
	}

	virtual public void SetStatePocket (PlayerController player) {
		cldr.enabled = false;
		renderer.enabled = false;

		owner = player;
		transform.SetParent (player.transform);
		transform.localPosition = new Vector3 (0, 0, transform.position.z);

		Rigidbody2D rgbd = GetComponent<Rigidbody2D> (); // check if it exists
		rgbd.gravityScale = 0;

		if (currentState == State.Ground)
			actionList.RemoveAction (SubmitPickUp);
		actionList.AddAction (SubmitDropdown, "Drop down");

		currentState = State.Pocket;
	}

	virtual public Sprite GetPreviewSprite () {
		return previewSprite;
	}

	virtual public PlayerController GetOwner () {
		return owner;
	}

	void Update () {
		if (currentState == State.Pocket) {
			transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.position.z);
		}
	}
}
