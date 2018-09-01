using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif
public class MyPlayerController : MonoBehaviour
{

	[SerializeField] private float force = 365f; // Скорость разгона
	[SerializeField] private float maxSpeed = 5f;
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float jumpForce = 200f;
	private bool grounded = false;
	private Transform groundCheck;
	private Animator anim;

	void Start()
	{

	}

	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		// Проверяем, что персонаж касается земли
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		jump = Input.GetButtonDown("Jump");
		#endif

		#if UNITY_ANDROID
		jump = CrossPlatformInputManager.GetButtonDown("Jump");
		#endif
		
		if (jump && grounded)
		{
			anim.SetTrigger("Jump");

			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			jump = false;

		}
		

	}

	void FixedUpdate()
	{
		float sp = 0f;
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		sp = Input.GetAxis("Horizontal");
		#endif

		#if UNITY_ANDROID
		sp = CrossPlatformInputManager.GetAxis("Horizontal");
		#endif

		//Анимация ходьбы
		anim.SetFloat("speed", Mathf.Abs(sp));

		// Толкаем персонажа пока не достигнем максимальной скорости
		if (sp * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * sp * force);

		// Если скорость начинает превышать максимальную, задаем разгон в нужную сторону
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		// Поворот персонажа
		if (sp > 0 && !facingRight)
			Flip();
		else if (sp < 0 && facingRight)
			Flip();

	}


	void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
