using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	public Vector3 offset;
	public float margin = 1f;
	public float smooth = 8f;

	private Transform player;      

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{

		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if (Mathf.Abs(transform.position.x - player.position.x) > margin)
			targetX = Mathf.Lerp(transform.position.x, player.position.x + offset.x, smooth * Time.deltaTime);

		if (Mathf.Abs(transform.position.y - player.position.y) > margin)
			targetY = Mathf.Lerp(transform.position.y, player.position.y + offset.y, smooth * Time.deltaTime);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
