using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
	void Start()
	{
		Destroy(gameObject, 2);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
			if (col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
			Destroy(gameObject);
	}
}
