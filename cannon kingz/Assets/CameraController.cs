using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private Vector3 initialPos;
	private Quaternion initialRot;
	private bool followTarget = false, lookAtTarget = false;
	public Transform target;
	public float speed;
	public float minDistance = 10f;
	public float lookSpeed = 3f;
	// Use this for initialization
	void Start () {
		initialPos = transform.position;
		initialRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = (target.position - transform.position).magnitude;
		if (followTarget && distance > minDistance) {
			transform.Translate (transform.forward * speed * Time.deltaTime);
		}

		if (lookAtTarget) {
			Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
		}
	}

	public void FollowTarget() {
		followTarget = true;
	}

	public void LookAtTarget() {
		lookAtTarget = true;
	}

	public void ResetCamera() {
		transform.position = initialPos;
		transform.rotation = initialRot;
		followTarget = false;
		lookAtTarget = false;
	}
}
