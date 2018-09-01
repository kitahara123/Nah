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
				spawn.GetComponent<Spawner>().onOff = false; // Выключаем спавн мобов
			}

			//Firework
			foreach (Transform c in transform)
				c.GetComponent<ParticleSystem>().Play();

			//WINNER WINNER CHICKEN DINNER
			GameObject.FindWithTag("Finish").GetComponent<Text>().enabled = true;

		}

	}
}
