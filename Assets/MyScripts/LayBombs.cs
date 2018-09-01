using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif

public class LayBombs : MonoBehaviour
{
	public int bombCount = 0;     
	public int maxBombCount = 0;
	public AudioClip bombsAway;			// Звук установки бомбы
	public GameObject bomb;            
	private bool doBomb = false;

	void Awake ()
	{
		InvokeRepeating("RegenerateBomb", 5, 5);
	}

	void Update ()
	{
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		doBomb = Input.GetButtonDown("Fire2");
		#endif

		#if UNITY_ANDROID
		doBomb = CrossPlatformInputManager.GetButtonDown("Fire2");
		#endif

		if (doBomb && bombCount > 0 && Time.timeScale > 0)
		{
			bombCount--;
			AudioSource.PlayClipAtPoint(bombsAway,transform.position);
			Instantiate(bomb, transform.position, transform.rotation);

			doBomb = false;
		}

	}
	
	private void RegenerateBomb()
	{
		if (bombCount < maxBombCount)
			bombCount++;
	}

}
