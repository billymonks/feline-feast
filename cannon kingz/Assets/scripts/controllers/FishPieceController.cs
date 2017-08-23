using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPieceController : MonoBehaviour {
	FishController fish;
	// Use this for initialization
	void Start () {
		fish = GetComponentInParent<FishController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c)
	{
		fish.ChildCollision (this.gameObject, c);
	}
}
