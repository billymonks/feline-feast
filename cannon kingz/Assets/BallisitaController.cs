using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallisitaController : MonoBehaviour {
	public static int score = 0;
	public static double combo = 0;

	public GameObject restingPosition;
	public Transform pulledPosition;
	public BallController ball;
	public Camera cam;

	private CameraController camController;
	private float ballSize = 1f;
	private bool dragInProgress = false;
	private float pulledDistance = 0;
	private Vector3 initialPullPos;
	public float pullScale = 5f, dragSpeed = 6f;
	public int maxBalls = 3;
	private int balls;

	public Text scoreUi, ballsUi;

	public RadialMenuController ballSelector;
	// Use this for initialization
	void Start () {
		initialPullPos = pulledPosition.position;
		balls = maxBalls;
		score = 0;

		camController = cam.GetComponent<CameraController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		camController.target = ball.gameObject.transform;
		scoreUi.text = score.ToString ();
		ballsUi.text = balls.ToString ();
		if (balls <= 0 && (ball.CanHit() || !ball.gameObject.activeInHierarchy)) {
			ball.gameObject.SetActive (false);
			if (Input.GetMouseButton (0)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
			return;
		}
		ballSize = ball.transform.localScale.y;
		restingPosition.transform.localScale = Vector3.one * ballSize;
		pulledDistance = (restingPosition.transform.position - pulledPosition.position).magnitude;

		if (!ball.CanHit ()) {
			ballSelector.gameObject.SetActive (false);
			pulledPosition.position = initialPullPos;
			return;
		} else if (!dragInProgress) {
			ballSelector.gameObject.SetActive (true);
		}

		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Ray r = cam.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (r, out hit)) {
				if (hit.collider.gameObject == restingPosition) {
					ballSelector.gameObject.SetActive (false);
					dragInProgress = true;
					pulledPosition.position = Vector3.Lerp(
						pulledPosition.position,
						restingPosition.transform.position + hit.normal * (ballSize / 2f),
						Time.deltaTime * dragSpeed);
				} else if (dragInProgress) {
					pulledPosition.position = Vector3.Lerp(
						pulledPosition.position,
						hit.point + hit.normal * (ballSize / 2f),
						Time.deltaTime * dragSpeed);
				}
			}
		} else if (dragInProgress) {
			//fire ball
			combo = 0;
			dragInProgress = false;

			ball.DoHit (pulledDistance * pullScale, restingPosition.transform);
			balls--;
		}


		ball.transform.position = pulledPosition.position;
	}

	public void changeBall(GameObject ball)
	{
		this.ball.gameObject.SetActive (false);
		this.ball = ball.GetComponent<BallController> ();
		this.ball.gameObject.SetActive (true);
	}
}
