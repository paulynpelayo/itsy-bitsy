using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public Transform target;

	private Vector3 origPos;
	// Use this for initialization
	void Start () {
		origPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camPos;

		camPos.z = origPos.z;
		camPos.y = origPos.y;
		camPos.x = target.localPosition.x + origPos.x;

		transform.localPosition = camPos;
	}
}
