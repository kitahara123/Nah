using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;            // The amount of damage to take when enemies touch the player

	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private MyPlayerController playerControl;		// Reference to the PlayerControl script.
	private Animator anim;                      // Reference to the Animator on the player
	private GameObject healthBar;


	void Awake ()
	{
		playerControl = GetComponent<MyPlayerController>();
		healthBar = GameObject.FindWithTag("HealthBar");
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		healthBar.GetComponent<Image>().fillAmount = health/100;
		healthBar.GetComponentInChildren<Text>().text = health.ToString();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Bullet")
		{
			if (col.gameObject.tag == "Bullet") Destroy(col.gameObject);

			// Короткое бессмертие после получения удара
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				if(health > 0f)
				{
					TakeDamage(col.transform);

					lastHitTime = Time.time; 
				}
				else
				{

					// Если жизни закончились переключаем коллайдеры в триггер и падаем
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Блочим контроллеры
					GetComponent<MyPlayerController>().enabled = false;
					GetComponentInChildren<Gun>().enabled = false;

					anim.SetTrigger("Die");
				}
			}
		}
	}


	void TakeDamage (Transform enemy)
	{
		playerControl.jump = false;

		// Отталкивание при ударе
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		health -= damageAmount;

		// Проигрываем звук получения урона
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


}
