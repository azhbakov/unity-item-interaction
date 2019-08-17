using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (InteractionManager))]
[RequireComponent (typeof (GUIManager))]
public class Player : MonoBehaviour {

	PlayerController playerController;
	InteractionManager intManager;
	GUIManager guiManager;
	public InteractionDetector detector;

	void Start () {
		playerController = GetComponent<PlayerController> ();
		intManager = GetComponent <InteractionManager> ();
		guiManager = GetComponent <GUIManager> ();
		if (detector == null) {
			throw new UnityException ("No Interaction Detector set to player");
		}
	}

	public PlayerController GetPlayerController () {
		return playerController;
	}

	public InteractionManager GetInteractionManager () {
		return intManager;
	}

	public GUIManager GetGUIManager () {
		return guiManager;
	}

	public InteractionDetector GetDetector () {
		return detector;
	}
}
