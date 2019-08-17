using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Interactable : MonoBehaviour {

	protected InteractionDetector detector; // hide, ItemDetector signs in here, multiple for multiplayer?

	public string _name; // make private add getter
	protected ActionList actionList;

	virtual protected void Start () {
		detector = null;
		actionList = new ActionList (false);
	}
	
	virtual public void OnMouseEnter () { // public for GUIItem...
		//print ("OnMouseEnter " + this._name);
		if (detector != null)
			detector.Highlight (this);
	}
	
	virtual public void OnMouseExit () {
		//print ("OnMouseExit " + this._name);
		// foreach
		if (detector != null)
			detector.NotHighlight (this);
	}
	
	virtual public void AddDetector (InteractionDetector detector) {
		this.detector = detector;
	}
	
	virtual public void RemoveDetector (InteractionDetector detector) {
		this.detector = null;
	}

	virtual public List<InteractableAction> GetActions () { // ADD ITEM ARG!!!
		return actionList.GetActions();
	}

	virtual public List<string> GetDescriptions () {
		return actionList.GetDescriptions();
	}

	virtual public void PerformAction (int index, Item itemArg) {
		actionList.GetAction(index)(itemArg);
	}

	public delegate void InteractableAction (Item itemArg);

	public struct ActionList {
		List<InteractableAction> actionListSeq;
		List<string> descriptionListSeq;

		public ActionList (bool fakeArg) {
			actionListSeq = new List<InteractableAction> ();
			descriptionListSeq = new List<string> ();
		}

		public void AddAction (InteractableAction action, string description) {
			actionListSeq.Add (action);
			descriptionListSeq.Add (description);
		}

		public void RemoveAction (InteractableAction action) {
			int index = actionListSeq.IndexOf (action);
			actionListSeq.Remove (action);
			descriptionListSeq.Remove (descriptionListSeq[index]);
		}

		public InteractableAction GetAction (int index) { // ADD ITEM ARG!!!
			return actionListSeq[index];
		}

		public List<InteractableAction> GetActions () { // ADD ITEM ARG!!!
			return actionListSeq;
		}
		
		public List<string> GetDescriptions () {
			return descriptionListSeq;
		}
	}
	//virtual public InteractionDetector GetDetector () { // for those who pretend
	//	return detector;
	//}

	//public Dictionary<UnityAction<Item>, string> GetActions () {
	//	return delegateDic;
	//}
	
	//public abstract void MainInteraction ();

	//public abstract void ListOfInteraction (Item item);

	//public abstract void InteractUsingItem (Item item);
}
