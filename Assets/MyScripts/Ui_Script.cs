using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui_Script : MonoBehaviour {

	private GameObject menu;

	void Start () {
		menu = GameObject.FindWithTag("PauseMenu").transform.GetChild(0).gameObject;
	}
	
	void Update () {

		// След от пальца just for fun
		if (Input.touchCount > 0) {

			Vector2 v = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = v;
		}


		if (Input.GetButtonDown("Cancel"))
		{
			menu.SetActive(!menu.activeInHierarchy); // Включаем и выключаем меню
		}

		if (menu.activeInHierarchy)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void Play(int num)
	{
		SceneManager.LoadScene(num);
	} 

	public void ExitGame()
	{
		Application.Quit();
	}
}
