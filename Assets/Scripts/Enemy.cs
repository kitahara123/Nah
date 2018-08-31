using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;        // The speed the enemy moves at.
	public int HP = 2;                  // How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;            // A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;         // An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;      // An array of audioclips that can play when the enemy dies.
	public int obsession = 10; // Как далеко будет преследовать
	public float deathSpinMin = -100f;          // A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;           // A value to give the maximum amount of Torque when dying


	protected SpriteRenderer ren;         // Reference to the sprite renderer.
	protected bool dead = false;          // Whether or not the enemy is dead.
	protected GameObject player;
	protected Vector3 respawn;
	protected bool facingRight;


	void Awake()
	{
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		player = GameObject.FindWithTag("Player");
		respawn = transform.position;
		facingRight = true;
	}

	void FixedUpdate()
	{

		Vector3 plPos = player.transform.position;
		Vector3 dir = plPos - transform.position;

		if (Mathf.Abs(dir.x) > obsession) // Если игрок убежал слишком далеко
			dir = respawn - transform.position; // Отправляемся на респаун

		if (dir.x < 0 && transform.localScale.x > 0) Flip();
		if (dir.x > 0 && transform.localScale.x < 0) Flip();

		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(dir.x, -moveSpeed, moveSpeed), GetComponent<Rigidbody2D>().velocity.y);

		if (HP == 1 && damagedEnemy != null)
			ren.sprite = damagedEnemy;

		if (HP <= 0 && !dead)
			Death();
	}

	public void Hurt()
	{
		HP--;
	}

	protected void Death()
	{
		// Выключаем все ненужные спрайты
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Включаем тело и ставим спрайт трупа
		ren.enabled = true;
		ren.sprite = deadEnemy;

		dead = true;

		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));

		Collider2D[] cols = GetComponents<Collider2D>();
		foreach (Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		Destroy(gameObject, 2);
	}


	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;

		facingRight = !facingRight;
	}
}
