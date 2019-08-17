using UnityEngine;
using System.Collections;

public class ItemContainerScript : MonoBehaviour {

	public int maxCapacity = 10;
	int currentCapacity = 0;

	ArrayList items;

	void Start () {
		items = new ArrayList ();
	}

	public bool Add (Item item) {
		if (item != null) {
			print ("ItemContainer: Added item " + item.name);
			if (currentCapacity + item.size < maxCapacity) {
				items.Add (item);
				currentCapacity += item.size;

				//gui_playerUI.AddButton (item);
				return true;
			}
		}
		return false;
	}

	public bool Remove (Item item) {
		if (items.Contains (item)) {
			print ("ItemContainer: Removed item " + item.name);
			items.Remove (item);
			currentCapacity -= item.size;

			//gui_playerUI.RemoveButton (item);
			return true;
		} else {
			return false;
		}
	}

	//public ArrayList GetContent () {
	//	return items;
	//}
}
