using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerEnemy : Enemy {

	public Rigidbody2D rocket;
	public float bulletSpeed = 300f;

	private LayerMask mask = 1 << 12;
	private bool onCooldown = false;
	private Vector3 dir;
	private Transform gun;

	void Start()
	{
		gun = transform.GetChild(0);

	}

	void FixedUpdate()
	{

		Vector3 plPos = player.transform.position;
		dir = plPos - transform.position;

		if (Mathf.Abs(dir.x) > obsession) // Если игрок убежал слишком далеко
		{ 
			dir = respawn - transform.position; // Отправляемся на респаун
		}

		if (dir.x < 0 && transform.localScale.x > 0) Flip();
		if (dir.x > 0 && transform.localScale.x < 0) Flip();

		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 5, mask);

		if (hit.collider == null)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(dir.x, -moveSpeed, moveSpeed), GetComponent<Rigidbody2D>().velocity.y);
		else
		{
			if (!onCooldown)
				Invoke("Shoot", 2);
			onCooldown = true;
		}


		if (HP == 1 && damagedEnemy != null)
			ren.sprite = damagedEnemy;

		if (HP <= 0 && !dead)
			Death();
	}
	void Shoot()
	{
		Rigidbody2D bulletInstance = Instantiate(rocket, gun.position, Quaternion.Euler(new Vector3(0, 0, facingRight ? 0 : 180f))) as Rigidbody2D;
		bulletInstance.AddForce((dir) * bulletSpeed);

		onCooldown = false;
	}

}
