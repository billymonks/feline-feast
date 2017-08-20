using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public float durability;
	public int score = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c)
	{
		if(c.relativeVelocity.magnitude > 2)
			durability -= c.relativeVelocity.magnitude;
		
		if (durability <= 0)
			Break ();
	}

	void Break()
	{
		
		BallisitaController.score += (int)(score * (1.0 + (BallisitaController.combo / 10.0)));
		BallisitaController.combo += 1;
		Destroy (gameObject);
	}
}
