using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// CHECKS ALL AVAILABILITY

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GUIManager))]
public class InteractionManager : MonoBehaviour {
	[HideInInspector]
	public Player player;

	InteractionDetector detector; // to be set by user via Player script
	GUIManager guiManager;
	PlayerController playerController;

	//Interactable lastInteractable;
	//Item lastItemArg;

	ListController listController;

	// Use this for initialization
	void Start () {
		player = transform.GetComponent <Player> ();
		if (player == null) {
			throw new UnityException ("No Player found in Interaction Manager");
		}

		guiManager = player.GetGUIManager ();
		if (guiManager == null) {
			throw new UnityException ("No GUIManager found in Interaction Manager");
		}

		detector = player.GetDetector ();
		if (detector == null) {
			throw new UnityException ("No Interaction Detector set in Interaction Manager");
		}

		playerController = player.GetPlayerController ();
		if (playerController == null) {
			throw new UnityException ("No Player Controller set in Interaction Manager");
		}

		listController = new ListController (guiManager);
	}
	
	public void SubmitAction (Item itemInHand) { // called by player with LMB
		Interactable i = detector.GetHighlighted ();
		if (detector.IsAvailable (i)) {
			listController.ShowList (i, itemInHand, i.GetDescriptions ());
		}
	}
	
	public void CallAction (int index/*, Item itemArg*/) {
		listController.GetListHost ().PerformAction (index, listController.GetItemArg());
		listController.CloseList (); // BUTTON HAS BEEN PRESSED, HIDE NOW
	}

	public void SubmitPickup (Item itemToPickUp) {
		if (detector.IsAvailable (itemToPickUp)) { // unlikely to cause misbehaviour as only can be called from Action List when item is available
			playerController.PickupItem (itemToPickUp);
		}
	}
	
	public void PickupConfirmed (Item pickedupItem, PlayerController player) { // called by player after pickup
		pickedupItem.PickupConfirmed (player);
	}

	public void SubmitDropdown (Item itemToDrop) {
		//if (detector.IsAvailable (itemToPickUp)) { // unlikely to cause misbehaviour as only can be called from Action List when item is available
		playerController.DropdownItem (itemToDrop);
		//}
	}
	
	public void DropdownConfirmed (Item droppedItem) { // called by player after pickup
		droppedItem.DropdownConfirmed ();
	}

	public Dictionary <string, GUIItem> GetGuiItems () {
		return guiManager.GetGuiItems ();
	}

	public void InteractableOutOfRange (Interactable interactable) { // notification from detector
		listController.InteractableOutOfRange (interactable);
	}
	
	struct ListController {
		GUIManager guiManager;
		bool isOpened ;
		Interactable listHost;
		Item shownWithItemArg;

		public ListController (GUIManager m) {
			guiManager = m;
			isOpened = false;
			listHost = null;
			shownWithItemArg = null;
		}

		public void ShowList (Interactable i, Item itemInHand, List<string> descriptions) {
			if (isOpened) {
				CloseList ();
			}
			guiManager.ShowDescriptionsList (descriptions);
			listHost = i;
			shownWithItemArg = itemInHand;
			isOpened = true;
		}

		public void CloseList () {
			guiManager.CloseDescriptionsList ();
			listHost = null;
			shownWithItemArg = null;
			isOpened = false;
		}

		public void InteractableOutOfRange (Interactable interactable) { // notification from detector
			if (isOpened && interactable.gameObject == listHost.gameObject) { // hide opened action list when leaving
				CloseList ();
			}
		}

		public Interactable GetListHost () {
			return listHost;
		}

		public Item GetItemArg  () {
			return shownWithItemArg;
		}
	}
}
