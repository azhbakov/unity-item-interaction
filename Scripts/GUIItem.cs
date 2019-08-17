using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Image))]
[RequireComponent (typeof (Button))]
public class GUIItem : Item {

	//Canvas canvas;
	// DETECTOR IS CONST, PLAYER DETECTOR

	Image backround;
	Button button;
	Image itemImage;

	Item originalItem;

	override protected void Start () {
		//base.Start ();

		backround = GetComponent <Image> ();
		button = GetComponent <Button> ();
		GameObject child = new GameObject ();
		child.transform.SetParent (this.transform, false);
		child.name = "Item Sprite";
		child.layer = 2;

		itemImage = child.AddComponent <Image> ();
		itemImage.enabled = false;

		RectTransform r = child.GetComponent<RectTransform> ();
		r.anchorMin = Vector2.zero;
		r.anchorMax = new Vector2 (1, 1);
		r.pivot = new Vector2 (1, 1);
		r.anchoredPosition = Vector2.zero;
		r.sizeDelta = Vector2.zero;


		EventTrigger eventTrigger = gameObject.AddComponent <EventTrigger> ();

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback = new EventTrigger.TriggerEvent();
		entry.callback.AddListener( delegate { OnMouseEnter(); } );
		eventTrigger.triggers = new List<EventTrigger.Entry> ();
		eventTrigger.triggers.Add(entry);

		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerExit;
		entry.callback = new EventTrigger.TriggerEvent();
		entry.callback.AddListener (delegate { OnMouseExit(); });
		eventTrigger.triggers.Add(entry);
	}

	void SetSprite (Sprite sprite) {
		if (sprite != null) {
			itemImage.enabled = true;
		} else {
			itemImage.enabled = false;
		}
		itemImage.sprite = sprite;
	}

	public void PretendToBeItem (Item i) {
		originalItem = i;
		SetSprite (i.GetPreviewSprite());
		
		button.onClick.AddListener (delegate {
			i.GetOwner().LMBInteract (); // or act in other way?..
		});
	}

	public void StopPretending () {
		originalItem = null;
		SetSprite (null);
	}

	public Item GetOriginal () {
		return originalItem;
	}

	//
	//
	// OVERRIDE
	//
	//
	override public void OnMouseEnter () {
		if (originalItem != null) {
			originalItem.OnMouseEnter ();
		}
		//print ("works");
	}
	
	override public void OnMouseExit () {
		if (originalItem != null) {
			originalItem.OnMouseExit ();
		}
		//print ("perfect");
	}
	
	override public void AddDetector (InteractionDetector detector) {
		if (originalItem != null) {
			originalItem.AddDetector (detector);
		}
	}
	
	override public void RemoveDetector (InteractionDetector detector) {
		if (originalItem != null) {
			originalItem.RemoveDetector (detector);
		}
	}
	
	override public List<InteractableAction> GetActions () { // ADD ITEM ARG!!!
		if (originalItem != null) {
			return originalItem.GetActions();
		} else {
			return null;
		}
	}
	
	override public List<string> GetDescriptions () {
		if (originalItem != null) {
			return originalItem.GetDescriptions();
		} else {
			return null;
		}
	}
	
	override public void PerformAction (int index, Item itemArg) {
		if (originalItem != null) {
			originalItem.PerformAction(index, itemArg);
		}
	}
	
	override public void PickupConfirmed (PlayerController player) {
		if (originalItem != null) {
			originalItem.PickupConfirmed (player);
			//StopPretending ();
		}
	}

	override public void DropdownConfirmed () {
		if (originalItem != null) {
			originalItem.DropdownConfirmed ();
		}
	}

	override public void SetStateGround () {
		if (originalItem != null) {
			originalItem.SetStateGround ();
		}
	}
	
	override public void SetStatePocket (PlayerController player) {
		if (originalItem != null) {
			originalItem.SetStatePocket (player);
		}
	}

	override public Sprite GetPreviewSprite () {
		if (originalItem != null) {
			return originalItem.GetPreviewSprite ();
		} else {
			return null;
		}
	}
	
	override public PlayerController GetOwner () {
		if (originalItem != null) {
			return originalItem.GetOwner ();
		} else {
			return null;
		}
	}

}
