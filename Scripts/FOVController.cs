using UnityEngine;
using System.Collections;

public class FOVController : MonoBehaviour {

	public Camera cam;
	public GameObject FOVLight;
	private Vector3 targetScreenPos;
	private Vector3 lightScreenPos; // eyes
	private Quaternion targetQ;

	//public float stiffAreaSizeX = 100;
	//public float stiffAreaSizeY = 100;
	
	void FixedUpdate () {
		var pos = cam.WorldToScreenPoint(FOVLight.transform.position);
		var dir = Input.mousePosition - pos;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		FOVLight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
	}
	// Update is called once per frame
	//void LateUpdate () {
	//	if (FOVLight != null && cam != null) {
	//		lightScreenPos = cam.WorldToScreenPoint(FOVLight.transform.position);
	//		lightScreenPos.z = 0;
	//		targetScreenPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
			//currentQ = FOVLight.transform.rotation;
	//		targetQ.SetFromToRotation(FOVLight.transform.right, targetScreenPos-lightScreenPos);
	//		FOVLight.transform.Rotate(targetQ.eulerAngles);
	//	}
	//}

	/*Vector3 getTargetScreenPos () { // to avoid glitching
		Vector3 targetScreenPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		float deltaX = targetScreenPos.x - lightScreenPos.x;
		float deltaY = targetScreenPos.y - lightScreenPos.y;
		//float facingRight = Mathf.Sign (targetScreenPos - lightScreenPos);

		if (Mathf.Abs(deltaX) < stiffAreaSizeX)
			targetScreenPos.x = lightScreenPos.x;
		if (Mathf.Abs(deltaY) < stiffAreaSizeY)
			targetScreenPos.y = lightScreenPos.y;

		return targetScreenPos;
	}

	void OnDrawGizmos() {
		Gizmos.DrawCube (lightScreenPos, new Vector3(stiffAreaSizeX, stiffAreaSizeY, 1));
	}*/
}
