using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	public float margin = 1f; // Отступ после которого камера начинает движение
	public float smooth = 8f; // Плавность, чем меньше тем плавнее

	private Transform player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{

		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// Интерполяция для более плавного движения камеры
		if (Mathf.Abs(transform.position.x - player.position.x) > margin)
			targetX = Mathf.Lerp(transform.position.x, player.position.x, smooth * Time.deltaTime);

		if (Mathf.Abs(transform.position.y - player.position.y) > margin)
			targetY = Mathf.Lerp(transform.position.y, player.position.y, smooth * Time.deltaTime);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
