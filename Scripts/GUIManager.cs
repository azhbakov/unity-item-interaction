using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent (typeof (InteractionManager))]
public class GUIManager : MonoBehaviour {
	
	InteractionManager intManager;
	Dictionary <string, GUIItem> guiItems;

	// CANVAS
	public GameObject canvasPrefab;
	Canvas canvas;

	// HANDS
	public GameObject guiHandsPanelPrefab;
	Image handsPanel;
	///*public */GUIItem leftHand;
	///*public */GUIItem rightHand;

	// ACCESSORIES
	public GameObject guiAccessoriesPanelPrefab;
	Image accessoriesPanel;
	public Image beltImage;
	public Image backpackImage;

	// CONTAINERS
	//public GameObject guiContainerPanelPrefab;
	//GameObject containerPanel; // to enable/disable
	//GameObject containerScrollRect;
	//public Button containerButtonPrefab;
	//public Text containerButtonTextPrefab;
	//public ItemContainerScript itemContainer;

	// LIST OF ACTIONS
	public GameObject actionListPrefab;
	GUIList actionList;
	//bool actionListIsOpened = false;

	void Start () {
		intManager = GetComponent<InteractionManager> ();
		if (intManager == null) {
			throw new UnityException ("No interaction manager found for UI");
		}

		guiItems = new Dictionary <string, GUIItem> ();

		// Creating canvas
		if (canvasPrefab == null) {
			throw new UnityException ("No Canvas prefab found");
		} else {
			canvas = Instantiate (canvasPrefab).GetComponent<Canvas> ();
		}

		// Adding hands panel
		if (guiHandsPanelPrefab == null) {
			throw new UnityException ("No Hands panel prefab found");
		} else {
			handsPanel = Instantiate (guiHandsPanelPrefab).GetComponent<Image> ();
			handsPanel.transform.SetParent (canvas.transform, false);
			GUIItem leftHand = GameObject.FindWithTag ("Left Hand Image").GetComponent<GUIItem>();
			guiItems.Add ("left hand", leftHand);
			//leftHand.enabled = false;
			GUIItem rightHand = GameObject.FindWithTag ("Right Hand Image").GetComponent<GUIItem>();
			guiItems.Add ("right hand", rightHand);
			//rightHand.enabled = false;
			if (leftHand == null || rightHand == null) {
				throw new UnityException ("No left/right hand GUIInteractable component detected in Hands Panel prefab");
			}
		}

		// Adding accessories panel
		if (guiAccessoriesPanelPrefab == null) {
			throw new UnityException ("No accessories panel prefab found");
		} else {
			accessoriesPanel = Instantiate (guiAccessoriesPanelPrefab).GetComponent<Image> ();
			accessoriesPanel.transform.SetParent (canvas.transform, false);
			beltImage = GameObject.FindWithTag ("Belt Image").GetComponent<Image>();
			beltImage.enabled = false;
			backpackImage = GameObject.FindWithTag ("Backpack Image").GetComponent<Image>();
			backpackImage.enabled = false;
			if (beltImage == null || backpackImage == null) {
				throw new UnityException ("No belt/backpack image detected in Accessories Panel prefab");
			}
		}

		// Adding action list panel
		if (actionListPrefab == null) {
			throw new UnityException ("No Action List prefab found");
		}
		actionList = Instantiate (actionListPrefab).GetComponent<GUIList> ();
		actionList.transform.SetParent (canvas.transform, false);
		if (actionList == null) {
			throw new UnityException ("GUIList component not found in action list prefab");
		}
		actionList.SetManager (this);
		//actionList.Hide (true);
	}

	//public Canvas GetCanvas () {
	//	return canvas;
	//}

	public void ButtonPressed (int index) {
		intManager.CallAction (index);
	}

	public void ShowDescriptionsList (List<string> descriptionList) {
		//actionList.RemoveButtonNames ();
		actionList.LoadButtonNames (descriptionList);
		actionList.Hide (false);
	}

	public void CloseDescriptionsList () {
		actionList.RemoveButtonNames ();
		actionList.Hide (true);
	}

	public Dictionary <string, GUIItem> GetGuiItems () {
		return guiItems;
	}

}
