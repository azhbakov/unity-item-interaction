using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof (SliderJoint2D))]
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (InteractionManager))]
public class PlayerController : MonoBehaviour {

	// MOVEMENT
	Rigidbody2D bodyRigidbody;
	BoxCollider2D cldr;
	public float maxRunSpeed;
	bool facingRight = true;
	[HideInInspector]
	public float input; // for CameraFollow script

	// JUMP
	bool grounded = false;
	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundMask;
	public float jumpVelocity = 5;

	// PASSENGER
	public bool canBoard = false; // hide private
	public bool boarded = false; // hide private
	public CarrierScript carrier; // hide private

	// PICK UP ITEMS
	//public InteractionDetector itemDetector; // hide? get from child
	//public ItemContainerScript itemContainer;

	// MANAGERS
	InteractionManager intManager;
	//public GUIManager guiManager;

	// HANDS
	Dictionary <string, GUIItem> guiItems;
	GUIItem leftHand;
	GUIItem rightHand;
	bool leftIsActive = true;

	// Use this for initialization
	void Start () {
		bodyRigidbody = GetComponent<Rigidbody2D> ();
		cldr = GetComponent<BoxCollider2D> ();
		/*itemDetector = this.GetComponentInChildren<InteractionDetector> ();
		if (itemDetector == null) {
			throw new MissingComponentException("No interaction detector set");
		}
		itemContainer = GetComponent<ItemContainerScript> ();*/
		//playerUI = GetComponent <GUI> ();
		intManager = transform.GetComponent<InteractionManager> ();
		if (intManager == null) {
			throw new UnityException ("No GUI Manager found for player");
		}

		guiItems = intManager.GetGuiItems ();
		leftHand = guiItems["left hand"];
		rightHand = guiItems["right hand"];
	}
	
	// Update is called once per frame
	void Update () {
		// GET IN TRANSPORT
		if (canBoard && Input.GetKeyDown ("f"))
			Board (carrier);
		else if (boarded && Input.GetKeyDown ("f"))
			Disembark ();

		// INTERACT
		if (Input.GetMouseButtonDown(0)) {
			//singleItem = itemDetector.pickupItem();
			//itemContainer.Add (itemDetector.pickupItem());
			//itemDetector.Interact ();
			LMBInteract ();
		}
		if (Input.GetMouseButtonDown(1)) {
			//singleItem = itemDetector.pickupItem();
			//itemContainer.Add (itemDetector.pickupItem());
			//itemDetector.Interact ();
			RMBInteract ();
		}

		// SWITCH HAND
		if (Input.GetKeyDown ("q")) {
			SetLeftHand ();
		}
		// SWITCH HAND
		if (Input.GetKeyDown ("e")) {
			SetRightHand ();
		}

		// SHOW INVENTORY
		//if (Input.GetKeyDown ("i")) {
		//	playerUI.ContainerSwitchDisplay ();
		//}
	}

	void FixedUpdate () { // not using Time.deltaTime!
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);

		if (boarded == false) {
			if (grounded && Input.GetKey ("space"))
				Jump ();

			float input = Input.GetAxis ("Horizontal");
			Move (input);

			if (input < 0 && facingRight) {
				Flip ();
			} else if (input > 0 && !facingRight)
				Flip ();
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere (groundCheck.position, groundCheckRadius);
	}

	//MOVEMENT
	void Move (float input){
		bodyRigidbody.velocity = new Vector2 (input * maxRunSpeed, bodyRigidbody.velocity.y);
	}

	void Flip () {
		facingRight = !facingRight;
		//Vector3 theScale = transform.localScale;
		//theScale.x *= -1;
		//transform.localScale = theScale;
	}

	// JUMP
	void Jump () {
		bodyRigidbody.velocity = new Vector2 (bodyRigidbody.velocity.x, jumpVelocity);
	}

	// BEING PASSENGER CONSIDER MULTIPLE CARRIERS AVAILABLE
	public void AllowBoard (CarrierScript carrier) { // called by carrier
		canBoard = true;
		this.carrier = carrier;
	}

	public void DisallowBoard (CarrierScript carrier) { // called by carrier
		canBoard = false;
		//this.carrier = null; // ?
	}

	void Board (CarrierScript carrier) {
		if (carrier.acceptPassenger (bodyRigidbody)) {
			boarded = true;
			DisallowBoard (carrier);
			cldr.enabled = false;
			bodyRigidbody.mass = 0;
			bodyRigidbody.gravityScale = 0;
		}
	}

	void Disembark () {
		carrier.releasePassenger (bodyRigidbody);
		boarded = false;
		AllowBoard (carrier); // possible glitch
		cldr.enabled = true;
		bodyRigidbody.mass = 1;
		bodyRigidbody.gravityScale = 1;
	}

	// HANDS
	void SwitchHand () {
		leftIsActive = !leftIsActive;
	}

	void SetLeftHand () {
		leftIsActive = true;
	}

	void SetRightHand () {
		leftIsActive = false;
	}

	bool CurrentHandIsFree () {
		if ((leftIsActive && leftHand.GetOriginal() == null) || // left hand is free
		    (!leftIsActive && rightHand.GetOriginal() == null)) { // right hand is free
			return true;
		} else {
			return false;
		}
	}

	public void LMBInteract () { // FIX ARGUMENTS FOR ACTIONS, public to be called from GUIButton to simulate lmb
		if (CurrentHandIsFree ()) {
			if (leftIsActive)
				intManager.SubmitAction (leftHand);
			else
				intManager.SubmitAction (rightHand);
		} else {
			print ("Hand is busy");
		}
	}

	public void RMBInteract () {
	/*	if (leftIsActive)
			itemDetector.AlternativeInteraction (leftHand);
		else
			itemDetector.AlternativeInteraction (rightHand);*/
	}

	public bool PickupItem (Item item) { // INVOKED BY ITEM can be called only when isAvailable //picked up? think about item types
		if (CurrentHandIsFree ()) {
			intManager.PickupConfirmed (item, this);

			if (leftIsActive)
				leftHand.PretendToBeItem (item);
			else
				rightHand.PretendToBeItem (item);
			return true;
		}
		return false;
	}

	public bool DropdownItem (Item item) { // ADD CHECKING BACKPACK CELL etc...
		if (leftHand.GetOriginal() == item) {
			intManager.DropdownConfirmed (item);

			leftHand.StopPretending ();
			return true;
		} else if (rightHand.GetOriginal() == item) {
			intManager.DropdownConfirmed (item);

			rightHand.StopPretending ();
			return true;
		} else {
			throw new UnityException ("TRYING TO DROP ITEM NOT IN HAND");
		}
	}
	
}
