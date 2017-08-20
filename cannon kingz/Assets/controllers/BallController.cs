using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	Rigidbody rb;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private bool canHit = true, charging = false;
	//private float hitPower = 0f;
	public float minPower, maxPower, chargeSpeed = 4f, floor;
	private float autoResetTimer;
	public float autoResetTime = 5f;
	public CameraController cam;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		startRotation = transform.rotation;
		rb = GetComponent<Rigidbody> ();

		InitializeBall ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < floor)
			ResetBall ();

		if (canHit) {
			
		} else {
			if(rb.velocity.magnitude < 0.5f)
				autoResetTimer -= Time.deltaTime;
			if (autoResetTimer <= 0f)
				ResetBall ();
		}
	}

	void ChargeHit() {
		//hitPower += Time.deltaTime * chargeSpeed;
	}

	public bool CanHit()
	{
		return canHit;
	}

	public void DoHit(float hitPower, Transform lookAt) {
		rb.isKinematic = false;
		transform.LookAt (lookAt);
		hitPower = Mathf.Clamp (hitPower, minPower, maxPower);
		Debug.DrawRay (transform.position, transform.forward);
		rb.AddForce (transform.forward * (hitPower) * rb.mass, ForceMode.Impulse);
		canHit = false;
		charging = false;
		cam.FollowTarget ();
		cam.LookAtTarget ();
	}
	public void InitializeBall() {
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = startPosition;
		transform.rotation = startRotation;

		//hitPower = minPower;
		autoResetTimer = autoResetTime;
		canHit = true;
		charging = false;
	}

	public void ResetBall() {
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = startPosition;
		transform.rotation = startRotation;

		//hitPower = minPower;
		autoResetTimer = autoResetTime;
		canHit = true;
		charging = false;
		cam.ResetCamera ();
	}
}
