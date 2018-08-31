using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Border : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// Перезапускаем уровень если игрок выпадает за границы
	/// </summary>
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
