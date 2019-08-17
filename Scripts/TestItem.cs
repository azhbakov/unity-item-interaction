using UnityEngine;
using System.Collections;

public class TestItem : Item {

	/*void PickUp (Item i) {
		if (detector.player.PickupItem (this) == true)
			Disappear ();
	}*/

	void MakeRed (Item i) {
		print ("TestItem is now red");
	}
	
	void MakeGreen (Item i) {
		print ("TestItem is now green");
	}

	void MakeBlack (Item i) {
		print ("TestItem is now black");
	}

	void MakeBlue (Item i) {
		print ("TestItem is now blue");
	}

	override protected void Start () {
		base.Start ();

		//delegateDic.Add (PickUp, "Pick Up");
		actionList.AddAction (MakeRed, "Make red");

		actionList.AddAction (MakeGreen, "Make green");

		actionList.AddAction (MakeBlack, "Make black");

		actionList.AddAction (MakeBlue, "Make blue");
	}

	/*override public void ListOfInteraction (Item item) {
		detector.player.playerUI.ShowActionList (transform.position, delegateDic);
	}*/

}
