using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRigidBodyPlatform : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		Rigidbody2D[] bodies = gameObject.GetComponents<Rigidbody2D>();

		foreach (Rigidbody2D body in bodies)
		{
			body.bodyType = RigidbodyType2D.Static;
		}
	}
}
