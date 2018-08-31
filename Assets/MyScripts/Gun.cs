using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;
	public float speed = 20f;               // Скорость полета снаряда

	private MyPlayerController playerCtrl;
	private bool ceasefire = false; // Не стрелять при нажатии кнопок
	

	void Awake()
	{
		playerCtrl = transform.root.GetComponent<MyPlayerController>();

	}

	public void CeaseFire()
	{
		ceasefire = true;
	}

	void Update()
	{
		Vector3 MousePos = new Vector3(0, 0, 0);
		bool shoot = Input.GetButtonDown("Fire1");

		#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		//MousePos = Input.mousePosition;
		#endif

		#if UNITY_ANDROID
		for (int i = 0; i < Input.touchCount; i++)
		{
			shoot = Input.GetTouch(i).phase == TouchPhase.Began;
			if (shoot && !ceasefire) MousePos = Input.GetTouch(i).position; // Не считываем позицию пальца находящегося на джойстике
		}
		#endif

		if (!MousePos.Equals(new Vector3(0, 0, 0)))
		{
			// Получаем позицию пушки относительно камеры
			Vector3 MyPos = Camera.main.WorldToScreenPoint(transform.position);

			// Получаем направление на курсор
			MousePos = MousePos - MyPos;

			// Получаем градусы отклонения векора направления от оси X
			float angle = Mathf.Atan2(MousePos.y, MousePos.x) * Mathf.Rad2Deg;

			// Крутим пушку на нужное количество градусов
			transform.parent.parent.parent.rotation = playerCtrl.facingRight ? Quaternion.Euler(0, 0, angle) : Quaternion.Euler(180, 180, angle);
		}

		if (!ceasefire && shoot && Time.timeScale > 0)
		{

			GetComponent<AudioSource>().Play();

			if (playerCtrl.facingRight)
			{
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
				bulletInstance.AddForce(transform.right * speed);
			}
			else
			{
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				bulletInstance.AddForce(transform.right * -speed);
			}
		}
		ceasefire = false;
	}

}
