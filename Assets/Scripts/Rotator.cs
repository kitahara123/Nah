﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	Rigidbody2D rg;

	// Use this for initialization
	void Start () {

		rg = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 vel = rg.velocity;

		float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, angle);
		
	}
}
