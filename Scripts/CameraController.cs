using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	private Vector3 targetPos;

	private Vector3 cameraPos;
	public Vector3 offset = new Vector3(2, 1, 0);

	void Start () {
		targetPos = target.transform.position;
		cameraPos = transform.position;
		transform.position = targetPos;
		//Debug.Log (targetPos);
	}

	// Update is called once per frame
	public void LateUpdate () {
		targetPos = target.transform.position;
		TrackTarget ();
	}

	private void TrackTarget () {
		if (Mathf.Abs (targetPos.x - cameraPos.x) > offset.x) {
			cameraPos.x = targetPos.x - Mathf.Sign (targetPos.x - cameraPos.x)* offset.x;
		}
		if (Mathf.Abs (targetPos.y - cameraPos.y) > offset.y) {
			cameraPos.y = targetPos.y - Mathf.Sign (targetPos.y - cameraPos.y)* offset.y;
		}

		transform.position = new Vector3 (cameraPos.x, cameraPos.y, cameraPos.z);
	}
}
