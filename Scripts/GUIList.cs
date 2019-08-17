using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// ATTACHED TO PANEL
public class GUIList : MonoBehaviour {
	GUIManager manager;
	
	GameObject listPanel;
	GameObject scrollRect;
	public Button buttonPrefab;
	public Text buttonTextPrefab;

	// Use this for initialization
	void Start () {
		listPanel = this.gameObject;
		scrollRect = listPanel.GetComponentInChildren<LayoutGroup>().gameObject;
		Hide (true);
	}

	public void LoadButtonNames (List<string> descriptionList) {
		for (int i = 0; i < descriptionList.Count; i++) {
			AddButton (descriptionList, i);
		}
	}

	public void RemoveButtonNames () {
		foreach (Transform child in scrollRect.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	public void SetPosition (Vector3 pos) {
		listPanel.transform.position = pos;
	}

	public void SetManager (GUIManager manager) { // somehow move to constructor?
		this.manager = manager;
	}

	public void Hide (bool b) {
		listPanel.SetActive (!b);
	}

	void AddButton (List<string> descriptionList, int buttonNum) {
		// Button
		Button button = Instantiate (buttonPrefab);
		button.name = "ActionList Button";
		button.onClick.AddListener (delegate {
			manager.ButtonPressed (buttonNum);
			//Hide (true); // FIX? no need?
		});
		button.transform.SetParent(scrollRect.transform, false);
		
		// Text
		Text text = Instantiate (buttonTextPrefab);
		text.text = descriptionList[buttonNum];
		text.transform.SetParent (button.transform, false);
	}


}
