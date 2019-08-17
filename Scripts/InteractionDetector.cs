using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent (typeof (CircleCollider2D))]
public class InteractionDetector : MonoBehaviour {
	//public PlayerController player;
	InteractionManager intManager;

	CircleCollider2D detectorCollider;
	protected Dictionary<Interactable, bool> availableInteractables;
	public LayerMask obstacleMask;
	protected Interactable chosenInteractable; // hide


	void Start () {
		intManager = this.GetComponentInParent<InteractionManager> ();
		if (intManager == null) {
			throw new MissingComponentException ("No Interaction Manager set for detector");
		}

		detectorCollider = GetComponent<CircleCollider2D> ();
		detectorCollider.isTrigger = true;
		availableInteractables = new Dictionary<Interactable, bool>(); // NOT NECESSARY?!
	}
	
	void OnTriggerEnter2D (Collider2D cldr) {
		//print("OnTriggerEnter2D");
		AddInteractable (cldr.GetComponent<Interactable> ());
	}

	void OnTriggerStay2D (Collider2D item) {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, // origin
		                                      item.transform.position - transform.position, // direction
		                                      ((Vector2)(item.transform.position - transform.position)).magnitude,
		                                      obstacleMask); // dist
		if (hit) {
			availableInteractables[item.GetComponent<Interactable>()] = false;
			Debug.DrawRay (transform.position, // origin
			               new Vector3(hit.point.x, hit.point.y, 0) - transform.position);
		} else {
			availableInteractables[item.GetComponent<Interactable>()] = true;
			Debug.DrawRay (transform.position, // origin
			               item.transform.position - transform.position);
		}
	}
	
	/*public */void OnTriggerExit2D (Collider2D cldr) { // public for item to Disappear () to fix exit case
		print("OnTriggerExit2D");
		RemoveInteractable (cldr.GetComponent<Interactable> ());
	}

	public void AddInteractable (Interactable interactable) {
		if (!availableInteractables.ContainsKey (interactable)) { // in case of being dropped/spawned from inventory (belongs - already known)
			interactable.AddDetector (this);
			availableInteractables.Add (interactable, false);
			//print ("Available " + item.name);
		}
	}

	public void RemoveInteractable (Interactable interactable) {
		intManager.InteractableOutOfRange (interactable);
		interactable.RemoveDetector(this);
		availableInteractables.Remove (interactable);
		//print ("No more available " + interactable.name);
	}

	/*public void OverrideAvailability (Interactable itemToAlwaysBeAvailable) {
		availableInteractables [itemToAlwaysBeAvailable] = true;
	}*/

	public void SubmitPickup (Item i) {
		intManager.SubmitPickup (i);
	}

	public void SubmitDropdown (Item i) {
		intManager.SubmitDropdown(i);
	}
	
	public void Highlight (Interactable interactable) { // can only be called by item if it is in range
		chosenInteractable = interactable;
	}
	
	public void NotHighlight (Interactable interactable) { // can only be called by item if it is in range
		chosenInteractable = null;
	}

	public bool IsAvailable (Interactable i) {
		if (i != null) {
			if (availableInteractables.ContainsKey (i)) {
				if (availableInteractables [i] == true) {
					return true;
				}
			}
		}
		return false;
	}

	public Interactable GetHighlighted () {
		return chosenInteractable;
	}

	public InteractionManager GetInteractionManager () {
		return intManager;
	}
}

