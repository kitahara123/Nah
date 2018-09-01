using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;
	public float spawnDelay = 3f;
	public GameObject[] enemies;
	public bool onOff; // Выключает спавн мобов
	public int maxEnemyCount = 10;
	[HideInInspector] public int enemyCount = 0;

	void Start ()
	{
		onOff = true;
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		if (enemyCount >= maxEnemyCount) onOff = false;

		if (!onOff) CancelInvoke();

		enemyCount++;
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

		// Анимация спавна
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
