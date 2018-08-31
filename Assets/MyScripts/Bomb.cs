using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	public float bombRadius = 10f;          // Radius within which enemies are killed.
	public float bombForce = 100f;          // Force that enemies are thrown from the blast.
	public AudioClip boom;                  // Audioclip of explosion.
	public AudioClip fuse;                  // Audioclip of fuse.
	public float fuseTime = 1.5f;
	public GameObject explosion;            // Prefab of explosion effect.

	private ParticleSystem explosionFX;     // Reference to the particle system of the explosion effect.
	private bool readyToExplode;

	void Awake()
	{
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
	}

	void Start()
	{
		StartCoroutine(BombDetonation());
	}


	IEnumerator BombDetonation()
	{
		AudioSource.PlayClipAtPoint(fuse, transform.position);

		yield return new WaitForSeconds(fuseTime);

		readyToExplode = true;
	}


	void OnTriggerEnter2D(Collider2D other) { Explode(); }
	void OnTriggerStay2D(Collider2D other) { Explode(); }

	public void Explode()
	{
		if (!readyToExplode) return;

		// Находит всех вокруг в определённом радиусе
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius);


		// Расталкивает всех
		foreach (Collider2D en in enemies)
		{
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if (rb == null) continue;

			// Направление от бомбы к врагу
			Vector3 deltaPos = rb.transform.position - transform.position;

			Vector3 force = deltaPos.normalized * bombForce;
			rb.AddForce(force);

			if (rb != null && rb.tag == "Enemy")
				rb.gameObject.GetComponent<Enemy>().HP = 0;
		}

		// Эфект взрыва
		explosionFX.transform.position = transform.position;
		explosionFX.Play();

		// Эфект взрыва
		Instantiate(explosion, transform.position, Quaternion.identity);

		AudioSource.PlayClipAtPoint(boom, transform.position);

		Destroy(gameObject);
	}
}
