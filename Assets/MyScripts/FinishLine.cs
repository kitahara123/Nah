using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
			foreach (GameObject spawn in spawns)
			{
				spawn.GetComponent<Spawner>().onOff = false;
			}

			transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			transform.GetChild(1).GetComponent<ParticleSystem>().Play();
			transform.GetChild(2).GetComponent<ParticleSystem>().Play();

			GameObject.FindWithTag("Finish").GetComponent<Text>().enabled = true;

		}

	}
}
